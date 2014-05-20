using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MMC;

namespace LongInt_Test
{
    public partial class Form_Main : Form
    {
        public void Add(string Msg)
        {
            int cnt = lbMain.Items.Count;
            lbMain.Items.Add(cnt.ToString("D4") + " : " + Msg);
        }

        private void Add(ULong N)
        {
            Add(N.ToString());
        }

        public Form_Main()
        {
            InitializeComponent();
            //--------------------------------------------------------
            ULong A,B;
            //A = new ULong(0x76543218U);
            //B = new ULong(A);

            //for (int i = 0; i < 32; i++)
            //{
            //    Add(A.ToString());
            //    //A.ShiftLeft(4);
            //    //A.Add(B);
            //    // A += B;
            //    //A.Add(0x76543218U);
            //    //A += 0x76543218U;
            //    A.Dec();
            //}

            A = new ULong(1);
            A.ShiftLeft(64);
            B = new ULong(5);
            B.ShiftLeft(46);
            Add(A);
            Add(B);
            B.Sub(A);
            Add(B);
        }
    }
}
