namespace Calc
{
    partial class Form_Main
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
            System.Windows.Forms.Panel pnlCalc;
            System.Windows.Forms.Button btnCalc;
            System.Windows.Forms.Panel pnlView;
            System.Windows.Forms.ToolStripMenuItem mnuCopy;
            System.Windows.Forms.ToolStripMenuItem mnuClear;
            System.Windows.Forms.ToolStripMenuItem outputBaseToolStripMenuItem;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.tbTerm = new System.Windows.Forms.TextBox();
            this.lbHistory = new System.Windows.Forms.ListBox();
            this.tbView = new System.Windows.Forms.RichTextBox();
            this.mnuResult = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPrecision = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrec_Double = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrec_Integer = new System.Windows.Forms.ToolStripMenuItem();
            this.lblHeader = new System.Windows.Forms.Label();
            pnlCalc = new System.Windows.Forms.Panel();
            btnCalc = new System.Windows.Forms.Button();
            pnlView = new System.Windows.Forms.Panel();
            mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            mnuClear = new System.Windows.Forms.ToolStripMenuItem();
            outputBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pnlCalc.SuspendLayout();
            pnlView.SuspendLayout();
            this.mnuResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCalc
            // 
            pnlCalc.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            pnlCalc.Controls.Add(this.tbTerm);
            pnlCalc.Controls.Add(btnCalc);
            pnlCalc.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlCalc.Location = new System.Drawing.Point(0, 395);
            pnlCalc.Name = "pnlCalc";
            pnlCalc.Padding = new System.Windows.Forms.Padding(3);
            pnlCalc.Size = new System.Drawing.Size(413, 32);
            pnlCalc.TabIndex = 0;
            // 
            // tbTerm
            // 
            this.tbTerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTerm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTerm.Location = new System.Drawing.Point(3, 3);
            this.tbTerm.Name = "tbTerm";
            this.tbTerm.Size = new System.Drawing.Size(338, 26);
            this.tbTerm.TabIndex = 0;
            this.tbTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTerm_KeyDown);
            // 
            // btnCalc
            // 
            btnCalc.Dock = System.Windows.Forms.DockStyle.Right;
            btnCalc.Location = new System.Drawing.Point(341, 3);
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new System.Drawing.Size(69, 26);
            btnCalc.TabIndex = 1;
            btnCalc.Text = "Calc";
            btnCalc.UseVisualStyleBackColor = true;
            btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // pnlView
            // 
            pnlView.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            pnlView.Controls.Add(this.lbHistory);
            pnlView.Controls.Add(this.tbView);
            pnlView.Controls.Add(this.lblHeader);
            pnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlView.Location = new System.Drawing.Point(0, 0);
            pnlView.Name = "pnlView";
            pnlView.Padding = new System.Windows.Forms.Padding(3);
            pnlView.Size = new System.Drawing.Size(413, 395);
            pnlView.TabIndex = 2;
            // 
            // lbHistory
            // 
            this.lbHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHistory.FormattingEnabled = true;
            this.lbHistory.ItemHeight = 20;
            this.lbHistory.Location = new System.Drawing.Point(3, 254);
            this.lbHistory.Name = "lbHistory";
            this.lbHistory.Size = new System.Drawing.Size(338, 144);
            this.lbHistory.TabIndex = 2;
            this.lbHistory.Visible = false;
            this.lbHistory.SelectedValueChanged += new System.EventHandler(this.lbHistory_SelectedValueChanged);
            this.lbHistory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbHistory_KeyDown);
            // 
            // tbView
            // 
            this.tbView.BackColor = System.Drawing.SystemColors.Window;
            this.tbView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbView.ContextMenuStrip = this.mnuResult;
            this.tbView.DetectUrls = false;
            this.tbView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbView.HideSelection = false;
            this.tbView.Location = new System.Drawing.Point(3, 19);
            this.tbView.Name = "tbView";
            this.tbView.ReadOnly = true;
            this.tbView.Size = new System.Drawing.Size(407, 373);
            this.tbView.TabIndex = 0;
            this.tbView.Text = "";
            this.tbView.WordWrap = false;
            // 
            // mnuResult
            // 
            this.mnuResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            mnuCopy,
            mnuClear,
            this.toolStripSeparator1,
            this.mnuPrecision,
            outputBaseToolStripMenuItem});
            this.mnuResult.Name = "mnuResult";
            this.mnuResult.Size = new System.Drawing.Size(153, 120);
            // 
            // mnuCopy
            // 
            mnuCopy.Name = "mnuCopy";
            mnuCopy.Size = new System.Drawing.Size(152, 22);
            mnuCopy.Text = "Copy";
            mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // mnuClear
            // 
            mnuClear.Name = "mnuClear";
            mnuClear.Size = new System.Drawing.Size(152, 22);
            mnuClear.Text = "Clear History";
            mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuPrecision
            // 
            this.mnuPrecision.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPrec_Double,
            this.mnuPrec_Integer});
            this.mnuPrecision.Name = "mnuPrecision";
            this.mnuPrecision.Size = new System.Drawing.Size(152, 22);
            this.mnuPrecision.Text = "Precision";
            // 
            // mnuPrec_Double
            // 
            this.mnuPrec_Double.Checked = true;
            this.mnuPrec_Double.CheckOnClick = true;
            this.mnuPrec_Double.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuPrec_Double.Name = "mnuPrec_Double";
            this.mnuPrec_Double.Size = new System.Drawing.Size(152, 22);
            this.mnuPrec_Double.Text = "Double";
            this.mnuPrec_Double.ToolTipText = "IEEE double precision";
            this.mnuPrec_Double.Click += new System.EventHandler(this.Precision_Click);
            // 
            // mnuPrec_Integer
            // 
            this.mnuPrec_Integer.CheckOnClick = true;
            this.mnuPrec_Integer.Name = "mnuPrec_Integer";
            this.mnuPrec_Integer.Size = new System.Drawing.Size(152, 22);
            this.mnuPrec_Integer.Text = "Integer";
            this.mnuPrec_Integer.ToolTipText = "infitinite integer calculation";
            this.mnuPrec_Integer.Click += new System.EventHandler(this.Precision_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Location = new System.Drawing.Point(3, 3);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(407, 16);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Result";
            // 
            // outputBaseToolStripMenuItem
            // 
            outputBaseToolStripMenuItem.Name = "outputBaseToolStripMenuItem";
            outputBaseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            outputBaseToolStripMenuItem.Text = "Output Base ...";
            outputBaseToolStripMenuItem.Click += new System.EventHandler(this.outputBaseToolStripMenuItem_Click);
            // 
            // Form_Main
            // 
            this.AcceptButton = btnCalc;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 427);
            this.Controls.Add(pnlView);
            this.Controls.Add(pnlCalc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Text = "MMC Calc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            pnlCalc.ResumeLayout(false);
            pnlCalc.PerformLayout();
            pnlView.ResumeLayout(false);
            this.mnuResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbTerm;
        private System.Windows.Forms.RichTextBox tbView;
        private System.Windows.Forms.ListBox lbHistory;
        private System.Windows.Forms.ContextMenuStrip mnuResult;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuPrec_Double;
        private System.Windows.Forms.ToolStripMenuItem mnuPrec_Integer;
        private System.Windows.Forms.ToolStripMenuItem mnuPrecision;
        private System.Windows.Forms.Label lblHeader;
    }
}

