using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZM.phpBBParser
{
    public class phpBBParserSettings
    {
        public phpBBParserSettings()
        {

        }

        private string _siteRoot;
        public virtual string SiteRoot
        {
            get { return _siteRoot; }
            set
            {
                _siteRoot = value;
                if (_siteRoot.EndsWith("/"))
                    _siteRoot = _siteRoot.Substring(0, _siteRoot.Length - 1);
            }
        }

        public virtual FileProcessingType ImageProcessing { get; set; }

        public virtual FileProcessingType StyleSheetProcessing { get; set; }

        public virtual bool DownloadImagesInCommonFolder { get; set; }

        public virtual string OutputPath { get; set; }

        public virtual string OutputFileName { get; set; }

        public virtual OutputFileNamingType OutputFileNaming { get; set; }

        public virtual bool AutoDownload { get; set; }
    }
}
