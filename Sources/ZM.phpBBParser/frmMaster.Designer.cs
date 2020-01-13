namespace ZM.phpBBParser
{
    partial class frmMaster
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaster));
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.txtSiteRoot = new System.Windows.Forms.TextBox();
            this.lblSiteRoot = new System.Windows.Forms.Label();
            this.lstImageProcessing = new System.Windows.Forms.ComboBox();
            this.lblImageProcessing = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOutputfilename = new System.Windows.Forms.TextBox();
            this.rdoOther = new System.Windows.Forms.RadioButton();
            this.rdoTitle = new System.Windows.Forms.RadioButton();
            this.rdoUrl = new System.Windows.Forms.RadioButton();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseOutputPath = new System.Windows.Forms.Button();
            this.lblStyleSheetProcessing = new System.Windows.Forms.Label();
            this.lstStyleSheetProcessing = new System.Windows.Forms.ComboBox();
            this.chkDowloadImagesToCommonFolder = new System.Windows.Forms.CheckBox();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAutoDownload = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(27, 166);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(104, 13);
            this.lblURL.TabIndex = 0;
            this.lblURL.Text = "Adresse de la page :";
            // 
            // txtURL
            // 
            this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtURL.Location = new System.Drawing.Point(190, 163);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(602, 20);
            this.txtURL.TabIndex = 0;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(190, 200);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(104, 34);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Télécharger";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // txtSiteRoot
            // 
            this.txtSiteRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSiteRoot.Location = new System.Drawing.Point(178, 26);
            this.txtSiteRoot.Name = "txtSiteRoot";
            this.txtSiteRoot.Size = new System.Drawing.Size(596, 20);
            this.txtSiteRoot.TabIndex = 0;
            this.txtSiteRoot.Text = "http://le-forum-du-n.1fr1.net";
            // 
            // lblSiteRoot
            // 
            this.lblSiteRoot.AutoSize = true;
            this.lblSiteRoot.Location = new System.Drawing.Point(15, 29);
            this.lblSiteRoot.Name = "lblSiteRoot";
            this.lblSiteRoot.Size = new System.Drawing.Size(81, 13);
            this.lblSiteRoot.TabIndex = 5;
            this.lblSiteRoot.Text = "Racine du site :";
            // 
            // lstImageProcessing
            // 
            this.lstImageProcessing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstImageProcessing.FormattingEnabled = true;
            this.lstImageProcessing.Location = new System.Drawing.Point(178, 79);
            this.lstImageProcessing.Name = "lstImageProcessing";
            this.lstImageProcessing.Size = new System.Drawing.Size(322, 21);
            this.lstImageProcessing.TabIndex = 2;
            this.lstImageProcessing.SelectedIndexChanged += new System.EventHandler(this.lstImageProcessing_SelectedIndexChanged);
            // 
            // lblImageProcessing
            // 
            this.lblImageProcessing.AutoSize = true;
            this.lblImageProcessing.Location = new System.Drawing.Point(15, 82);
            this.lblImageProcessing.Name = "lblImageProcessing";
            this.lblImageProcessing.Size = new System.Drawing.Size(119, 13);
            this.lblImageProcessing.TabIndex = 8;
            this.lblImageProcessing.Text = "Traitement des images :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOutputfilename);
            this.groupBox1.Controls.Add(this.rdoOther);
            this.groupBox1.Controls.Add(this.rdoTitle);
            this.groupBox1.Controls.Add(this.rdoUrl);
            this.groupBox1.Location = new System.Drawing.Point(178, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 115);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nom du fichier de sortie";
            // 
            // txtOutputfilename
            // 
            this.txtOutputfilename.Enabled = false;
            this.txtOutputfilename.Location = new System.Drawing.Point(84, 77);
            this.txtOutputfilename.Name = "txtOutputfilename";
            this.txtOutputfilename.Size = new System.Drawing.Size(289, 20);
            this.txtOutputfilename.TabIndex = 3;
            // 
            // rdoOther
            // 
            this.rdoOther.AutoSize = true;
            this.rdoOther.Location = new System.Drawing.Point(26, 78);
            this.rdoOther.Name = "rdoOther";
            this.rdoOther.Size = new System.Drawing.Size(56, 17);
            this.rdoOther.TabIndex = 2;
            this.rdoOther.Text = "Autre :";
            this.rdoOther.UseVisualStyleBackColor = true;
            this.rdoOther.CheckedChanged += new System.EventHandler(this.rdoOther_CheckedChanged);
            // 
            // rdoTitle
            // 
            this.rdoTitle.AutoSize = true;
            this.rdoTitle.Checked = true;
            this.rdoTitle.Location = new System.Drawing.Point(26, 55);
            this.rdoTitle.Name = "rdoTitle";
            this.rdoTitle.Size = new System.Drawing.Size(145, 17);
            this.rdoTitle.TabIndex = 1;
            this.rdoTitle.TabStop = true;
            this.rdoTitle.Text = "D\'après le titre de la page";
            this.rdoTitle.UseVisualStyleBackColor = true;
            // 
            // rdoUrl
            // 
            this.rdoUrl.AutoSize = true;
            this.rdoUrl.Location = new System.Drawing.Point(26, 32);
            this.rdoUrl.Name = "rdoUrl";
            this.rdoUrl.Size = new System.Drawing.Size(158, 17);
            this.rdoUrl.TabIndex = 0;
            this.rdoUrl.Text = "D\'après l\'adresse de la page";
            this.rdoUrl.UseVisualStyleBackColor = true;
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Location = new System.Drawing.Point(178, 106);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(565, 20);
            this.txtOutputFolder.TabIndex = 4;
            this.txtOutputFolder.Text = "C:\\Temp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Dossier d\'enregistrement :";
            // 
            // btnBrowseOutputPath
            // 
            this.btnBrowseOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseOutputPath.Location = new System.Drawing.Point(749, 105);
            this.btnBrowseOutputPath.Name = "btnBrowseOutputPath";
            this.btnBrowseOutputPath.Size = new System.Drawing.Size(25, 22);
            this.btnBrowseOutputPath.TabIndex = 5;
            this.btnBrowseOutputPath.Text = "...";
            this.btnBrowseOutputPath.UseVisualStyleBackColor = true;
            this.btnBrowseOutputPath.Click += new System.EventHandler(this.btnBrowseOutputPath_Click);
            // 
            // lblStyleSheetProcessing
            // 
            this.lblStyleSheetProcessing.AutoSize = true;
            this.lblStyleSheetProcessing.Location = new System.Drawing.Point(15, 55);
            this.lblStyleSheetProcessing.Name = "lblStyleSheetProcessing";
            this.lblStyleSheetProcessing.Size = new System.Drawing.Size(157, 13);
            this.lblStyleSheetProcessing.TabIndex = 14;
            this.lblStyleSheetProcessing.Text = "Traitement des feuilles de style :";
            // 
            // lstStyleSheetProcessing
            // 
            this.lstStyleSheetProcessing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstStyleSheetProcessing.FormattingEnabled = true;
            this.lstStyleSheetProcessing.Location = new System.Drawing.Point(178, 52);
            this.lstStyleSheetProcessing.Name = "lstStyleSheetProcessing";
            this.lstStyleSheetProcessing.Size = new System.Drawing.Size(322, 21);
            this.lstStyleSheetProcessing.TabIndex = 1;
            // 
            // chkDowloadImagesToCommonFolder
            // 
            this.chkDowloadImagesToCommonFolder.AutoSize = true;
            this.chkDowloadImagesToCommonFolder.Location = new System.Drawing.Point(516, 81);
            this.chkDowloadImagesToCommonFolder.Name = "chkDowloadImagesToCommonFolder";
            this.chkDowloadImagesToCommonFolder.Size = new System.Drawing.Size(228, 17);
            this.chkDowloadImagesToCommonFolder.TabIndex = 3;
            this.chkDowloadImagesToCommonFolder.Text = "Placer les images dans un dossier commun";
            this.chkDowloadImagesToCommonFolder.UseVisualStyleBackColor = true;
            this.chkDowloadImagesToCommonFolder.Visible = false;
            // 
            // grpSettings
            // 
            this.grpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSettings.Controls.Add(this.lblSiteRoot);
            this.grpSettings.Controls.Add(this.chkDowloadImagesToCommonFolder);
            this.grpSettings.Controls.Add(this.txtSiteRoot);
            this.grpSettings.Controls.Add(this.lblStyleSheetProcessing);
            this.grpSettings.Controls.Add(this.lstStyleSheetProcessing);
            this.grpSettings.Controls.Add(this.lstImageProcessing);
            this.grpSettings.Controls.Add(this.btnBrowseOutputPath);
            this.grpSettings.Controls.Add(this.lblImageProcessing);
            this.grpSettings.Controls.Add(this.label1);
            this.grpSettings.Controls.Add(this.groupBox1);
            this.grpSettings.Controls.Add(this.txtOutputFolder);
            this.grpSettings.Location = new System.Drawing.Point(12, 318);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(780, 275);
            this.grpSettings.TabIndex = 2;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Paramètres";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(185, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(462, 29);
            this.label2.TabIndex = 17;
            this.label2.Text = "Archiveur phpBB2 pour Le Forum du N";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(187, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(581, 55);
            this.label3.TabIndex = 18;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // chkAutoDownload
            // 
            this.chkAutoDownload.AutoSize = true;
            this.chkAutoDownload.Location = new System.Drawing.Point(336, 210);
            this.chkAutoDownload.Name = "chkAutoDownload";
            this.chkAutoDownload.Size = new System.Drawing.Size(278, 17);
            this.chkAutoDownload.TabIndex = 19;
            this.chkAutoDownload.Text = "Télécharger automatiquement les adresses détectées";
            this.chkAutoDownload.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(190, 242);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(602, 23);
            this.progressBar1.TabIndex = 20;
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Location = new System.Drawing.Point(188, 268);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(604, 23);
            this.lblProgress.TabIndex = 21;
            this.lblProgress.Text = "-";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(761, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "v1.0";
            // 
            // frmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 605);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chkAutoDownload);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.lblURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archiveur phpBB2 pour Le Forum du N";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMaster_FormClosing);
            this.Load += new System.EventHandler(this.frmMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox txtSiteRoot;
        private System.Windows.Forms.Label lblSiteRoot;
        private System.Windows.Forms.ComboBox lstImageProcessing;
        private System.Windows.Forms.Label lblImageProcessing;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOutputfilename;
        private System.Windows.Forms.RadioButton rdoOther;
        private System.Windows.Forms.RadioButton rdoTitle;
        private System.Windows.Forms.RadioButton rdoUrl;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseOutputPath;
        private System.Windows.Forms.Label lblStyleSheetProcessing;
        private System.Windows.Forms.ComboBox lstStyleSheetProcessing;
        private System.Windows.Forms.CheckBox chkDowloadImagesToCommonFolder;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAutoDownload;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
    }
}

