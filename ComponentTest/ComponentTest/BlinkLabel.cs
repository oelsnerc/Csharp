using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComponentTest
{
    public partial class BlinkLabel : Label
    {
        public BlinkLabel()
        {
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Start();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
        }
    }
}
