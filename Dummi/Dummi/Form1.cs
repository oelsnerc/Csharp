using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dummi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bumbel.Text = Box.Text;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            Bumbel.Text = e.End.ToLongDateString();
        }
    }
}
