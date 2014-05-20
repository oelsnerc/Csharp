namespace LongInt_Test
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
            this.lbMain = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbMain
            // 
            this.lbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMain.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMain.FormattingEnabled = true;
            this.lbMain.ItemHeight = 11;
            this.lbMain.Location = new System.Drawing.Point(0, 0);
            this.lbMain.Name = "lbMain";
            this.lbMain.Size = new System.Drawing.Size(491, 521);
            this.lbMain.TabIndex = 0;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 521);
            this.Controls.Add(this.lbMain);
            this.Name = "Form_Main";
            this.Text = "LongInt Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbMain;
    }
}

