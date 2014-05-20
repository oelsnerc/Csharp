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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.ImageList imgList;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.Panel pnlBackground;
            this.txtProtocol = new System.Windows.Forms.RichTextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.btnHide = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SystemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.ssdLog_Seconds = new MMC_Controls.Seven_Segment_Display();
            this.ssdLog_Minutes = new MMC_Controls.Seven_Segment_Display();
            this.ssdLog_Hours = new MMC_Controls.Seven_Segment_Display();
            this.ssdHours = new MMC_Controls.Seven_Segment_Display();
            this.ssdMinutes = new MMC_Controls.Seven_Segment_Display();
            this.ssdSeconds = new MMC_Controls.Seven_Segment_Display();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            imgList = new System.Windows.Forms.ImageList(this.components);
            pnlBackground = new System.Windows.Forms.Panel();
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Enabled = false;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(363, 34);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(12, 16);
            label2.TabIndex = 8;
            label2.Text = ":";
            // 
            // txtProtocol
            // 
            this.txtProtocol.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProtocol.Location = new System.Drawing.Point(12, 114);
            this.txtProtocol.Name = "txtProtocol";
            this.txtProtocol.Size = new System.Drawing.Size(455, 347);
            this.txtProtocol.TabIndex = 0;
            this.txtProtocol.Text = "";
            // 
            // btnRun
            // 
            this.btnRun.ImageIndex = 0;
            this.btnRun.ImageList = imgList;
            this.btnRun.Location = new System.Drawing.Point(400, 78);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(30, 23);
            this.btnRun.TabIndex = 1;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // imgList
            // 
            imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            imgList.TransparentColor = System.Drawing.Color.Transparent;
            imgList.Images.SetKeyName(0, "Play");
            imgList.Images.SetKeyName(1, "Stop");
            imgList.Images.SetKeyName(2, "Kill");
            // 
            // dlgFileSave
            // 
            this.dlgFileSave.DefaultExt = "log";
            this.dlgFileSave.OverwritePrompt = false;
            this.dlgFileSave.Title = "Append Log to file";
            // 
            // tmrClock
            // 
            this.tmrClock.Interval = 250;
            this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(311, 78);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(64, 23);
            this.btnHide.TabIndex = 6;
            this.btnHide.Text = "ShowLog";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 2;
            this.btnClose.ImageList = imgList;
            this.btnClose.Location = new System.Drawing.Point(436, 78);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SystemTray
            // 
            this.SystemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("SystemTray.Icon")));
            this.SystemTray.Text = "Time protocol";
            this.SystemTray.Visible = true;
            this.SystemTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SystemTray_MouseDoubleClick);
            // 
            // pnlBackground
            // 
            pnlBackground.BackColor = System.Drawing.SystemColors.Control;
            pnlBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlBackground.Enabled = false;
            pnlBackground.Location = new System.Drawing.Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.Padding = new System.Windows.Forms.Padding(4);
            pnlBackground.Size = new System.Drawing.Size(478, 473);
            pnlBackground.TabIndex = 12;
            // 
            // ssdLog_Seconds
            // 
            this.ssdLog_Seconds.Digits = 2;
            this.ssdLog_Seconds.Enabled = false;
            this.ssdLog_Seconds.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ssdLog_Seconds.Location = new System.Drawing.Point(431, 12);
            this.ssdLog_Seconds.Name = "ssdLog_Seconds";
            this.ssdLog_Seconds.Size = new System.Drawing.Size(35, 39);
            this.ssdLog_Seconds.TabIndex = 10;
            this.ssdLog_Seconds.Thickness = 3;
            // 
            // ssdLog_Minutes
            // 
            this.ssdLog_Minutes.Digits = 2;
            this.ssdLog_Minutes.Enabled = false;
            this.ssdLog_Minutes.Location = new System.Drawing.Point(374, 12);
            this.ssdLog_Minutes.Name = "ssdLog_Minutes";
            this.ssdLog_Minutes.Size = new System.Drawing.Size(50, 60);
            this.ssdLog_Minutes.TabIndex = 9;
            this.ssdLog_Minutes.Thickness = 4;
            // 
            // ssdLog_Hours
            // 
            this.ssdLog_Hours.Digits = 2;
            this.ssdLog_Hours.Enabled = false;
            this.ssdLog_Hours.Location = new System.Drawing.Point(311, 12);
            this.ssdLog_Hours.Name = "ssdLog_Hours";
            this.ssdLog_Hours.Size = new System.Drawing.Size(50, 60);
            this.ssdLog_Hours.TabIndex = 7;
            this.ssdLog_Hours.Thickness = 4;
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
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 473);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ssdLog_Seconds);
            this.Controls.Add(this.ssdLog_Minutes);
            this.Controls.Add(label2);
            this.Controls.Add(this.ssdLog_Hours);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(label1);
            this.Controls.Add(this.ssdHours);
            this.Controls.Add(this.ssdMinutes);
            this.Controls.Add(this.ssdSeconds);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtProtocol);
            this.Controls.Add(pnlBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Time Protocol";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.DoubleClick += new System.EventHandler(this.frmMain_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtProtocol;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.SaveFileDialog dlgFileSave;
        private MMC_Controls.Seven_Segment_Display ssdSeconds;
        private MMC_Controls.Seven_Segment_Display ssdMinutes;
        private MMC_Controls.Seven_Segment_Display ssdHours;
        private System.Windows.Forms.Timer tmrClock;
        private System.Windows.Forms.Button btnHide;
        private MMC_Controls.Seven_Segment_Display ssdLog_Hours;
        private MMC_Controls.Seven_Segment_Display ssdLog_Minutes;
        private MMC_Controls.Seven_Segment_Display ssdLog_Seconds;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.NotifyIcon SystemTray;
    }
}

