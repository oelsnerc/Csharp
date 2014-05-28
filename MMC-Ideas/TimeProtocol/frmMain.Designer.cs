namespace TimeProtocol
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.ImageList imgList;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.Panel pnlBackground;
            System.Windows.Forms.ContextMenuStrip mnuMain;
            System.Windows.Forms.ToolStripMenuItem mnuFile;
            System.Windows.Forms.ToolStripMenuItem mnuFileNew;
            System.Windows.Forms.ToolStripMenuItem mnuOpen;
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.SystemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.ssdHours = new MMC_Controls.Seven_Segment_Display();
            this.ssdMinutes = new MMC_Controls.Seven_Segment_Display();
            this.ssdSeconds = new MMC_Controls.Seven_Segment_Display();
            label1 = new System.Windows.Forms.Label();
            imgList = new System.Windows.Forms.ImageList(this.components);
            pnlBackground = new System.Windows.Forms.Panel();
            mnuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            pnlBackground.SuspendLayout();
            mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Enabled = false;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(107, 38);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(16, 24);
            label1.TabIndex = 5;
            label1.Text = ":";
            // 
            // imgList
            // 
            imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            imgList.TransparentColor = System.Drawing.Color.Transparent;
            imgList.Images.SetKeyName(0, "Play");
            imgList.Images.SetKeyName(1, "Stop");
            imgList.Images.SetKeyName(2, "Kill");
            // 
            // pnlBackground
            // 
            pnlBackground.BackColor = System.Drawing.SystemColors.Control;
            pnlBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pnlBackground.ContextMenuStrip = mnuMain;
            pnlBackground.Controls.Add(this.btnClose);
            pnlBackground.Controls.Add(this.btnRun);
            pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlBackground.Location = new System.Drawing.Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.Padding = new System.Windows.Forms.Padding(4);
            pnlBackground.Size = new System.Drawing.Size(296, 110);
            pnlBackground.TabIndex = 12;
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 2;
            this.btnClose.ImageList = imgList;
            this.btnClose.Location = new System.Drawing.Point(256, 76);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRun
            // 
            this.btnRun.ImageIndex = 0;
            this.btnRun.ImageList = imgList;
            this.btnRun.Location = new System.Drawing.Point(220, 76);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(30, 23);
            this.btnRun.TabIndex = 1;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // dlgFileSave
            // 
            this.dlgFileSave.DefaultExt = "log";
            this.dlgFileSave.OverwritePrompt = false;
            this.dlgFileSave.Title = "Append Log to file";
            // 
            // tmrClock
            // 
            this.tmrClock.Interval = 1000;
            this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
            // 
            // SystemTray
            // 
            this.SystemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("SystemTray.Icon")));
            this.SystemTray.Text = "Time protocol";
            this.SystemTray.Visible = true;
            this.SystemTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SystemTray_MouseDoubleClick);
            // 
            // ssdHours
            // 
            this.ssdHours.Digits = 2;
            this.ssdHours.Enabled = false;
            this.ssdHours.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ssdHours.Location = new System.Drawing.Point(12, 12);
            this.ssdHours.Name = "ssdHours";
            this.ssdHours.Size = new System.Drawing.Size(89, 89);
            this.ssdHours.TabIndex = 4;
            this.ssdHours.Thickness = 6;
            // 
            // ssdMinutes
            // 
            this.ssdMinutes.Digits = 2;
            this.ssdMinutes.Enabled = false;
            this.ssdMinutes.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ssdMinutes.Location = new System.Drawing.Point(127, 12);
            this.ssdMinutes.Name = "ssdMinutes";
            this.ssdMinutes.Size = new System.Drawing.Size(89, 89);
            this.ssdMinutes.TabIndex = 3;
            this.ssdMinutes.Thickness = 6;
            // 
            // ssdSeconds
            // 
            this.ssdSeconds.Digits = 2;
            this.ssdSeconds.Enabled = false;
            this.ssdSeconds.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ssdSeconds.Location = new System.Drawing.Point(228, 12);
            this.ssdSeconds.Name = "ssdSeconds";
            this.ssdSeconds.Size = new System.Drawing.Size(50, 50);
            this.ssdSeconds.TabIndex = 2;
            this.ssdSeconds.Value = 34;
            // 
            // mnuMain
            // 
            mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            mnuFile});
            mnuMain.Name = "mnuMain";
            mnuMain.Size = new System.Drawing.Size(93, 26);
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            mnuFileNew,
            mnuOpen});
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new System.Drawing.Size(152, 22);
            mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            mnuFileNew.Name = "mnuFileNew";
            mnuFileNew.Size = new System.Drawing.Size(156, 22);
            mnuFileNew.Text = "New ...";
            mnuFileNew.Click += new System.EventHandler(this.mnuNewFile_Click);
            // 
            // mnuOpen
            // 
            mnuOpen.Name = "mnuOpen";
            mnuOpen.Size = new System.Drawing.Size(156, 22);
            mnuOpen.Text = "Open externally";
            mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 110);
            this.Controls.Add(label1);
            this.Controls.Add(this.ssdHours);
            this.Controls.Add(this.ssdMinutes);
            this.Controls.Add(this.ssdSeconds);
            this.Controls.Add(pnlBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Time Protocol";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.DoubleClick += new System.EventHandler(this.frmMain_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseMove);
            pnlBackground.ResumeLayout(false);
            mnuMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.SaveFileDialog dlgFileSave;
        private MMC_Controls.Seven_Segment_Display ssdSeconds;
        private MMC_Controls.Seven_Segment_Display ssdMinutes;
        private MMC_Controls.Seven_Segment_Display ssdHours;
        private System.Windows.Forms.Timer tmrClock;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.NotifyIcon SystemTray;
    }
}

