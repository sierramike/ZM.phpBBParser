using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace ZM.phpBBParser
{
    public partial class frmMaster : Form
    {
        private string SettingsFileName { get; set; }

        public frmMaster()
        {
            var exeLocation = System.Reflection.Assembly.GetEntryAssembly().Location;
            var fi = new FileInfo(exeLocation);
            SettingsFileName = Path.Combine(fi.DirectoryName, "Settings.xml");

            InitializeComponent();

            lstImageProcessing.Items.Add("Ne rien faire");
            lstImageProcessing.Items.Add("Télécharger les fichiers");
            lstImageProcessing.Items.Add("Inclure dans le fichier de sortie (augmentation du volume)");
            lstImageProcessing.SelectedIndex = 0;

            lstStyleSheetProcessing.Items.Add("Ne rien faire");
            lstStyleSheetProcessing.Items.Add("Télécharger les fichiers");
            lstStyleSheetProcessing.Items.Add("Inclure dans le fichier de sortie (augmentation du volume)");
            lstStyleSheetProcessing.SelectedIndex = 0;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(txtOutputFolder.Text))
                    Directory.CreateDirectory(txtOutputFolder.Text);

                var s = GetSettings();

                var p = new phpBBParser() { Settings = s };
                p.Progress += P_Progress;

                var t = new Thread(() => Download(p, txtURL.Text));
                t.Start();
            }
            catch   (Exception ex)
            {
                MessageBox.Show($"Erreur :\n\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Download(phpBBParser p, string url)
        {
            try
            {
                p.Retrieve(url);

                p.SaveToFile();

                P_Progress(this, new ProgressEventArgs() { Message = "Terminé.", Value = 1, MaximumValue = 1 });
            }
            catch (Exception ex)
            {
                P_Progress(this, new ProgressEventArgs() { Message = ex.Message, Value = 0, MaximumValue = 0 });
            }
        }

        private void P_Progress(object sender, ProgressEventArgs e)
        {
            if (progressBar1.InvokeRequired)
            {
                var c = new EventHandler<ProgressEventArgs>(P_Progress);
                Invoke(c, new object[] { sender, e });
            }
            else
            {
                lblProgress.Text = e.Message;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = e.MaximumValue;
                progressBar1.Value = e.Value;
                if (e.Value == 0 && e.MaximumValue == 0)
                {
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
                    TaskbarManager.Instance.SetProgressValue(100, 100);
                }
                else
                {
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                    TaskbarManager.Instance.SetProgressValue(e.Value, e.MaximumValue);
                }
            }
        }

        private void rdoOther_CheckedChanged(object sender, EventArgs e)
        {
            txtOutputfilename.Enabled = rdoOther.Checked;
        }

        private void btnBrowseOutputPath_Click(object sender, EventArgs e)
        {
            var fd = new FolderBrowserDialog();
            fd.SelectedPath = txtOutputFolder.Text;
            fd.ShowNewFolderButton = true;

            if (fd.ShowDialog() == DialogResult.OK)
                txtOutputFolder.Text = fd.SelectedPath;
        }

        private void lstImageProcessing_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkDowloadImagesToCommonFolder.Visible = (lstImageProcessing.SelectedIndex == 1);
        }

        #region Clipboard

        private IntPtr _ClipboardViewerNext;

        private void RegisterClipboardViewer()
        {
            _ClipboardViewerNext = User32.SetClipboardViewer(this.Handle);
        }

        private void UnregisterClipboardViewer()
        {
            User32.ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
        }

        protected override void WndProc(ref Message m)
        {
            switch ((Msgs)m.Msg)
            {
                case Msgs.WM_DRAWCLIPBOARD:
                    GetClipboardData();
                    User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;

                case Msgs.WM_CHANGECBCHAIN:
                    if (m.WParam == _ClipboardViewerNext)
                        _ClipboardViewerNext = m.LParam;
                    else
                        User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);

                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void GetClipboardData()
        {
            // Data on the clipboard uses the IDataObject interface
            IDataObject iData = new DataObject();

            try
            {
                iData = Clipboard.GetDataObject();
            }
            catch
            {
                return;
            }

            if (iData.GetDataPresent(DataFormats.Text))
            {
                var sText = iData.GetData(DataFormats.Text) as string;
                if (sText != null)
                {
                    if (sText.ToUpper().StartsWith(txtSiteRoot.Text.ToUpper()))
                    {
                        if (txtURL.Text != sText)
                        {
                            txtURL.Text = sText;
                            if (chkAutoDownload.Checked)
                                btnDownload_Click(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        #endregion

        private void frmMaster_Load(object sender, EventArgs e)
        {
            RegisterClipboardViewer();

            var s = LoadSettings();
            if (s != null)
                PutSettings(s);
        }

        private void frmMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterClipboardViewer();

            var s = GetSettings();

            SaveSettings(s);
        }

        private phpBBParserSettings LoadSettings()
        {
            phpBBParserSettings s = null;

            if (File.Exists(SettingsFileName))
            {
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(phpBBParserSettings));
                var xml = File.ReadAllText(SettingsFileName);

                using (var reader = new StringReader(xml))
                {
                    s = (phpBBParserSettings)xs.Deserialize(reader);
                }
            }
            return s;
        }

        private void SaveSettings(phpBBParserSettings s)
        {
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(phpBBParserSettings));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (var writer = System.Xml.XmlWriter.Create(sww))
                {
                    xs.Serialize(writer, s);
                    xml = sww.ToString();
                }
            }

            File.WriteAllText(SettingsFileName, xml);
        }

        private phpBBParserSettings GetSettings()
        {
            var s = new phpBBParserSettings()
            {
                AutoDownload = chkAutoDownload.Checked,
                SiteRoot = txtSiteRoot.Text,
                StyleSheetProcessing = (FileProcessingType)lstStyleSheetProcessing.SelectedIndex,
                ImageProcessing = (FileProcessingType)lstImageProcessing.SelectedIndex,
                DownloadImagesInCommonFolder = chkDowloadImagesToCommonFolder.Checked,
                OutputPath = txtOutputFolder.Text,
                OutputFileName = txtOutputfilename.Text,
                OutputFileNaming = (rdoUrl.Checked ? OutputFileNamingType.Url : (rdoTitle.Checked ? OutputFileNamingType.Title : OutputFileNamingType.Other))
            };
            return s;
        }

        private void PutSettings(phpBBParserSettings s)
        {
            txtSiteRoot.Text = s.SiteRoot;
            lstStyleSheetProcessing.SelectedIndex = (int)s.StyleSheetProcessing;
            lstImageProcessing.SelectedIndex = (int)s.ImageProcessing;
            chkDowloadImagesToCommonFolder.Checked = s.DownloadImagesInCommonFolder;
            txtOutputFolder.Text = s.OutputPath;
            txtOutputfilename.Text = s.OutputFileName;
            rdoUrl.Checked = (s.OutputFileNaming == OutputFileNamingType.Url);
            rdoTitle.Checked = (s.OutputFileNaming == OutputFileNamingType.Title);
            rdoOther.Checked = (s.OutputFileNaming == OutputFileNamingType.Other);
            chkAutoDownload.Checked = s.AutoDownload;
        }
    }
}
