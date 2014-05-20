namespace Taptris
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
            System.Windows.Forms.ToolStrip toolStrip1;
            this.pbDesk = new System.Windows.Forms.PictureBox();
            this.tmTick = new System.Windows.Forms.Timer(this.components);
            this.lblBricks = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.pbDesk)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(592, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // pbDesk
            // 
            this.pbDesk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDesk.Location = new System.Drawing.Point(0, 25);
            this.pbDesk.Name = "pbDesk";
            this.pbDesk.Size = new System.Drawing.Size(492, 596);
            this.pbDesk.TabIndex = 1;
            this.pbDesk.TabStop = false;
            this.pbDesk.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbDesk_MouseUp);
            this.pbDesk.SizeChanged += new System.EventHandler(this.pbDesk_SizeChanged);
            // 
            // tmTick
            // 
            this.tmTick.Interval = 200;
            this.tmTick.Tick += new System.EventHandler(this.tmTick_Tick);
            // 
            // lblBricks
            // 
            this.lblBricks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBricks.Location = new System.Drawing.Point(43, 13);
            this.lblBricks.Name = "lblBricks";
            this.lblBricks.Size = new System.Drawing.Size(47, 20);
            this.lblBricks.TabIndex = 2;
            this.lblBricks.Text = "0";
            this.lblBricks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblScore);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblBricks);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(492, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 596);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bricks:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Score:";
            // 
            // lblScore
            // 
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(43, 71);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(47, 20);
            this.lblScore.TabIndex = 5;
            this.lblScore.Text = "0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 621);
            this.Controls.Add(this.pbDesk);
            this.Controls.Add(this.panel1);
            this.Controls.Add(toolStrip1);
            this.MinimumSize = new System.Drawing.Size(500, 600);
            this.Name = "frmMain";
            this.Text = "MMC Taptris";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pbDesk)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbDesk;
        private System.Windows.Forms.Timer tmTick;
        private System.Windows.Forms.Label lblBricks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label label2;
    }
}

