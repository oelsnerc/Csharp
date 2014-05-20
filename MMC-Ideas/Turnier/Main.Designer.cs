namespace Turnier
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.edCount = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.edCourts = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 13);
            label1.TabIndex = 0;
            label1.Text = "Anzahl Teams:";
            // 
            // edCount
            // 
            this.edCount.Location = new System.Drawing.Point(95, 6);
            this.edCount.Name = "edCount";
            this.edCount.Size = new System.Drawing.Size(56, 20);
            this.edCount.TabIndex = 1;
            this.edCount.Text = "4";
            this.edCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(157, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "trn";
            this.dlgSave.Filter = "Turnier-Dateien|*.trn|Alle Dateien|*.*";
            this.dlgSave.Title = "Turnier Text Datei speichern";
            // 
            // edCourts
            // 
            this.edCourts.Location = new System.Drawing.Point(95, 35);
            this.edCourts.Name = "edCourts";
            this.edCourts.Size = new System.Drawing.Size(56, 20);
            this.edCourts.TabIndex = 4;
            this.edCourts.Text = "2";
            this.edCourts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(15, 38);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(74, 13);
            label2.TabIndex = 3;
            label2.Text = "Anzahl Felder:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 271);
            this.Controls.Add(this.edCourts);
            this.Controls.Add(label2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.edCount);
            this.Controls.Add(label1);
            this.Name = "frmMain";
            this.Text = "Turnier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edCount;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.TextBox edCourts;
    }
}

