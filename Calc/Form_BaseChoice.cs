//********************************************************************
// Form_BaseChoice - Dialog to let the user determine the representation base
// (c) Jan 2011 MMC
//********************************************************************
using System;
using System.Windows.Forms;

namespace Calc
{
    public partial class Form_BaseChoice : Form
    {
        public Form_BaseChoice()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int Base;
            if (Int32.TryParse(edBase.Text, out Base) && (Base > 1) && (Base < 37))
            {
                lbBases.Items.Add(edBase.Text);
                edBase.Clear();
            }
            else
            {
                MessageBox.Show("Please provide only integer numbers\nbetween 1 and 37!", "Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int idx = lbBases.SelectedIndex;
            if (idx>=0) lbBases.Items.RemoveAt(idx);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int idx = lbBases.SelectedIndex;
            if (idx > 0) // can not move the 0
            {
                lbBases.BeginUpdate();
                object tmp = lbBases.Items[idx];
                lbBases.Items[idx] = lbBases.Items[idx - 1];
                lbBases.Items[idx - 1] = tmp;
                lbBases.SelectedIndex = idx - 1;
                lbBases.EndUpdate();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int idx = lbBases.SelectedIndex;
            int max = lbBases.Items.Count - 1;
            if (idx >= 0 && idx < max) // can not move the last one
            {
                lbBases.BeginUpdate();
                object tmp = lbBases.Items[idx];
                lbBases.Items[idx] = lbBases.Items[idx + 1];
                lbBases.Items[idx + 1] = tmp;
                lbBases.SelectedIndex = idx + 1;
                lbBases.EndUpdate();
            }
        }

        public int[] Bases
        {
            get
            {
                int cnt = lbBases.Items.Count;
                int[] res = new int[cnt];
                for (int i = 0; i < cnt; i++)
                {
                    res[i] = int.Parse(lbBases.Items[i].ToString());
                }
                return res;
            }

            set
            {
                lbBases.BeginUpdate();
                lbBases.Items.Clear();
                foreach (int Base in value)
                {
                    lbBases.Items.Add(Base.ToString());
                }
                lbBases.EndUpdate();
            }
        }

        public override string ToString()
        {
            string res = String.Empty;
            foreach (object item in lbBases.Items)
            {
                res += item.ToString();
                res += ',';
            }
            return res.Remove(res.Length - 1);
        }

        public void FromString(string List)
        {
            lbBases.BeginUpdate();
            lbBases.Items.Clear();
            string[] Bases = List.Split(',');
            foreach (string Base in Bases)
            {
                lbBases.Items.Add(Base.ToString());
            }
            lbBases.EndUpdate();
        }
    }
}

//********************************************************************
// END OF FILE Form_BaseChoice
//********************************************************************
