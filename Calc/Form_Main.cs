using System;
using System.Drawing;
using System.Windows.Forms;
using MMC.Calc;
using MMC.Numbers;
using Microsoft.Win32;  // for the registry access

namespace Calc
{
    public partial class Form_Main : Form
    {
        private Form_BaseChoice _OutputBase;
        private Font _Font_Term;
        private Font _Font_Number;
        private Font _Font_Result;
        private MMC.Calc.CEnvironment Env;
        private uint _Number;

        private string RegKey = "Software\\CO\\Calc";
        private string RegKey_Bases = "Bases";
        private string RegKey_Precision = "Precision";
        private string RegKey_Terms = "Terms";
        private Color Color_Default = Color.Black;
        private Color Color_Error = Color.Red;
        private Color Color_Back = Color.BlanchedAlmond;
        
        //************************************************************
        public Form_Main()
        {
            InitializeComponent();

            _OutputBase = new Form_BaseChoice();
            
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // create the output fonts
            _Font_Result = (Font) tbView.Font.Clone();
            _Font_Number = (Font) tbView.Font.Clone();
            _Font_Term = new Font(_Font_Result, FontStyle.Bold);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            mnuPrec_Integer.Tag = new CEnvironment(CNumber.CNumberType.cnt_Integer);
            mnuPrec_Double.Tag = new CEnvironment(CNumber.CNumberType.cnt_Double);

            SetPrecision(mnuPrec_Double);

            _Number = 0;

            ConfigRead(RegKey);
        }

        //------------------------------------------------------------
        public void ConfigRead(string Name)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(Name, false);
            if (reg != null)
            {
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // read the output bases
                Object Value = reg.GetValue(RegKey_Bases);
                if (Value != null)
                {
                    _OutputBase.FromString(Value.ToString());
                }
                else
                {
                    _OutputBase.FromString("10,16");
                }

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // read the precision
                Value = reg.GetValue(RegKey_Precision);
                if (Value != null)
                {
                    string Text = Value.ToString();
                    foreach (ToolStripMenuItem item in mnuPrecision.DropDownItems)
                    {
                        if (item.Text == Text)
                        {
                            SetPrecision(item);
                            break;
                        }
                    }
                }

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // read the terms
                Value = reg.GetValue(RegKey_Terms);
                if (Value != null)
                {
                    string[] Terms = Value.ToString().Split('\n');
                    lbHistory.Items.Clear();
                    foreach (string Term in Terms)
                    {
                        if (!String.IsNullOrEmpty(Term))
                            lbHistory.Items.Add(Term);
                    }
                }
            }
        }

        //------------------------------------------------------------
        public void ConfigWrite(string Name)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey(Name);
            if (reg != null)
            {
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // write the output bases
                string Bases = _OutputBase.ToString();
                reg.SetValue(RegKey_Bases, Bases);

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // write the precision
                foreach (ToolStripMenuItem item in mnuPrecision.DropDownItems)
                {
                    if (item.Checked)
                    {
                        reg.SetValue(RegKey_Precision, item.Text);
                        break;
                    }
                }

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // write the history
                int max = lbHistory.Items.Count;
                int idx = (max > 10) ? max-10 : 0;
                string Terms = String.Empty;
                while (idx < max)
                {
                    if (!String.IsNullOrEmpty(Terms)) Terms += '\n';
                    Terms += lbHistory.Items[idx];
                    idx++;
                }
                reg.SetValue(RegKey_Terms, Terms);
            }
        }

        //************************************************************
        protected void NewEntry(string Msg)
        {
            _Number++;
            tbView.SelectionLength = 0;
            tbView.SelectionStart = tbView.TextLength;
            tbView.SelectionFont = _Font_Number;
            tbView.SelectionIndent = 0;
            tbView.SelectionColor = Color_Default;

            tbView.AppendText(_Number.ToString("D4") + ":\t");

            Color old_bg = tbView.SelectionBackColor;
            tbView.SelectionFont = _Font_Term;
            tbView.SelectionBackColor = Color_Back;
            tbView.AppendText(Msg);
            tbView.SelectionBackColor = old_bg;

            tbView.AppendText(Environment.NewLine);
        }

        protected void NewSubEntry(string Msg, Color Col)
        {
            tbView.SelectionLength = 0;
            tbView.SelectionStart = tbView.TextLength;
            tbView.SelectionFont = _Font_Result;
            tbView.SelectionColor = Col;
            tbView.AppendText("\t" + Msg + Environment.NewLine);
        }

        //------------------------------------------------------------
        protected void AddToHistory(string Term)
        {
            // check if we already have it in history
            int idx = lbHistory.Items.IndexOf(Term);
            if ((lbHistory.Items.Count < 1) || (idx < lbHistory.Items.Count - 1)) // do not care for the most recent one
            {
                if (idx >= 0) lbHistory.Items.RemoveAt(idx);    // remove from the middle
                lbHistory.Items.Add(Term);                       // append it to the end
            }
        }

        //------------------------------------------------------------
        protected void SetPrecision(ToolStripMenuItem MenuItem)
        {
            foreach (ToolStripMenuItem item in mnuPrecision.DropDownItems)
            {
                item.Checked = (item == MenuItem);
            }
            Env = (MMC.Calc.CEnvironment)MenuItem.Tag;
            lblHeader.Text = "Result [" + MenuItem.Text + ']';
        }

        //------------------------------------------------------------
        // Add a result to the output TextBox
        // returns true if the Term was added
        protected bool Add(string Term)
        {
            // write to the output
            NewEntry(Term);

            try
            {
                // let the calculation begin
                // this will throw an exception on error
                MMC.Numbers.CNumber Result = Env.Evaluate(Term);
                if (Result == null) throw new MMC.Calc.CTermException("could not calculate!");

                //----------------------------------------------------
                // add the term to the history listbox
                AddToHistory(Term);

                //------------------------------------------------
                // and write the result to the output
                // print for all output-bases
                foreach (int Base in _OutputBase.Bases)
                {
                    NewSubEntry(Result.ToString((uint) Base), Color_Default);
                }
            }
            catch (MMC.Calc.CTermException exc)
            {
                NewSubEntry(exc.Message, Color_Error);
                return false;
            }
            return true;
        }

        //------------------------------------------------------------
        // What to do if calc was clicked?
        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (lbHistory.Visible && lbHistory.SelectedIndex>=0)
            {
                tbTerm.Text = (string) lbHistory.SelectedItem;
                lbHistory.Visible = false;
            }

            if (!String.IsNullOrEmpty(tbTerm.Text))
            {
                if (Add(tbTerm.Text)) tbTerm.Clear();
            }
        }

        //------------------------------------------------------------
        // implememnt a history like interface
        private void tbTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && lbHistory.Items.Count > 0)
            {
                lbHistory.Top = tbView.Bottom + tbTerm.Top - lbHistory.Height;
                lbHistory.Width = tbTerm.Width;
                lbHistory.Visible = true;
                lbHistory.SelectedIndex = lbHistory.Items.Count - 1;
                lbHistory.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down )
            {
                tbTerm.SelectedText = "RES";
                e.Handled = true;
            }
        }

        private void lbHistory_SelectedValueChanged(object sender, EventArgs e)
        {
            tbTerm.Text = (string) lbHistory.SelectedItem;
            tbTerm.SelectionStart = tbTerm.TextLength;
        }

        private void lbHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Space)
            {
                lbHistory.Visible = false;
                tbTerm.Focus();
                e.Handled = true;
            }
        }

        //------------------------------------------------------------
        private void mnuCopy_Click(object sender, EventArgs e)
        {
            tbView.Copy();
        }

        //------------------------------------------------------------
        // write the config before close
        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigWrite(RegKey);
        }

        //------------------------------------------------------------
        // clear the complete history
        private void mnuClear_Click(object sender, EventArgs e)
        {
            lbHistory.Items.Clear();
        }

        //------------------------------------------------------------
        // someone selected a new precision
        private void Precision_Click(object sender, EventArgs e)
        {
            SetPrecision((ToolStripMenuItem)sender);
        }

        private void outputBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OutputBase.ShowDialog();
        }
    }
}
