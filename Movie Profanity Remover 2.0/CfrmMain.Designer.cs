namespace Movie_Profanity_Remover_2._0
{
    partial class CfrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CfrmMain));
            this.lblClose = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblMinimize = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpVideoSettings = new System.Windows.Forms.GroupBox();
            this.txtCustomAffix = new System.Windows.Forms.TextBox();
            this.chkAspectRatio = new System.Windows.Forms.CheckBox();
            this.cmbOutputType = new System.Windows.Forms.ComboBox();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.lblX = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.lnkMoviesubtitles = new System.Windows.Forms.LinkLabel();
            this.lnkOpensubtitles = new System.Windows.Forms.LinkLabel();
            this.lnkSubscene = new System.Windows.Forms.LinkLabel();
            this.lblSelectSubtitles = new System.Windows.Forms.Label();
            this.txtSubtitles = new System.Windows.Forms.TextBox();
            this.lblSelectVideo = new System.Windows.Forms.Label();
            this.btnSelectSubtitle = new System.Windows.Forms.Button();
            this.txtVideo = new System.Windows.Forms.TextBox();
            this.btnSelectVideo = new System.Windows.Forms.Button();
            this.lvVideos = new System.Windows.Forms.ListView();
            this.cDummy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cVideos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkCreateSubtitles = new System.Windows.Forms.CheckBox();
            this.btnPeekSwearCount = new System.Windows.Forms.Button();
            this.btnAddToQueue = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.grpFilterOptions = new System.Windows.Forms.GroupBox();
            this.lblAfter = new System.Windows.Forms.Label();
            this.lblMS2 = new System.Windows.Forms.Label();
            this.numSingleWordAfterMS = new System.Windows.Forms.NumericUpDown();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBefore = new System.Windows.Forms.Label();
            this.lblMS1 = new System.Windows.Forms.Label();
            this.numSingleWordBeforeMS = new System.Windows.Forms.NumericUpDown();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.tTip = new System.Windows.Forms.ToolTip(this.components);
            this.chkEmbedSubtitles = new System.Windows.Forms.CheckBox();
            this.radNormal = new System.Windows.Forms.RadioButton();
            this.radExclusive = new System.Windows.Forms.RadioButton();
            this.radBoth = new System.Windows.Forms.RadioButton();
            this.chkDeleteOriginalFiles = new System.Windows.Forms.CheckBox();
            this.chkMuteVocalsOnly = new System.Windows.Forms.CheckBox();
            this.dlgOpenSubtitle = new System.Windows.Forms.OpenFileDialog();
            this.dlgOpenVideo = new System.Windows.Forms.OpenFileDialog();
            this.prgCurrent = new System.Windows.Forms.ProgressBar();
            this.prgOverall = new System.Windows.Forms.ProgressBar();
            this.grpSubtitlesSettings = new System.Windows.Forms.GroupBox();
            this.grpOtherSettings = new System.Windows.Forms.GroupBox();
            this.pnlTitle.SuspendLayout();
            this.grpVideoSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            this.grpFilterOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSingleWordAfterMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSingleWordBeforeMS)).BeginInit();
            this.grpSubtitlesSettings.SuspendLayout();
            this.grpOtherSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblClose
            // 
            this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Location = new System.Drawing.Point(1007, 1);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(20, 20);
            this.lblClose.TabIndex = 0;
            this.lblClose.Text = "X";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClose.Click += new System.EventHandler(this.LblClose_Click);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlTitle.Controls.Add(this.lblMinimize);
            this.pnlTitle.Controls.Add(this.lblClose);
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1027, 24);
            this.pnlTitle.TabIndex = 1;
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PnlTitle_MouseDown);
            // 
            // lblMinimize
            // 
            this.lblMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMinimize.Location = new System.Drawing.Point(989, -1);
            this.lblMinimize.Name = "lblMinimize";
            this.lblMinimize.Size = new System.Drawing.Size(20, 20);
            this.lblMinimize.TabIndex = 0;
            this.lblMinimize.Text = "__";
            this.lblMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMinimize.Click += new System.EventHandler(this.LblMinimize_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(425, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(176, 17);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Movie Profanity Remover 2.0";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PnlTitle_MouseDown);
            // 
            // grpVideoSettings
            // 
            this.grpVideoSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grpVideoSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grpVideoSettings.Controls.Add(this.txtCustomAffix);
            this.grpVideoSettings.Controls.Add(this.chkAspectRatio);
            this.grpVideoSettings.Controls.Add(this.cmbOutputType);
            this.grpVideoSettings.Controls.Add(this.numHeight);
            this.grpVideoSettings.Controls.Add(this.numWidth);
            this.grpVideoSettings.Controls.Add(this.lblX);
            this.grpVideoSettings.Controls.Add(this.label2);
            this.grpVideoSettings.Controls.Add(this.lblOutput);
            this.grpVideoSettings.Location = new System.Drawing.Point(12, 430);
            this.grpVideoSettings.Name = "grpVideoSettings";
            this.grpVideoSettings.Size = new System.Drawing.Size(498, 92);
            this.grpVideoSettings.TabIndex = 6;
            this.grpVideoSettings.TabStop = false;
            this.grpVideoSettings.Text = "Video Settings";
            // 
            // txtCustomAffix
            // 
            this.txtCustomAffix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCustomAffix.Location = new System.Drawing.Point(352, 56);
            this.txtCustomAffix.Name = "txtCustomAffix";
            this.txtCustomAffix.Size = new System.Drawing.Size(127, 25);
            this.txtCustomAffix.TabIndex = 11;
            this.txtCustomAffix.Text = "_SL";
            this.txtCustomAffix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tTip.SetToolTip(this.txtCustomAffix, "Text to append to the back of the output file name");
            this.txtCustomAffix.TextChanged += new System.EventHandler(this.TxtCustomAffix_TextChanged);
            // 
            // chkAspectRatio
            // 
            this.chkAspectRatio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAspectRatio.AutoSize = true;
            this.chkAspectRatio.Location = new System.Drawing.Point(17, 41);
            this.chkAspectRatio.Name = "chkAspectRatio";
            this.chkAspectRatio.Size = new System.Drawing.Size(103, 21);
            this.chkAspectRatio.TabIndex = 7;
            this.chkAspectRatio.Text = "Aspect Ratio:";
            this.tTip.SetToolTip(this.chkAspectRatio, "Enable/disable aspect ratio change");
            this.chkAspectRatio.UseVisualStyleBackColor = true;
            this.chkAspectRatio.CheckedChanged += new System.EventHandler(this.ChkAspectRatio_CheckedChanged);
            // 
            // cmbOutputType
            // 
            this.cmbOutputType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbOutputType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputType.ForeColor = System.Drawing.Color.White;
            this.cmbOutputType.FormattingEnabled = true;
            this.cmbOutputType.Items.AddRange(new object[] {
            ".mkv",
            ".mp4"});
            this.cmbOutputType.Location = new System.Drawing.Point(352, 20);
            this.cmbOutputType.Name = "cmbOutputType";
            this.cmbOutputType.Size = new System.Drawing.Size(127, 25);
            this.cmbOutputType.TabIndex = 10;
            this.tTip.SetToolTip(this.cmbOutputType, "Output format");
            this.cmbOutputType.SelectedIndexChanged += new System.EventHandler(this.CmbOutputType_SelectedIndexChanged);
            // 
            // numHeight
            // 
            this.numHeight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numHeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numHeight.Enabled = false;
            this.numHeight.ForeColor = System.Drawing.Color.White;
            this.numHeight.Location = new System.Drawing.Point(194, 39);
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(49, 25);
            this.numHeight.TabIndex = 9;
            this.numHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tTip.SetToolTip(this.numHeight, "Height");
            this.numHeight.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numHeight.ValueChanged += new System.EventHandler(this.NumAspectRatio_ValueChanged);
            // 
            // numWidth
            // 
            this.numWidth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numWidth.Enabled = false;
            this.numWidth.ForeColor = System.Drawing.Color.White;
            this.numWidth.Location = new System.Drawing.Point(123, 39);
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(49, 25);
            this.numWidth.TabIndex = 8;
            this.numWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tTip.SetToolTip(this.numWidth, "Width");
            this.numWidth.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numWidth.ValueChanged += new System.EventHandler(this.NumAspectRatio_ValueChanged);
            // 
            // lblX
            // 
            this.lblX.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblX.AutoSize = true;
            this.lblX.Enabled = false;
            this.lblX.Location = new System.Drawing.Point(176, 41);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(16, 17);
            this.lblX.TabIndex = 0;
            this.lblX.Text = "X";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Custom Affix:";
            // 
            // lblOutput
            // 
            this.lblOutput.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(264, 23);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(82, 17);
            this.lblOutput.TabIndex = 0;
            this.lblOutput.Text = "Output Type:";
            // 
            // lnkMoviesubtitles
            // 
            this.lnkMoviesubtitles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkMoviesubtitles.AutoSize = true;
            this.lnkMoviesubtitles.LinkColor = System.Drawing.Color.Silver;
            this.lnkMoviesubtitles.Location = new System.Drawing.Point(878, 158);
            this.lnkMoviesubtitles.Name = "lnkMoviesubtitles";
            this.lnkMoviesubtitles.Size = new System.Drawing.Size(137, 17);
            this.lnkMoviesubtitles.TabIndex = 4;
            this.lnkMoviesubtitles.TabStop = true;
            this.lnkMoviesubtitles.Text = "MOVIESUBTITLES.ORG";
            this.tTip.SetToolTip(this.lnkMoviesubtitles, "Navigate to moviesubtitles.org");
            this.lnkMoviesubtitles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMoviesubtitles_LinkClicked);
            // 
            // lnkOpensubtitles
            // 
            this.lnkOpensubtitles.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lnkOpensubtitles.AutoSize = true;
            this.lnkOpensubtitles.LinkColor = System.Drawing.Color.Silver;
            this.lnkOpensubtitles.Location = new System.Drawing.Point(446, 158);
            this.lnkOpensubtitles.Name = "lnkOpensubtitles";
            this.lnkOpensubtitles.Size = new System.Drawing.Size(134, 17);
            this.lnkOpensubtitles.TabIndex = 3;
            this.lnkOpensubtitles.TabStop = true;
            this.lnkOpensubtitles.Text = "OPENSUBTITLES.COM";
            this.tTip.SetToolTip(this.lnkOpensubtitles, "Navigate to opensubtitles.com");
            this.lnkOpensubtitles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOpensubtitles_LinkClicked);
            // 
            // lnkSubscene
            // 
            this.lnkSubscene.AutoSize = true;
            this.lnkSubscene.LinkColor = System.Drawing.Color.Silver;
            this.lnkSubscene.Location = new System.Drawing.Point(12, 158);
            this.lnkSubscene.Name = "lnkSubscene";
            this.lnkSubscene.Size = new System.Drawing.Size(103, 17);
            this.lnkSubscene.TabIndex = 2;
            this.lnkSubscene.TabStop = true;
            this.lnkSubscene.Text = "SUBSCENE.COM";
            this.tTip.SetToolTip(this.lnkSubscene, "Navigate to subscene.com");
            this.lnkSubscene.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSubscene_LinkClicked);
            // 
            // lblSelectSubtitles
            // 
            this.lblSelectSubtitles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectSubtitles.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectSubtitles.Location = new System.Drawing.Point(12, 100);
            this.lblSelectSubtitles.Name = "lblSelectSubtitles";
            this.lblSelectSubtitles.Size = new System.Drawing.Size(1003, 23);
            this.lblSelectSubtitles.TabIndex = 7;
            this.lblSelectSubtitles.Text = "Subtitles:";
            this.lblSelectSubtitles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSubtitles
            // 
            this.txtSubtitles.AllowDrop = true;
            this.txtSubtitles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubtitles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSubtitles.ForeColor = System.Drawing.Color.White;
            this.txtSubtitles.Location = new System.Drawing.Point(12, 130);
            this.txtSubtitles.Name = "txtSubtitles";
            this.txtSubtitles.ReadOnly = true;
            this.txtSubtitles.Size = new System.Drawing.Size(973, 25);
            this.txtSubtitles.TabIndex = 0;
            this.txtSubtitles.TabStop = false;
            this.txtSubtitles.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubtitles.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtSubtitle_DragDrop);
            this.txtSubtitles.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // lblSelectVideo
            // 
            this.lblSelectVideo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectVideo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectVideo.Location = new System.Drawing.Point(12, 39);
            this.lblSelectVideo.Name = "lblSelectVideo";
            this.lblSelectVideo.Size = new System.Drawing.Size(1003, 23);
            this.lblSelectVideo.TabIndex = 8;
            this.lblSelectVideo.Text = "Video:";
            this.lblSelectVideo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSelectSubtitle
            // 
            this.btnSelectSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSelectSubtitle.ForeColor = System.Drawing.Color.White;
            this.btnSelectSubtitle.Location = new System.Drawing.Point(986, 129);
            this.btnSelectSubtitle.Name = "btnSelectSubtitle";
            this.btnSelectSubtitle.Size = new System.Drawing.Size(29, 27);
            this.btnSelectSubtitle.TabIndex = 1;
            this.btnSelectSubtitle.Text = "...";
            this.tTip.SetToolTip(this.btnSelectSubtitle, "Select subtitles");
            this.btnSelectSubtitle.UseVisualStyleBackColor = false;
            this.btnSelectSubtitle.Click += new System.EventHandler(this.btnSelectSubtitle_Click);
            // 
            // txtVideo
            // 
            this.txtVideo.AllowDrop = true;
            this.txtVideo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVideo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtVideo.ForeColor = System.Drawing.Color.White;
            this.txtVideo.Location = new System.Drawing.Point(12, 69);
            this.txtVideo.Name = "txtVideo";
            this.txtVideo.ReadOnly = true;
            this.txtVideo.Size = new System.Drawing.Size(973, 25);
            this.txtVideo.TabIndex = 0;
            this.txtVideo.TabStop = false;
            this.txtVideo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVideo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtVideo_DragDrop);
            this.txtVideo.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // btnSelectVideo
            // 
            this.btnSelectVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectVideo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSelectVideo.ForeColor = System.Drawing.Color.White;
            this.btnSelectVideo.Location = new System.Drawing.Point(986, 68);
            this.btnSelectVideo.Name = "btnSelectVideo";
            this.btnSelectVideo.Size = new System.Drawing.Size(29, 27);
            this.btnSelectVideo.TabIndex = 0;
            this.btnSelectVideo.Text = "...";
            this.tTip.SetToolTip(this.btnSelectVideo, "Select video");
            this.btnSelectVideo.UseVisualStyleBackColor = false;
            this.btnSelectVideo.Click += new System.EventHandler(this.btnSelectVideo_Click);
            // 
            // lvVideos
            // 
            this.lvVideos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvVideos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lvVideos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cDummy,
            this.cVideos});
            this.lvVideos.ForeColor = System.Drawing.Color.White;
            this.lvVideos.FullRowSelect = true;
            this.lvVideos.HideSelection = false;
            this.lvVideos.Location = new System.Drawing.Point(12, 190);
            this.lvVideos.Name = "lvVideos";
            this.lvVideos.Size = new System.Drawing.Size(1003, 234);
            this.lvVideos.TabIndex = 5;
            this.lvVideos.UseCompatibleStateImageBehavior = false;
            this.lvVideos.View = System.Windows.Forms.View.Details;
            this.lvVideos.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.LvVideos_ColumnWidthChanging);
            this.lvVideos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LvVideos_KeyDown);
            this.lvVideos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LvVideos_MouseUp);
            // 
            // cDummy
            // 
            this.cDummy.Text = "";
            this.cDummy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cDummy.Width = 0;
            // 
            // cVideos
            // 
            this.cVideos.Text = "Videos";
            this.cVideos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cVideos.Width = 998;
            // 
            // chkCreateSubtitles
            // 
            this.chkCreateSubtitles.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkCreateSubtitles.AutoSize = true;
            this.chkCreateSubtitles.Checked = true;
            this.chkCreateSubtitles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateSubtitles.Location = new System.Drawing.Point(54, 41);
            this.chkCreateSubtitles.Name = "chkCreateSubtitles";
            this.chkCreateSubtitles.Size = new System.Drawing.Size(118, 21);
            this.chkCreateSubtitles.TabIndex = 17;
            this.chkCreateSubtitles.Text = "Create Subtitles";
            this.tTip.SetToolTip(this.chkCreateSubtitles, "Create new subtitles");
            this.chkCreateSubtitles.UseVisualStyleBackColor = true;
            this.chkCreateSubtitles.CheckedChanged += new System.EventHandler(this.ChkCreateSubtitles_CheckedChanged);
            // 
            // btnPeekSwearCount
            // 
            this.btnPeekSwearCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPeekSwearCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPeekSwearCount.Location = new System.Drawing.Point(10, 621);
            this.btnPeekSwearCount.Name = "btnPeekSwearCount";
            this.btnPeekSwearCount.Size = new System.Drawing.Size(82, 31);
            this.btnPeekSwearCount.TabIndex = 24;
            this.btnPeekSwearCount.Text = "Peek";
            this.tTip.SetToolTip(this.btnPeekSwearCount, "Display swear words (from file currently in subtitles text box)");
            this.btnPeekSwearCount.UseVisualStyleBackColor = false;
            this.btnPeekSwearCount.Click += new System.EventHandler(this.btnPeekSwearCount_Click);
            // 
            // btnAddToQueue
            // 
            this.btnAddToQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToQueue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddToQueue.Location = new System.Drawing.Point(846, 621);
            this.btnAddToQueue.Name = "btnAddToQueue";
            this.btnAddToQueue.Size = new System.Drawing.Size(82, 31);
            this.btnAddToQueue.TabIndex = 25;
            this.btnAddToQueue.Text = "Add";
            this.tTip.SetToolTip(this.btnAddToQueue, "Add specified video and subtitles to queue");
            this.btnAddToQueue.UseVisualStyleBackColor = false;
            this.btnAddToQueue.Click += new System.EventHandler(this.btnAddToQueue_Click);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGo.Location = new System.Drawing.Point(934, 621);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(82, 31);
            this.btnGo.TabIndex = 26;
            this.btnGo.Text = "Go";
            this.tTip.SetToolTip(this.btnGo, "Left click - Create videos and subtitles\r\nRight click - Create subtitles only");
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnGo_MouseUp);
            // 
            // grpFilterOptions
            // 
            this.grpFilterOptions.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grpFilterOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grpFilterOptions.Controls.Add(this.lblAfter);
            this.grpFilterOptions.Controls.Add(this.lblMS2);
            this.grpFilterOptions.Controls.Add(this.numSingleWordAfterMS);
            this.grpFilterOptions.Controls.Add(this.btnFilter);
            this.grpFilterOptions.Controls.Add(this.label1);
            this.grpFilterOptions.Controls.Add(this.lblBefore);
            this.grpFilterOptions.Controls.Add(this.lblMS1);
            this.grpFilterOptions.Controls.Add(this.numSingleWordBeforeMS);
            this.grpFilterOptions.Location = new System.Drawing.Point(517, 430);
            this.grpFilterOptions.Name = "grpFilterOptions";
            this.grpFilterOptions.Size = new System.Drawing.Size(498, 92);
            this.grpFilterOptions.TabIndex = 12;
            this.grpFilterOptions.TabStop = false;
            this.grpFilterOptions.Text = "Filter Options";
            // 
            // lblAfter
            // 
            this.lblAfter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAfter.AutoSize = true;
            this.lblAfter.Location = new System.Drawing.Point(305, 56);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(39, 17);
            this.lblAfter.TabIndex = 0;
            this.lblAfter.Text = "After:";
            // 
            // lblMS2
            // 
            this.lblMS2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMS2.AutoSize = true;
            this.lblMS2.Location = new System.Drawing.Point(449, 56);
            this.lblMS2.Name = "lblMS2";
            this.lblMS2.Size = new System.Drawing.Size(35, 17);
            this.lblMS2.TabIndex = 0;
            this.lblMS2.Text = "(MS)";
            // 
            // numSingleWordAfterMS
            // 
            this.numSingleWordAfterMS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numSingleWordAfterMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numSingleWordAfterMS.ForeColor = System.Drawing.Color.White;
            this.numSingleWordAfterMS.Location = new System.Drawing.Point(354, 54);
            this.numSingleWordAfterMS.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numSingleWordAfterMS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSingleWordAfterMS.Name = "numSingleWordAfterMS";
            this.numSingleWordAfterMS.Size = new System.Drawing.Size(89, 25);
            this.numSingleWordAfterMS.TabIndex = 15;
            this.numSingleWordAfterMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tTip.SetToolTip(this.numSingleWordAfterMS, "Milliseconds to mute after filtered words");
            this.numSingleWordAfterMS.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numSingleWordAfterMS.ValueChanged += new System.EventHandler(this.NumSingleWordMS_ValueChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilter.Location = new System.Drawing.Point(28, 34);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(104, 31);
            this.btnFilter.TabIndex = 13;
            this.btnFilter.Text = "Filter";
            this.tTip.SetToolTip(this.btnFilter, "Add/remove filtered words");
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.BtnFilter_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Single word removal:";
            this.tTip.SetToolTip(this.label1, "Acurracy based on subtitle file");
            // 
            // lblBefore
            // 
            this.lblBefore.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBefore.AutoSize = true;
            this.lblBefore.Location = new System.Drawing.Point(300, 25);
            this.lblBefore.Name = "lblBefore";
            this.lblBefore.Size = new System.Drawing.Size(49, 17);
            this.lblBefore.TabIndex = 0;
            this.lblBefore.Text = "Before:";
            // 
            // lblMS1
            // 
            this.lblMS1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMS1.AutoSize = true;
            this.lblMS1.Location = new System.Drawing.Point(449, 25);
            this.lblMS1.Name = "lblMS1";
            this.lblMS1.Size = new System.Drawing.Size(35, 17);
            this.lblMS1.TabIndex = 0;
            this.lblMS1.Text = "(MS)";
            // 
            // numSingleWordBeforeMS
            // 
            this.numSingleWordBeforeMS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numSingleWordBeforeMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numSingleWordBeforeMS.ForeColor = System.Drawing.Color.White;
            this.numSingleWordBeforeMS.Location = new System.Drawing.Point(354, 23);
            this.numSingleWordBeforeMS.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numSingleWordBeforeMS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSingleWordBeforeMS.Name = "numSingleWordBeforeMS";
            this.numSingleWordBeforeMS.Size = new System.Drawing.Size(89, 25);
            this.numSingleWordBeforeMS.TabIndex = 14;
            this.numSingleWordBeforeMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tTip.SetToolTip(this.numSingleWordBeforeMS, "Milliseconds to mute before filtered words");
            this.numSingleWordBeforeMS.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numSingleWordBeforeMS.ValueChanged += new System.EventHandler(this.NumSingleWordMS_ValueChanged);
            // 
            // pnlLeft
            // 
            this.pnlLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(2, 664);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlRight.Location = new System.Drawing.Point(1025, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(2, 664);
            this.pnlRight.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlBottom.Location = new System.Drawing.Point(0, 662);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1027, 2);
            this.pnlBottom.TabIndex = 1;
            // 
            // tTip
            // 
            this.tTip.AutoPopDelay = 10000;
            this.tTip.InitialDelay = 500;
            this.tTip.ReshowDelay = 100;
            // 
            // chkEmbedSubtitles
            // 
            this.chkEmbedSubtitles.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEmbedSubtitles.AutoSize = true;
            this.chkEmbedSubtitles.Checked = true;
            this.chkEmbedSubtitles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEmbedSubtitles.Location = new System.Drawing.Point(316, 41);
            this.chkEmbedSubtitles.Name = "chkEmbedSubtitles";
            this.chkEmbedSubtitles.Size = new System.Drawing.Size(121, 21);
            this.chkEmbedSubtitles.TabIndex = 21;
            this.chkEmbedSubtitles.Text = "Embed Subtitles";
            this.tTip.SetToolTip(this.chkEmbedSubtitles, "Embed subtitles into movie");
            this.chkEmbedSubtitles.UseVisualStyleBackColor = true;
            this.chkEmbedSubtitles.CheckedChanged += new System.EventHandler(this.ChkEmbedSubtitles_CheckedChanged);
            // 
            // radNormal
            // 
            this.radNormal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radNormal.AutoSize = true;
            this.radNormal.Checked = true;
            this.radNormal.Location = new System.Drawing.Point(203, 22);
            this.radNormal.Name = "radNormal";
            this.radNormal.Size = new System.Drawing.Size(70, 21);
            this.radNormal.TabIndex = 18;
            this.radNormal.TabStop = true;
            this.radNormal.Text = "Normal";
            this.tTip.SetToolTip(this.radNormal, "Create normal subtitles (swear words hidden)");
            this.radNormal.UseVisualStyleBackColor = true;
            this.radNormal.CheckedChanged += new System.EventHandler(this.RadNormal_CheckedChanged);
            // 
            // radExclusive
            // 
            this.radExclusive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radExclusive.AutoSize = true;
            this.radExclusive.Location = new System.Drawing.Point(203, 40);
            this.radExclusive.Name = "radExclusive";
            this.radExclusive.Size = new System.Drawing.Size(77, 21);
            this.radExclusive.TabIndex = 19;
            this.radExclusive.Text = "Exclusive";
            this.tTip.SetToolTip(this.radExclusive, "Create subtitles showing only lines with swear words (swear words hidden)");
            this.radExclusive.UseVisualStyleBackColor = true;
            this.radExclusive.CheckedChanged += new System.EventHandler(this.RadExclusive_CheckedChanged);
            // 
            // radBoth
            // 
            this.radBoth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radBoth.AutoSize = true;
            this.radBoth.Location = new System.Drawing.Point(203, 58);
            this.radBoth.Name = "radBoth";
            this.radBoth.Size = new System.Drawing.Size(52, 21);
            this.radBoth.TabIndex = 20;
            this.radBoth.Text = "Both";
            this.tTip.SetToolTip(this.radBoth, "Create both types of subtitles");
            this.radBoth.UseVisualStyleBackColor = true;
            this.radBoth.CheckedChanged += new System.EventHandler(this.RadBoth_CheckedChanged);
            // 
            // chkDeleteOriginalFiles
            // 
            this.chkDeleteOriginalFiles.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDeleteOriginalFiles.AutoSize = true;
            this.chkDeleteOriginalFiles.Location = new System.Drawing.Point(69, 41);
            this.chkDeleteOriginalFiles.Name = "chkDeleteOriginalFiles";
            this.chkDeleteOriginalFiles.Size = new System.Drawing.Size(143, 21);
            this.chkDeleteOriginalFiles.TabIndex = 23;
            this.chkDeleteOriginalFiles.Text = "Delete Original Files";
            this.tTip.SetToolTip(this.chkDeleteOriginalFiles, "Delete original movie & subtitle file");
            this.chkDeleteOriginalFiles.UseVisualStyleBackColor = true;
            this.chkDeleteOriginalFiles.CheckedChanged += new System.EventHandler(this.ChkDeleteOriginalFiles_CheckedChanged);
            // 
            // chkMuteVocalsOnly
            // 
            this.chkMuteVocalsOnly.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkMuteVocalsOnly.AutoSize = true;
            this.chkMuteVocalsOnly.Location = new System.Drawing.Point(278, 41);
            this.chkMuteVocalsOnly.Name = "chkMuteVocalsOnly";
            this.chkMuteVocalsOnly.Size = new System.Drawing.Size(165, 21);
            this.chkMuteVocalsOnly.TabIndex = 23;
            this.chkMuteVocalsOnly.Text = "Mute Vocals Only (Beta)";
            this.tTip.SetToolTip(this.chkMuteVocalsOnly, "Mutes FC (Front Center), SL (Surround Left) and SR (Surround Right) Channels\r\nRec" +
        "ommended: 5.1 Surround Sound");
            this.chkMuteVocalsOnly.UseVisualStyleBackColor = true;
            this.chkMuteVocalsOnly.CheckedChanged += new System.EventHandler(this.ChkMuteVocalsOnly_CheckedChanged);
            // 
            // dlgOpenSubtitle
            // 
            this.dlgOpenSubtitle.Filter = "Subtitle Files (*.srt)|*.srt";
            // 
            // dlgOpenVideo
            // 
            this.dlgOpenVideo.Filter = "Video Files (*.mkv;*.mp4;*.avi)|*.mkv;*.mp4;*.avi";
            // 
            // prgCurrent
            // 
            this.prgCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgCurrent.Location = new System.Drawing.Point(98, 622);
            this.prgCurrent.Maximum = 1000;
            this.prgCurrent.Name = "prgCurrent";
            this.prgCurrent.Size = new System.Drawing.Size(830, 14);
            this.prgCurrent.TabIndex = 0;
            this.prgCurrent.Visible = false;
            // 
            // prgOverall
            // 
            this.prgOverall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgOverall.Location = new System.Drawing.Point(98, 637);
            this.prgOverall.Maximum = 1000;
            this.prgOverall.Name = "prgOverall";
            this.prgOverall.Size = new System.Drawing.Size(830, 14);
            this.prgOverall.TabIndex = 0;
            this.prgOverall.Visible = false;
            // 
            // grpSubtitlesSettings
            // 
            this.grpSubtitlesSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grpSubtitlesSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grpSubtitlesSettings.Controls.Add(this.radBoth);
            this.grpSubtitlesSettings.Controls.Add(this.radExclusive);
            this.grpSubtitlesSettings.Controls.Add(this.radNormal);
            this.grpSubtitlesSettings.Controls.Add(this.chkCreateSubtitles);
            this.grpSubtitlesSettings.Controls.Add(this.chkEmbedSubtitles);
            this.grpSubtitlesSettings.Location = new System.Drawing.Point(12, 522);
            this.grpSubtitlesSettings.Name = "grpSubtitlesSettings";
            this.grpSubtitlesSettings.Size = new System.Drawing.Size(498, 92);
            this.grpSubtitlesSettings.TabIndex = 16;
            this.grpSubtitlesSettings.TabStop = false;
            this.grpSubtitlesSettings.Text = "Subtitles Settings";
            // 
            // grpOtherSettings
            // 
            this.grpOtherSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grpOtherSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grpOtherSettings.Controls.Add(this.chkMuteVocalsOnly);
            this.grpOtherSettings.Controls.Add(this.chkDeleteOriginalFiles);
            this.grpOtherSettings.Location = new System.Drawing.Point(517, 522);
            this.grpOtherSettings.Name = "grpOtherSettings";
            this.grpOtherSettings.Size = new System.Drawing.Size(498, 92);
            this.grpOtherSettings.TabIndex = 22;
            this.grpOtherSettings.TabStop = false;
            this.grpOtherSettings.Text = "Other Settings";
            // 
            // CfrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1027, 664);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.btnPeekSwearCount);
            this.Controls.Add(this.btnAddToQueue);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lvVideos);
            this.Controls.Add(this.lnkMoviesubtitles);
            this.Controls.Add(this.lnkOpensubtitles);
            this.Controls.Add(this.lnkSubscene);
            this.Controls.Add(this.lblSelectSubtitles);
            this.Controls.Add(this.txtSubtitles);
            this.Controls.Add(this.lblSelectVideo);
            this.Controls.Add(this.btnSelectSubtitle);
            this.Controls.Add(this.txtVideo);
            this.Controls.Add(this.btnSelectVideo);
            this.Controls.Add(this.grpFilterOptions);
            this.Controls.Add(this.grpOtherSettings);
            this.Controls.Add(this.grpSubtitlesSettings);
            this.Controls.Add(this.grpVideoSettings);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.prgOverall);
            this.Controls.Add(this.prgCurrent);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "CfrmMain";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movie Profanity Remover 2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CfrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.CfrmMain_Shown);
            this.pnlTitle.ResumeLayout(false);
            this.grpVideoSettings.ResumeLayout(false);
            this.grpVideoSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            this.grpFilterOptions.ResumeLayout(false);
            this.grpFilterOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSingleWordAfterMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSingleWordBeforeMS)).EndInit();
            this.grpSubtitlesSettings.ResumeLayout(false);
            this.grpSubtitlesSettings.PerformLayout();
            this.grpOtherSettings.ResumeLayout(false);
            this.grpOtherSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblMinimize;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpVideoSettings;
        private System.Windows.Forms.CheckBox chkAspectRatio;
        private System.Windows.Forms.ComboBox cmbOutputType;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.LinkLabel lnkMoviesubtitles;
        private System.Windows.Forms.LinkLabel lnkOpensubtitles;
        private System.Windows.Forms.LinkLabel lnkSubscene;
        private System.Windows.Forms.Label lblSelectSubtitles;
        private System.Windows.Forms.TextBox txtSubtitles;
        private System.Windows.Forms.Label lblSelectVideo;
        private System.Windows.Forms.Button btnSelectSubtitle;
        private System.Windows.Forms.TextBox txtVideo;
        private System.Windows.Forms.Button btnSelectVideo;
        private System.Windows.Forms.ListView lvVideos;
        private System.Windows.Forms.ColumnHeader cDummy;
        private System.Windows.Forms.ColumnHeader cVideos;
        private System.Windows.Forms.CheckBox chkCreateSubtitles;
        private System.Windows.Forms.Button btnPeekSwearCount;
        private System.Windows.Forms.Button btnAddToQueue;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.GroupBox grpFilterOptions;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblAfter;
        private System.Windows.Forms.Label lblMS2;
        private System.Windows.Forms.NumericUpDown numSingleWordAfterMS;
        private System.Windows.Forms.Label lblBefore;
        private System.Windows.Forms.Label lblMS1;
        private System.Windows.Forms.NumericUpDown numSingleWordBeforeMS;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.ToolTip tTip;
        private System.Windows.Forms.OpenFileDialog dlgOpenSubtitle;
        private System.Windows.Forms.OpenFileDialog dlgOpenVideo;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar prgCurrent;
        private System.Windows.Forms.ProgressBar prgOverall;
        private System.Windows.Forms.TextBox txtCustomAffix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEmbedSubtitles;
        private System.Windows.Forms.GroupBox grpSubtitlesSettings;
        private System.Windows.Forms.RadioButton radBoth;
        private System.Windows.Forms.RadioButton radExclusive;
        private System.Windows.Forms.RadioButton radNormal;
        private System.Windows.Forms.GroupBox grpOtherSettings;
        private System.Windows.Forms.CheckBox chkDeleteOriginalFiles;
        private System.Windows.Forms.CheckBox chkMuteVocalsOnly;
    }
}

