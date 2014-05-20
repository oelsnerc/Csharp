namespace Calc_Smart
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.Windows.Forms.Button btn_1;
            System.Windows.Forms.Button btn_4;
            System.Windows.Forms.Button btn_2;
            System.Windows.Forms.Button btn_3;
            System.Windows.Forms.Button btn_5;
            System.Windows.Forms.Button btn_6;
            System.Windows.Forms.Button btn_7;
            System.Windows.Forms.Button btn_8;
            System.Windows.Forms.Button btn_9;
            System.Windows.Forms.Button btn_0;
            System.Windows.Forms.Button btn_DecimalPoint;
            System.Windows.Forms.Button btn_Sub;
            System.Windows.Forms.Button btn_Add;
            System.Windows.Forms.Button btn_Mul;
            System.Windows.Forms.Button btn_Div;
            System.Windows.Forms.Button btn_Brackets;
            System.Windows.Forms.Panel panel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btn_BCK = new System.Windows.Forms.Button();
            this.tbView = new System.Windows.Forms.TextBox();
            btn_1 = new System.Windows.Forms.Button();
            btn_4 = new System.Windows.Forms.Button();
            btn_2 = new System.Windows.Forms.Button();
            btn_3 = new System.Windows.Forms.Button();
            btn_5 = new System.Windows.Forms.Button();
            btn_6 = new System.Windows.Forms.Button();
            btn_7 = new System.Windows.Forms.Button();
            btn_8 = new System.Windows.Forms.Button();
            btn_9 = new System.Windows.Forms.Button();
            btn_0 = new System.Windows.Forms.Button();
            btn_DecimalPoint = new System.Windows.Forms.Button();
            btn_Sub = new System.Windows.Forms.Button();
            btn_Add = new System.Windows.Forms.Button();
            btn_Mul = new System.Windows.Forms.Button();
            btn_Div = new System.Windows.Forms.Button();
            btn_Brackets = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_1
            // 
            btn_1.Location = new System.Drawing.Point(5, 100);
            btn_1.Name = "btn_1";
            btn_1.Size = new System.Drawing.Size(30, 25);
            btn_1.TabIndex = 1;
            btn_1.Text = "1";
            btn_1.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_4
            // 
            btn_4.Location = new System.Drawing.Point(5, 69);
            btn_4.Name = "btn_4";
            btn_4.Size = new System.Drawing.Size(30, 25);
            btn_4.TabIndex = 2;
            btn_4.Text = "4";
            btn_4.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_2
            // 
            btn_2.Location = new System.Drawing.Point(41, 100);
            btn_2.Name = "btn_2";
            btn_2.Size = new System.Drawing.Size(30, 25);
            btn_2.TabIndex = 3;
            btn_2.Text = "2";
            btn_2.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_3
            // 
            btn_3.Location = new System.Drawing.Point(77, 100);
            btn_3.Name = "btn_3";
            btn_3.Size = new System.Drawing.Size(30, 25);
            btn_3.TabIndex = 6;
            btn_3.Text = "3";
            btn_3.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_5
            // 
            btn_5.Location = new System.Drawing.Point(41, 69);
            btn_5.Name = "btn_5";
            btn_5.Size = new System.Drawing.Size(30, 25);
            btn_5.TabIndex = 7;
            btn_5.Text = "5";
            btn_5.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_6
            // 
            btn_6.Location = new System.Drawing.Point(77, 69);
            btn_6.Name = "btn_6";
            btn_6.Size = new System.Drawing.Size(30, 25);
            btn_6.TabIndex = 8;
            btn_6.Text = "6";
            btn_6.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_7
            // 
            btn_7.Location = new System.Drawing.Point(5, 38);
            btn_7.Name = "btn_7";
            btn_7.Size = new System.Drawing.Size(30, 25);
            btn_7.TabIndex = 9;
            btn_7.Text = "7";
            btn_7.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_8
            // 
            btn_8.Location = new System.Drawing.Point(41, 38);
            btn_8.Name = "btn_8";
            btn_8.Size = new System.Drawing.Size(30, 25);
            btn_8.TabIndex = 10;
            btn_8.Text = "8";
            btn_8.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_9
            // 
            btn_9.Location = new System.Drawing.Point(77, 38);
            btn_9.Name = "btn_9";
            btn_9.Size = new System.Drawing.Size(30, 25);
            btn_9.TabIndex = 11;
            btn_9.Text = "9";
            btn_9.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_0
            // 
            btn_0.Location = new System.Drawing.Point(41, 131);
            btn_0.Name = "btn_0";
            btn_0.Size = new System.Drawing.Size(30, 25);
            btn_0.TabIndex = 12;
            btn_0.Text = "0";
            btn_0.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_DecimalPoint
            // 
            btn_DecimalPoint.Location = new System.Drawing.Point(5, 131);
            btn_DecimalPoint.Name = "btn_DecimalPoint";
            btn_DecimalPoint.Size = new System.Drawing.Size(30, 25);
            btn_DecimalPoint.TabIndex = 13;
            btn_DecimalPoint.Text = ",";
            btn_DecimalPoint.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_Sub
            // 
            btn_Sub.Location = new System.Drawing.Point(179, 100);
            btn_Sub.Name = "btn_Sub";
            btn_Sub.Size = new System.Drawing.Size(30, 25);
            btn_Sub.TabIndex = 14;
            btn_Sub.Text = "-";
            btn_Sub.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_Add
            // 
            btn_Add.Location = new System.Drawing.Point(143, 100);
            btn_Add.Name = "btn_Add";
            btn_Add.Size = new System.Drawing.Size(30, 25);
            btn_Add.TabIndex = 15;
            btn_Add.Text = "+";
            btn_Add.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_Mul
            // 
            btn_Mul.Location = new System.Drawing.Point(143, 69);
            btn_Mul.Name = "btn_Mul";
            btn_Mul.Size = new System.Drawing.Size(30, 25);
            btn_Mul.TabIndex = 16;
            btn_Mul.Text = "*";
            btn_Mul.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_Div
            // 
            btn_Div.Location = new System.Drawing.Point(179, 69);
            btn_Div.Name = "btn_Div";
            btn_Div.Size = new System.Drawing.Size(30, 25);
            btn_Div.TabIndex = 17;
            btn_Div.Text = "/";
            btn_Div.Click += new System.EventHandler(this.btnNumberClick);
            // 
            // btn_Brackets
            // 
            btn_Brackets.Location = new System.Drawing.Point(143, 38);
            btn_Brackets.Name = "btn_Brackets";
            btn_Brackets.Size = new System.Drawing.Size(30, 25);
            btn_Brackets.TabIndex = 18;
            btn_Brackets.Text = "( )";
            btn_Brackets.Click += new System.EventHandler(this.btn_Brackets_Click);
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtInput.Location = new System.Drawing.Point(3, 6);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(234, 26);
            this.txtInput.TabIndex = 0;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(77, 131);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(60, 25);
            this.btnCalc.TabIndex = 5;
            this.btnCalc.Text = "Calc";
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btn_BCK
            // 
            this.btn_BCK.Location = new System.Drawing.Point(143, 131);
            this.btn_BCK.Name = "btn_BCK";
            this.btn_BCK.Size = new System.Drawing.Size(60, 25);
            this.btn_BCK.TabIndex = 19;
            this.btn_BCK.Text = "BACK";
            // 
            // panel1
            // 
            panel1.Controls.Add(this.txtInput);
            panel1.Controls.Add(this.btn_BCK);
            panel1.Controls.Add(this.btnCalc);
            panel1.Controls.Add(btn_Brackets);
            panel1.Controls.Add(btn_1);
            panel1.Controls.Add(btn_Div);
            panel1.Controls.Add(btn_4);
            panel1.Controls.Add(btn_Mul);
            panel1.Controls.Add(btn_2);
            panel1.Controls.Add(btn_Add);
            panel1.Controls.Add(btn_3);
            panel1.Controls.Add(btn_Sub);
            panel1.Controls.Add(btn_5);
            panel1.Controls.Add(btn_DecimalPoint);
            panel1.Controls.Add(btn_6);
            panel1.Controls.Add(btn_0);
            panel1.Controls.Add(btn_7);
            panel1.Controls.Add(btn_9);
            panel1.Controls.Add(btn_8);
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(0, 107);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(240, 161);
            // 
            // tbView
            // 
            this.tbView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbView.Location = new System.Drawing.Point(0, 0);
            this.tbView.Multiline = true;
            this.tbView.Name = "tbView";
            this.tbView.ReadOnly = true;
            this.tbView.Size = new System.Drawing.Size(240, 107);
            this.tbView.TabIndex = 21;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tbView);
            this.Controls.Add(panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "frmMain";
            this.Text = "Calc Smart";
            panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Button btn_BCK;
        private System.Windows.Forms.TextBox tbView;
    }
}

