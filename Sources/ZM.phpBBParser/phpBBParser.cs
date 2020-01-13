using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZM.phpBBParser
{
    public class phpBBParser
    {
        public phpBBParser()
        {
            Settings = new phpBBParserSettings();

            Settings.StyleSheetProcessing = FileProcessingType.DoNothing;
            Settings.ImageProcessing = FileProcessingType.DoNothing;
            Content = string.Empty;
        }

        public virtual phpBBParserSettings Settings { get; set; }

        public virtual string Content { get; set; }

        public virtual string PageName { get; set; }

        public virtual string Title { get; set; }

        private HtmlDocument document { get; set; }

        private string CleanURL(string url)
        {
            if (url.StartsWith("/") && url.Length > 1)
                return url.Substring(1);
            else if (url.ToUpper().StartsWith(Settings.SiteRoot.ToUpper() + "/") && url.Length > Settings.SiteRoot.Length + 1)
                return url.Substring(Settings.SiteRoot.Length + 1);
            else if (url.ToUpper().StartsWith("HTTP"))
                throw new FormatException($"L'URL {url} n'est pas supportée.");
            else
                return url;
        }

        #region Public methods

        public virtual string GetOutputFileName()
        {
            string outputFileName = Settings.OutputFileName;

            if (Settings.OutputFileNaming == OutputFileNamingType.Url)
                outputFileName = PageName;
            else if (Settings.OutputFileNaming == OutputFileNamingType.Title)
                outputFileName = Title;

            if (outputFileName.ToUpper().EndsWith(".HTM"))
                outputFileName = outputFileName.Substring(0, outputFileName.Length - 4);
            else if (outputFileName.ToUpper().EndsWith(".HTML"))
                outputFileName = outputFileName.Substring(0, outputFileName.Length - 5);

            outputFileName = EscapeFileName(outputFileName);

            return outputFileName;
        }

        public void Retrieve(string urlOrPage, bool loadNextPages = true)
        {
            var page = CleanURL(urlOrPage);

            if (!page.StartsWith("t"))
                throw new NotSupportedException("L'archivage ne supporte que le téléchargement de threads complets.");

            FireProgress("Démarrage", 0, 1);

            if (loadNextPages)
                page = GetPageName(page, 1);

            PageName = page;

            var web = new HtmlWeb();
            document = web.Load(Settings.SiteRoot + "/" + page);

            RemoveScripts(document.DocumentNode);
            FixStyleSheets();
            FixImages();

            // Read document title
            var head = GetHead();
            var title = head.ChildNodes.Where(x => x.Name.ToUpper() == "TITLE").FirstOrDefault();
            if (title != null)
                Title = title.InnerText;

            // Remove ticker section
            RemoveTicker();
            // Remove the criteo ad
            RemoveCriteo();
            // Remove main menu
            RemoveMenu();
            // Remove page footer
            var footer = GetPageFooter();
            footer.Remove();
            // Get last page number
            int iLastPageNumber = GetLastPageNumber();
            // Remove thread footer
            RemoveThreadFooter();

            FireProgress($"Page 1 sur {iLastPageNumber}", 1, iLastPageNumber);

            // Fix relative links
            FixLinks();


            var threadParentNode = GetThreadParentNode();
            // Remove top pagination
            threadParentNode.ChildNodes[1].Remove();
            // Remove sponsorised post at the end
            var post0 = threadParentNode.ChildNodes.Where(x => x.Attributes["class"]?.Value.ToUpper() == "POST POST--0").FirstOrDefault();
            if (post0 != null)
                post0.Remove();
            post0 = threadParentNode.ChildNodes.Where(x => x.Attributes["class"]?.Value.ToUpper() == "POST--0").FirstOrDefault();
            if (post0 != null)
                post0.Remove();
            // Remove spacers between posts
            var spacers = threadParentNode.ChildNodes.Where(x => x.Attributes["class"]?.Value.StartsWith("post--") ?? false).ToArray();
            for (int i = spacers.Length - 1; i > -1; i--)
                spacers[i].Remove();

            // Load next pages and insert posts
            if (loadNextPages)
            {
                var lastTitle = threadParentNode.ChildNodes.Last();
                if (iLastPageNumber > 1)
                {
                    for (int i = 2; i <= iLastPageNumber; i++)
                    {
                        FireProgress($"Page {i} sur {iLastPageNumber}", i, iLastPageNumber);

                        var p = new phpBBParser() { Settings = Settings };
                        p.Retrieve(GetPageName(page, i), false);
                        var posts = p.GetPosts();
                        foreach (var post in posts)
                            threadParentNode.InsertBefore(post, lastTitle);
                    }
                }
            }

            // Remove last 2 divs of body (RGPD disclosure and unused video div)
            var body = GetBody();
            if (body != null)
            {
                var lastdiv = body.ChildNodes.Where(x => x.Name.ToUpper() == "DIV").LastOrDefault();
                body.ChildNodes.Remove(lastdiv);
                lastdiv = body.ChildNodes.Where(x => x.Name.ToUpper() == "DIV").LastOrDefault();
                body.ChildNodes.Remove(lastdiv);
            }

            // If loadNextPages, then we are on first page processing
            if (loadNextPages)
            {
                // Download stylesheets content if necessary
                ProcessStyleSheets("");

                // Download images
                var imagesPath = GetOutputFileName() + " - images";
                if (Settings.DownloadImagesInCommonFolder)
                    imagesPath = "images";

                var imagesFullPath = Path.Combine(Settings.OutputPath, imagesPath);
                if (Settings.ImageProcessing == FileProcessingType.Download && !Directory.Exists(imagesFullPath))
                    Directory.CreateDirectory(imagesFullPath);
                ProcessImages(imagesPath);
            }

            Content = document.DocumentNode.OuterHtml;
        }

        public virtual void SaveToFile()
        {
            var outputFilePath = Path.Combine(Settings.OutputPath, GetOutputFileName() + ".html");

            File.WriteAllText(outputFilePath, Content);
        }

        #endregion

        private string EscapeFileName(string filename)
        {
            var outputFileName = filename;
            outputFileName = outputFileName.Replace("/", "_");
            outputFileName = outputFileName.Replace(@"\", "_");
            outputFileName = outputFileName.Replace("?", "_");
            outputFileName = outputFileName.Replace("!", "_");
            outputFileName = outputFileName.Replace("*", "_");
            outputFileName = outputFileName.Replace(":", "_");
            return outputFileName;
        }

        #region Fixers

        private void FixAttributeUrl(HtmlNode[] nodes, string attributeName)
        {
            foreach (var n in nodes)
            {
                if (n.Attributes[attributeName] != null)
                    n.Attributes[attributeName].Value = FixUrl(n.Attributes[attributeName]?.Value);
            }
        }

        private void FixImages()
        {
            var images = GetImages(document.DocumentNode);
            FixAttributeUrl(images, "src");
        }

        private void FixLinks()
        {
            var links = GetLinks(document.DocumentNode);
            FixAttributeUrl(links, "href");
        }

        private void FixStyleSheets()
        {
            var styleSheets = GetStylesheets();
            FixAttributeUrl(styleSheets, "href");
        }

        private string FixUrl(string url)
        {
            if (url?.StartsWith("/") ?? false)
                return Settings.SiteRoot + url;
            else if (!url?.StartsWith("http") ?? false)
                return Settings.SiteRoot + "/" + url;
            else
                return url;
        }

        #endregion

        private void ProcessImages(string path)
        {
            var nodes = GetImages(document.DocumentNode);
            ProcessFiles(nodes, "src", path, ProcessImage, ProcessImageTag);
        }

        private void ProcessStyleSheets(string path)
        {
            var nodes = GetStylesheets();
            ProcessFiles(nodes, "href", path, ProcessStyleSheet, ProcessStyleSheetTag);
        }

        private void ProcessFiles(HtmlNode[] nodes, string attributeName, string path, ProcessFile processMethod, ProcessFileTag processTagMethod)
        {
            var d = GetFilesDictionnary(nodes, attributeName);

            for (int i = 0; i < d.Keys.Count; i++)
            {
                var file = d.Keys.ElementAt(i);
                FireProgress(file, i, d.Keys.Count - 1);
                d[file] = processMethod(file, path);
            }

            foreach (var n in nodes)
                processTagMethod(n, d);
        }

        private delegate string ProcessFile(string file, string path);

        private delegate void ProcessFileTag(HtmlNode node, Dictionary<string, string> dicData);

        private void ProcessImageTag(HtmlNode node, Dictionary<string, string> dicData)
        {
            var src = node.Attributes["src"]?.Value;
            if (src != null)
                node.Attributes["src"].Value = dicData[src];
        }

        private void ProcessStyleSheetTag(HtmlNode node, Dictionary<string, string> dicData)
        {
            if (Settings.StyleSheetProcessing == FileProcessingType.Embed)
            {
                var href = node.Attributes["href"]?.Value;
                if (href != null)
                {
                    while (node.Attributes.Count > 0)
                        node.Attributes[node.Attributes.Count - 1].Remove();

                    node.Name = "style";
                    node.InnerHtml = dicData[href];
                }
            }
            else if (Settings.StyleSheetProcessing == FileProcessingType.Download)
            {
                var href = node.Attributes["href"]?.Value;
                if (href != null)
                    node.Attributes["href"].Value = dicData[href];
            }
        }

        private string ProcessStyleSheet(string file, string path)
        {
            if (Settings.StyleSheetProcessing == FileProcessingType.DoNothing)
                return file;
            else if (Settings.StyleSheetProcessing == FileProcessingType.Download)
            {
                var outputFileName = EscapeFileName(file);

                var s = DownloadTextFile(file);
                File.WriteAllText(Path.Combine(Settings.OutputPath, path, outputFileName), s);
                return Path.Combine(path, outputFileName);
            }
            else if (Settings.StyleSheetProcessing == FileProcessingType.Embed)
                return DownloadTextFile(file);
            else
                return string.Empty;
        }

        private string ProcessImage(string file, string path)
        {
            if (Settings.ImageProcessing == FileProcessingType.DoNothing)
                return file;
            else if (Settings.ImageProcessing == FileProcessingType.Download)
            {
                var outputFileName = EscapeFileName(file);

                var b = DownloadBinaryFile(file);
                File.WriteAllBytes(Path.Combine(Settings.OutputPath, path, outputFileName), b);
                return Path.Combine(path, outputFileName);
            }
            else if (Settings.ImageProcessing == FileProcessingType.Embed)
            {
                var b = DownloadBinaryFile(file);

                string s = Convert.ToBase64String(b);

                string imageType = string.Empty;
                if (file.ToUpper().EndsWith(".JPG") || file.ToUpper().EndsWith(".JPEG"))
                    imageType = "image/jpeg";
                else if (file.ToUpper().EndsWith(".PNG"))
                    imageType = "image/png";
                else if (file.ToUpper().EndsWith(".GIF"))
                    imageType = "image/gif";

                return $"data:{imageType};base64,{s}";
            }
            else
                return string.Empty;
        }

        private Dictionary<string, string> GetFilesDictionnary(HtmlNode[] images, string attributeName)
        {
            var d = new Dictionary<string, string>();

            foreach (var i in images)
            {
                var src = i.Attributes[attributeName]?.Value;
                if (src != null)
                {
                    if (!d.ContainsKey(src))
                        d.Add(src, "");
                }
            }

            return d;
        }

        private byte[] DownloadBinaryFile(string url)
        {
            try
            {
                return new WebClient().DownloadData(url);
            }
            catch (Exception ex)
            {
                return new byte[0];
            }
        }

        private string DownloadTextFile(string url)
        {
            try
            {
                return new WebClient().DownloadString(url);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }



        private HtmlNode[] GetLinks(HtmlNode node)
        {
            return GetTags(node, "A");
        }

        private HtmlNode[] GetImages(HtmlNode node)
        {
            return GetTags(node, "IMG");
        }

        private HtmlNode[] GetStylesheets()
        {
            var head = GetHead();
            var styleSheets = head.ChildNodes.Where(x => x.Name.ToUpper() == "LINK" && x.Attributes["rel"]?.Value.ToUpper() == "STYLESHEET").ToArray();
            return styleSheets;
        }

        private HtmlNode[] GetTags(HtmlNode node, string tagName)
        {
            var l = new List<HtmlNode>();
            foreach (var n in node.ChildNodes)
            {
                if (n.Name.ToUpper() == tagName.ToUpper())
                    l.Add(n);

                l.AddRange(GetTags(n, tagName));
            }
            return l.ToArray();
        }



        private string GetPageName(string page, int pageNumber)
        {
            int iHash = page.IndexOf('#');
            if (iHash > 0)
                page = page.Substring(0, iHash);

            int iMinus = page.IndexOf('-');
            if (iMinus > 0)
            {
                var postNumber = page.Substring(0, iMinus);
                var postName = page.Substring(iMinus);

                int iP = postNumber.IndexOf('p');
                if (iP > 0)
                    postNumber = postNumber.Substring(0, iP);

                var postIndex = ((pageNumber > 1) ? $"p{(pageNumber - 1) * 25}" : "");

                return $"{postNumber}{postIndex}{postName}";
            }

            throw new FormatException("Le nom de la page n'est pas un thread valide.");
        }

        #region Removers

        private void RemoveCriteo()
        {
            var body = GetPageBody();
            if (body != null)
            {
                var firstDiv = body.ChildNodes.Where(x => x.Name.ToUpper() == "DIV").FirstOrDefault();
                var firstTable = firstDiv.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").FirstOrDefault();
                var firstTbody = firstTable.ChildNodes.Where(x => x.Name.ToUpper() == "TBODY").FirstOrDefault();
                var firstTr = firstTbody.ChildNodes.Where(x => x.Name.ToUpper() == "TR").FirstOrDefault();

                var td = firstTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").FirstOrDefault();
                if (td != null)
                    td.Remove();
                td = firstTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").FirstOrDefault();
                var div = td?.ChildNodes.Where(x => x.Name.ToUpper() == "DIV").FirstOrDefault();
                if (div != null)
                    div.Remove();
            }
        }

        private void RemoveMenu()
        {
            var body = GetBody();
            if (body != null)
            {
                var firstTable = body.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").FirstOrDefault();
                var firstTr = firstTable.ChildNodes.Where(x => x.Name.ToUpper() == "TR").FirstOrDefault();
                var firstTd = firstTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").FirstOrDefault();
                var tables = firstTd.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").ToArray();
                if (tables.Length > 1)
                    tables[1].Remove();
            }
        }

        private void RemoveScripts(HtmlNode node)
        {
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                var subNode = node.ChildNodes[i];
                if (subNode.Name.ToUpper() == "SCRIPT")
                {
                    subNode.Remove();
                    i--;
                }
                else
                    RemoveScripts(subNode);
            }
        }

        private void RemoveThreadFooter()
        {
            var body = GetPageBody();
            if (body != null)
            {
                var firstDiv = body.ChildNodes.Where(x => x.Name.ToUpper() == "DIV").FirstOrDefault();
                var firstTable = firstDiv.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").FirstOrDefault();
                var firstTbody = firstTable.ChildNodes.Where(x => x.Name.ToUpper() == "TBODY").FirstOrDefault();
                var firstTr = firstTbody.ChildNodes.Where(x => x.Name.ToUpper() == "TR").FirstOrDefault();
                var firstTd = firstTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").FirstOrDefault();

                // Remove last 4 nodes
                for (int i = 0; i < 4; i++)
                {
                    var node = firstTd.ChildNodes.LastOrDefault();
                    if (node != null)
                        node.Remove();
                }

                var tables = firstTd.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").ToArray();
                for (int i = tables.Length - 1; i > 1; i--)
                    tables[i].Remove();
            }
        }

        private void RemoveTicker()
        {
            var body = GetBody();
            if (body != null)
            {
                var firstTable = body.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").FirstOrDefault();
                var firstTr = firstTable.ChildNodes.Where(x => x.Name.ToUpper() == "TR").FirstOrDefault();
                var firstTd = firstTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").FirstOrDefault();
                var div = firstTd.ChildNodes.Where(x => x.Name.ToUpper() == "DIV" && x.Attributes["id"]?.Value == "fa_ticker_block").FirstOrDefault();
                if (div != null)
                    div.Remove();
            }
        }

        #endregion

        #region Getters

        private HtmlNode GetBody()
        {
            var html = document?.DocumentNode.ChildNodes.Where(x => x.Name.ToUpper() == "HTML").FirstOrDefault();
            return html?.ChildNodes.Where(x => x.Name.ToUpper() == "BODY").FirstOrDefault();
        }

        private HtmlNode GetHead()
        {
            var html = document?.DocumentNode.ChildNodes.Where(x => x.Name.ToUpper() == "HTML").FirstOrDefault();
            return html?.ChildNodes.Where(x => x.Name.ToUpper() == "HEAD").FirstOrDefault();
        }

        private int GetLastPageNumber()
        {
            var body = GetPageBody();
            if (body != null)
            {
                var firstDiv = body.ChildNodes.Where(x => x.Name.ToUpper() == "DIV").FirstOrDefault();
                var firstTable = firstDiv.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").FirstOrDefault();
                var firstTbody = firstTable.ChildNodes.Where(x => x.Name.ToUpper() == "TBODY").FirstOrDefault();
                var firstTr = firstTbody.ChildNodes.Where(x => x.Name.ToUpper() == "TR").FirstOrDefault();
                var firstTd = firstTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").FirstOrDefault();
                var tables = firstTd.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").ToArray();

                if (tables.Length > 2)
                {
                    var paginationTable = tables[2];
                    var secondTr = paginationTable.ChildNodes.Where(x => x.Name.ToUpper() == "TR").FirstOrDefault();
                    var secondTd = secondTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").ToArray();
                    if (secondTd.Length > 1)
                    {
                        var span = secondTd[1].ChildNodes.Where(x => x.Name.ToUpper() == "SPAN").FirstOrDefault();
                        var aList = span.ChildNodes.Where(x => x.Name.ToUpper() == "A").ToArray();
                        var lastPage = aList[aList.Length - 2].InnerText;
                        if (int.TryParse(lastPage, out int iLastPage))
                            return iLastPage;
                    }
                }
            }

            return 1;
        }

        private HtmlNode GetPageBody()
        {
            var body = GetBody();
            if (body != null)
            {
                var firstTable = body.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").FirstOrDefault();
                var firstTr = firstTable.ChildNodes.Where(x => x.Name.ToUpper() == "TR").FirstOrDefault();
                var firstTd = firstTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").FirstOrDefault();
                var div = firstTd.ChildNodes.Where(x => x.Name.ToUpper() == "DIV" && x.Attributes["id"]?.Value == "page-body").FirstOrDefault();
                if (div != null)
                    return div;
            }

            throw new FormatException("Missing page-body section.");
        }

        private HtmlNode GetPageFooter()
        {
            var body = GetBody();
            if (body != null)
            {
                var firstTable = body.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").FirstOrDefault();
                var firstTr = firstTable.ChildNodes.Where(x => x.Name.ToUpper() == "TR").FirstOrDefault();
                var firstTd = firstTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").FirstOrDefault();
                var div = firstTd.ChildNodes.Where(x => x.Name.ToUpper() == "DIV" && x.Attributes["id"]?.Value == "page-footer").FirstOrDefault();
                if (div != null)
                    return div;
            }

            throw new FormatException("Missing page-footer section.");
        }

        public virtual HtmlNode[] GetPosts()
        {
            var node = GetThreadParentNode();
            return node.ChildNodes.Where(x => x.Attributes["class"]?.Value.StartsWith("post post--") ?? false).ToArray();
        }

        private HtmlNode GetThreadParentNode()
        {
            var body = GetPageBody();
            if (body != null)
            {
                var firstDiv = body.ChildNodes.Where(x => x.Name.ToUpper() == "DIV").FirstOrDefault();
                var firstTable = firstDiv.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").FirstOrDefault();
                var firstTbody = firstTable.ChildNodes.Where(x => x.Name.ToUpper() == "TBODY").FirstOrDefault();
                var firstTr = firstTbody.ChildNodes.Where(x => x.Name.ToUpper() == "TR").FirstOrDefault();
                var firstTd = firstTr.ChildNodes.Where(x => x.Name.ToUpper() == "TD").FirstOrDefault();
                var tables = firstTd.ChildNodes.Where(x => x.Name.ToUpper() == "TABLE").ToArray();

                if (tables.Length > 1)
                    return tables[1];
            }

            return null;
        }

        #endregion

        public event EventHandler<ProgressEventArgs> Progress;

        private void FireProgress(string message, int value, int maximum)
        {
            var e = new ProgressEventArgs() { Message = message, Value = value, MaximumValue = maximum };
            Progress?.Invoke(this, e);
        }
    }

    public enum FileProcessingType
    {
        DoNothing = 0,
        Download = 1,
        Embed = 2
    }

    public enum OutputFileNamingType
    {
        Url = 0,
        Title = 1,
        Other = 2
    }
}
