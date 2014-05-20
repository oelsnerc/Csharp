namespace FileBox
{
    partial class Form1
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
            System.Windows.Forms.ToolStrip tsMain;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.StatusStrip stsStrip;
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUp = new System.Windows.Forms.ToolStripButton();
            this.btnDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuListType = new System.Windows.Forms.ToolStripDropDownButton();
            this.miListTypeRelative = new System.Windows.Forms.ToolStripMenuItem();
            this.miListTypeAbsolute = new System.Windows.Forms.ToolStripMenuItem();
            this.txtNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            tsMain = new System.Windows.Forms.ToolStrip();
            stsStrip = new System.Windows.Forms.StatusStrip();
            tsMain.SuspendLayout();
            stsStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnAdd,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnUp,
            this.btnDown,
            this.toolStripSeparator2,
            this.mnuListType});
            tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            tsMain.Location = new System.Drawing.Point(0, 0);
            tsMain.Name = "tsMain";
            tsMain.Size = new System.Drawing.Size(602, 25);
            tsMain.TabIndex = 6;
            tsMain.Text = "ToolStrip";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save this list";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23, 22);
            this.btnAdd.Text = "Add Files";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete Files";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUp
            // 
            this.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(23, 22);
            this.btnUp.Text = "Move Up";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(23, 22);
            this.btnDown.Text = "Move Down";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuListType
            // 
            this.mnuListType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuListType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miListTypeRelative,
            this.miListTypeAbsolute});
            this.mnuListType.Image = ((System.Drawing.Image)(resources.GetObject("mnuListType.Image")));
            this.mnuListType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuListType.Name = "mnuListType";
            this.mnuListType.Size = new System.Drawing.Size(60, 22);
            this.mnuListType.Text = "ListType";
            // 
            // miListTypeRelative
            // 
            this.miListTypeRelative.Checked = true;
            this.miListTypeRelative.CheckOnClick = true;
            this.miListTypeRelative.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miListTypeRelative.Name = "miListTypeRelative";
            this.miListTypeRelative.Size = new System.Drawing.Size(152, 22);
            this.miListTypeRelative.Text = "relative";
            this.miListTypeRelative.Click += new System.EventHandler(this.miListType_Click);
            // 
            // miListTypeAbsolute
            // 
            this.miListTypeAbsolute.CheckOnClick = true;
            this.miListTypeAbsolute.Name = "miListTypeAbsolute";
            this.miListTypeAbsolute.Size = new System.Drawing.Size(152, 22);
            this.miListTypeAbsolute.Text = "absolute";
            this.miListTypeAbsolute.Click += new System.EventHandler(this.miListType_Click);
            // 
            // stsStrip
            // 
            stsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtNumber});
            stsStrip.Location = new System.Drawing.Point(0, 588);
            stsStrip.Name = "stsStrip";
            stsStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            stsStrip.ShowItemToolTips = true;
            stsStrip.Size = new System.Drawing.Size(602, 22);
            stsStrip.TabIndex = 7;
            stsStrip.Text = "statusStrip1";
            // 
            // txtNumber
            // 
            this.txtNumber.AutoSize = false;
            this.txtNumber.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.txtNumber.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(50, 17);
            this.txtNumber.Text = "0";
            this.txtNumber.ToolTipText = "Number of Files in the playlist";
            // 
            // lbFiles
            // 
            this.lbFiles.AllowDrop = true;
            this.lbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.HorizontalScrollbar = true;
            this.lbFiles.Location = new System.Drawing.Point(0, 25);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFiles.Size = new System.Drawing.Size(602, 563);
            this.lbFiles.TabIndex = 0;
            this.lbFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbFiles_MouseUp);
            this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.lbFiles_SelectedIndexChanged);
            this.lbFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbFiles_DragDrop);
            this.lbFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbFiles_MouseMove);
            this.lbFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbFiles_MouseDown);
            this.lbFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbFiles_DragEnter);
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "m3u";
            this.dlgSave.Filter = "Filelists (*.lst)|*.lst|All Files|*.*";
            this.dlgSave.Title = "Save the list";
            // 
            // dlgOpen
            // 
            this.dlgOpen.AddExtension = false;
            this.dlgOpen.Filter = "Alle Files|*.*";
            this.dlgOpen.Multiselect = true;
            this.dlgOpen.Title = "Add Files to the list";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 610);
            this.Controls.Add(this.lbFiles);
            this.Controls.Add(tsMain);
            this.Controls.Add(stsStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FileBox";
            tsMain.ResumeLayout(false);
            tsMain.PerformLayout();
            stsStrip.ResumeLayout(false);
            stsStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnUp;
        private System.Windows.Forms.ToolStripButton btnDown;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripStatusLabel txtNumber;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton mnuListType;
        private System.Windows.Forms.ToolStripMenuItem miListTypeRelative;
        private System.Windows.Forms.ToolStripMenuItem miListTypeAbsolute;
    }
}

