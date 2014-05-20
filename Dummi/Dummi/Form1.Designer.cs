namespace Dummi
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
            this.Bumbel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Box = new System.Windows.Forms.TextBox();
            this.Daten = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // Bumbel
            // 
            this.Bumbel.AutoSize = true;
            this.Bumbel.Location = new System.Drawing.Point(12, 44);
            this.Bumbel.Name = "Bumbel";
            this.Bumbel.Size = new System.Drawing.Size(35, 13);
            this.Bumbel.TabIndex = 0;
            this.Bumbel.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Drücker";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Box
            // 
            this.Box.Location = new System.Drawing.Point(23, 152);
            this.Box.Name = "Box";
            this.Box.Size = new System.Drawing.Size(257, 20);
            this.Box.TabIndex = 2;
            // 
            // Daten
            // 
            this.Daten.Location = new System.Drawing.Point(23, 287);
            this.Daten.Name = "Daten";
            this.Daten.TabIndex = 3;
            this.Daten.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 504);
            this.Controls.Add(this.Daten);
            this.Controls.Add(this.Box);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Bumbel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Bumbel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Box;
        private System.Windows.Forms.MonthCalendar Daten;
    }
}

