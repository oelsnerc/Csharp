//********************************************************************
// frmMain - MainForm for the Calc Smart application
// (c) Dez 2010 MMC
//********************************************************************
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//********************************************************************
namespace Calc_Smart
{
    //****************************************************************
    public partial class frmMain : Form
    {
        private uint _Number;

        //************************************************************
        public frmMain()
        {
            InitializeComponent();

            _Number = 0;

        }

        //************************************************************
        // little Helpers
        protected void NewEntry(string Msg)
        {
            _Number++;
            tbView.SelectionLength = 0;
            tbView.SelectionStart = tbView.TextLength;
            tbView.SelectedText = (_Number.ToString("D4") + ":\t");
            tbView.SelectedText = (Msg + Environment.NewLine);
        }

        //************************************************************
        // the event handlers
        private void btnNumberClick(object sender, EventArgs e)
        {
            txtInput.SelectedText = ((Button)sender).Text;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            NewEntry(txtInput.Text);
            txtInput.Text = "";
        }

        private void btn_Brackets_Click(object sender, EventArgs e)
        {
            int bgn = txtInput.SelectionStart;
            int len = txtInput.SelectionLength;
            txtInput.SelectionLength = 0;
            txtInput.SelectionStart = bgn + len;
            txtInput.SelectedText = ")";
            txtInput.SelectionStart = bgn;
            txtInput.SelectedText = "(";
        }
    }
}
//********************************************************************
// END OF FILE frmMain
//********************************************************************
