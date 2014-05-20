namespace CFind
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.btnTagFile = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.StatusStrip();
            this.Status_Text = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtResults_C = new System.Windows.Forms.RichTextBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnPrefix = new System.Windows.Forms.Button();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.tabResults = new System.Windows.Forms.TabControl();
            this.tabResuls_C = new System.Windows.Forms.TabPage();
            this.tabResults_H = new System.Windows.Forms.TabPage();
            this.txtResults_H = new System.Windows.Forms.RichTextBox();
            this.tabResults_IDL = new System.Windows.Forms.TabPage();
            this.txtResults_IDL = new System.Windows.Forms.RichTextBox();
            this.tabResults_Unknown = new System.Windows.Forms.TabPage();
            this.txtResults_Unknown = new System.Windows.Forms.RichTextBox();
            this.tabResults_Files = new System.Windows.Forms.TabPage();
            this.txtResults_Files = new System.Windows.Forms.RichTextBox();
            this.lblWait = new System.Windows.Forms.Label();
            this.Status.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.tabResults.SuspendLayout();
            this.tabResuls_C.SuspendLayout();
            this.tabResults_H.SuspendLayout();
            this.tabResults_IDL.SuspendLayout();
            this.tabResults_Unknown.SuspendLayout();
            this.tabResults_Files.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTagFile
            // 
            this.btnTagFile.Location = new System.Drawing.Point(286, 10);
            this.btnTagFile.Name = "btnTagFile";
            this.btnTagFile.Size = new System.Drawing.Size(75, 23);
            this.btnTagFile.TabIndex = 1;
            this.btnTagFile.Text = "new TagFile";
            this.btnTagFile.UseVisualStyleBackColor = true;
            this.btnTagFile.Click += new System.EventHandler(this.btnTagFile_Click);
            // 
            // Status
            // 
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status_Text});
            this.Status.Location = new System.Drawing.Point(0, 445);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(1000, 22);
            this.Status.TabIndex = 1;
            this.Status.Text = "None";
            // 
            // Status_Text
            // 
            this.Status_Text.Name = "Status_Text";
            this.Status_Text.Size = new System.Drawing.Size(92, 17);
            this.Status_Text.Text = "No TagFile loaded";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(12, 12);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(268, 20);
            this.txtInput.TabIndex = 0;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // txtResults_C
            // 
            this.txtResults_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults_C.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults_C.Location = new System.Drawing.Point(3, 3);
            this.txtResults_C.Name = "txtResults_C";
            this.txtResults_C.ReadOnly = true;
            this.txtResults_C.Size = new System.Drawing.Size(986, 337);
            this.txtResults_C.TabIndex = 3;
            this.txtResults_C.Text = "";
            this.txtResults_C.WordWrap = false;
            this.txtResults_C.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtResults_MouseUp);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnPrefix);
            this.pnlBottom.Controls.Add(this.txtPrefix);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 414);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1000, 31);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnPrefix
            // 
            this.btnPrefix.Location = new System.Drawing.Point(182, 4);
            this.btnPrefix.Name = "btnPrefix";
            this.btnPrefix.Size = new System.Drawing.Size(75, 23);
            this.btnPrefix.TabIndex = 1;
            this.btnPrefix.Text = "Hide Prefix";
            this.btnPrefix.UseVisualStyleBackColor = true;
            this.btnPrefix.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(12, 6);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(164, 20);
            this.txtPrefix.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnTagFile);
            this.pnlTop.Controls.Add(this.txtInput);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1000, 45);
            this.pnlTop.TabIndex = 0;
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.tabResuls_C);
            this.tabResults.Controls.Add(this.tabResults_H);
            this.tabResults.Controls.Add(this.tabResults_IDL);
            this.tabResults.Controls.Add(this.tabResults_Unknown);
            this.tabResults.Controls.Add(this.tabResults_Files);
            this.tabResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResults.Location = new System.Drawing.Point(0, 45);
            this.tabResults.Name = "tabResults";
            this.tabResults.SelectedIndex = 0;
            this.tabResults.Size = new System.Drawing.Size(1000, 369);
            this.tabResults.TabIndex = 4;
            this.tabResults.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabResults_Selecting);
            // 
            // tabResuls_C
            // 
            this.tabResuls_C.Controls.Add(this.txtResults_C);
            this.tabResuls_C.Location = new System.Drawing.Point(4, 22);
            this.tabResuls_C.Name = "tabResuls_C";
            this.tabResuls_C.Padding = new System.Windows.Forms.Padding(3);
            this.tabResuls_C.Size = new System.Drawing.Size(992, 343);
            this.tabResuls_C.TabIndex = 0;
            this.tabResuls_C.Text = "C-Files (0)";
            this.tabResuls_C.UseVisualStyleBackColor = true;
            // 
            // tabResults_H
            // 
            this.tabResults_H.Controls.Add(this.txtResults_H);
            this.tabResults_H.Location = new System.Drawing.Point(4, 22);
            this.tabResults_H.Name = "tabResults_H";
            this.tabResults_H.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults_H.Size = new System.Drawing.Size(992, 343);
            this.tabResults_H.TabIndex = 1;
            this.tabResults_H.Text = "H-Files (0)";
            this.tabResults_H.UseVisualStyleBackColor = true;
            // 
            // txtResults_H
            // 
            this.txtResults_H.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults_H.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults_H.Location = new System.Drawing.Point(3, 3);
            this.txtResults_H.Name = "txtResults_H";
            this.txtResults_H.ReadOnly = true;
            this.txtResults_H.Size = new System.Drawing.Size(986, 337);
            this.txtResults_H.TabIndex = 4;
            this.txtResults_H.Text = "";
            this.txtResults_H.WordWrap = false;
            this.txtResults_H.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtResults_MouseUp);
            // 
            // tabResults_IDL
            // 
            this.tabResults_IDL.Controls.Add(this.txtResults_IDL);
            this.tabResults_IDL.Location = new System.Drawing.Point(4, 22);
            this.tabResults_IDL.Name = "tabResults_IDL";
            this.tabResults_IDL.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults_IDL.Size = new System.Drawing.Size(992, 343);
            this.tabResults_IDL.TabIndex = 2;
            this.tabResults_IDL.Text = "IDL-Files (0)";
            this.tabResults_IDL.UseVisualStyleBackColor = true;
            // 
            // txtResults_IDL
            // 
            this.txtResults_IDL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults_IDL.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults_IDL.Location = new System.Drawing.Point(3, 3);
            this.txtResults_IDL.Name = "txtResults_IDL";
            this.txtResults_IDL.ReadOnly = true;
            this.txtResults_IDL.Size = new System.Drawing.Size(986, 337);
            this.txtResults_IDL.TabIndex = 5;
            this.txtResults_IDL.Text = "";
            this.txtResults_IDL.WordWrap = false;
            this.txtResults_IDL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtResults_MouseUp);
            // 
            // tabResults_Unknown
            // 
            this.tabResults_Unknown.Controls.Add(this.txtResults_Unknown);
            this.tabResults_Unknown.Location = new System.Drawing.Point(4, 22);
            this.tabResults_Unknown.Name = "tabResults_Unknown";
            this.tabResults_Unknown.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults_Unknown.Size = new System.Drawing.Size(992, 343);
            this.tabResults_Unknown.TabIndex = 3;
            this.tabResults_Unknown.Text = "Unknown (0)";
            this.tabResults_Unknown.UseVisualStyleBackColor = true;
            // 
            // txtResults_Unknown
            // 
            this.txtResults_Unknown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults_Unknown.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults_Unknown.Location = new System.Drawing.Point(3, 3);
            this.txtResults_Unknown.Name = "txtResults_Unknown";
            this.txtResults_Unknown.ReadOnly = true;
            this.txtResults_Unknown.Size = new System.Drawing.Size(986, 337);
            this.txtResults_Unknown.TabIndex = 5;
            this.txtResults_Unknown.Text = "";
            this.txtResults_Unknown.WordWrap = false;
            this.txtResults_Unknown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtResults_MouseUp);
            // 
            // tabResults_Files
            // 
            this.tabResults_Files.Controls.Add(this.txtResults_Files);
            this.tabResults_Files.Location = new System.Drawing.Point(4, 22);
            this.tabResults_Files.Name = "tabResults_Files";
            this.tabResults_Files.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults_Files.Size = new System.Drawing.Size(992, 343);
            this.tabResults_Files.TabIndex = 4;
            this.tabResults_Files.Text = "Files (0)";
            this.tabResults_Files.UseVisualStyleBackColor = true;
            // 
            // txtResults_Files
            // 
            this.txtResults_Files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults_Files.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults_Files.Location = new System.Drawing.Point(3, 3);
            this.txtResults_Files.Name = "txtResults_Files";
            this.txtResults_Files.ReadOnly = true;
            this.txtResults_Files.Size = new System.Drawing.Size(986, 337);
            this.txtResults_Files.TabIndex = 6;
            this.txtResults_Files.Text = "";
            this.txtResults_Files.WordWrap = false;
            this.txtResults_Files.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtResults_MouseUp);
            // 
            // lblWait
            // 
            this.lblWait.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWait.Location = new System.Drawing.Point(328, 81);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(314, 173);
            this.lblWait.TabIndex = 2;
            this.lblWait.Text = "PLEASE WAIT!";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWait.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 467);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.tabResults);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.Status);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "CFind";
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tabResults.ResumeLayout(false);
            this.tabResuls_C.ResumeLayout(false);
            this.tabResults_H.ResumeLayout(false);
            this.tabResults_IDL.ResumeLayout(false);
            this.tabResults_Unknown.ResumeLayout(false);
            this.tabResults_Files.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Button btnTagFile;
        private System.Windows.Forms.StatusStrip Status;
        private System.Windows.Forms.ToolStripStatusLabel Status_Text;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.RichTextBox txtResults_C;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TabControl tabResults;
        private System.Windows.Forms.TabPage tabResuls_C;
        private System.Windows.Forms.TabPage tabResults_H;
        private System.Windows.Forms.RichTextBox txtResults_H;
        private System.Windows.Forms.TabPage tabResults_IDL;
        private System.Windows.Forms.TabPage tabResults_Unknown;
        private System.Windows.Forms.RichTextBox txtResults_IDL;
        private System.Windows.Forms.RichTextBox txtResults_Unknown;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Button btnPrefix;
        private System.Windows.Forms.TabPage tabResults_Files;
        private System.Windows.Forms.RichTextBox txtResults_Files;
        private System.Windows.Forms.Label lblWait;
    }
}

