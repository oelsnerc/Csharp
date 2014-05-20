namespace Zonk
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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            this.lblCount = new System.Windows.Forms.Label();
            this.lblZonk = new System.Windows.Forms.Label();
            this.lblAuto = new System.Windows.Forms.Label();
            this.lblRadio = new System.Windows.Forms.Label();
            this.clkBeat = new System.Windows.Forms.Timer(this.components);
            this.btnBeat = new System.Windows.Forms.Button();
            this.edSeed = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(69, 23);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(35, 13);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "label1";
            // 
            // lblZonk
            // 
            this.lblZonk.AutoSize = true;
            this.lblZonk.Location = new System.Drawing.Point(69, 47);
            this.lblZonk.Name = "lblZonk";
            this.lblZonk.Size = new System.Drawing.Size(35, 13);
            this.lblZonk.TabIndex = 1;
            this.lblZonk.Text = "label1";
            // 
            // lblAuto
            // 
            this.lblAuto.AutoSize = true;
            this.lblAuto.Location = new System.Drawing.Point(69, 71);
            this.lblAuto.Name = "lblAuto";
            this.lblAuto.Size = new System.Drawing.Size(35, 13);
            this.lblAuto.TabIndex = 2;
            this.lblAuto.Text = "label1";
            // 
            // lblRadio
            // 
            this.lblRadio.AutoSize = true;
            this.lblRadio.Location = new System.Drawing.Point(69, 95);
            this.lblRadio.Name = "lblRadio";
            this.lblRadio.Size = new System.Drawing.Size(35, 13);
            this.lblRadio.TabIndex = 3;
            this.lblRadio.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(25, 23);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(38, 13);
            label1.TabIndex = 4;
            label1.Text = "Count:";
            label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(28, 47);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 13);
            label2.TabIndex = 5;
            label2.Text = "Zonk:";
            label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(31, 71);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(32, 13);
            label3.TabIndex = 6;
            label3.Text = "Auto:";
            label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(25, 95);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(38, 13);
            label4.TabIndex = 7;
            label4.Text = "Radio:";
            label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // clkBeat
            // 
            this.clkBeat.Interval = 10;
            this.clkBeat.Tick += new System.EventHandler(this.clkBeat_Tick);
            // 
            // btnBeat
            // 
            this.btnBeat.Location = new System.Drawing.Point(214, 128);
            this.btnBeat.Name = "btnBeat";
            this.btnBeat.Size = new System.Drawing.Size(75, 23);
            this.btnBeat.TabIndex = 8;
            this.btnBeat.Text = "Start";
            this.btnBeat.UseVisualStyleBackColor = true;
            this.btnBeat.Click += new System.EventHandler(this.btnBeat_Click);
            // 
            // edSeed
            // 
            this.edSeed.Location = new System.Drawing.Point(163, 128);
            this.edSeed.Name = "edSeed";
            this.edSeed.Size = new System.Drawing.Size(45, 20);
            this.edSeed.TabIndex = 9;
            this.edSeed.Text = "42";
            this.edSeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(122, 131);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(35, 13);
            label5.TabIndex = 10;
            label5.Text = "Seed:";
            label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 154);
            this.Controls.Add(label5);
            this.Controls.Add(this.edSeed);
            this.Controls.Add(this.btnBeat);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.lblRadio);
            this.Controls.Add(this.lblAuto);
            this.Controls.Add(this.lblZonk);
            this.Controls.Add(this.lblCount);
            this.Name = "frmMain";
            this.Text = "Zonk";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblZonk;
        private System.Windows.Forms.Label lblAuto;
        private System.Windows.Forms.Label lblRadio;
        private System.Windows.Forms.Timer clkBeat;
        private System.Windows.Forms.Button btnBeat;
        private System.Windows.Forms.TextBox edSeed;
    }
}

