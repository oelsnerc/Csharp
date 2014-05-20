using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMC_SuDoKu
{
    public partial class frmMain : Form
    {
        protected Point pntField;               // hold the current field

        public frmMain()
        {
            InitializeComponent();
            New(3);
        }

        public void New(int Dimension)
        {
            sdkMain.Dimension = Dimension;
            lblStatus.Text = "Sudoku of Dimension " + sdkMain.Dimension.ToString() + " created";
        }

        public void Solve()
        {
            bool valid = sdkMain.Solve();
            sdkMain.Invalidate();
            if (!valid)
            {
                string Msg = lblStatus.Text + " lead to a contradiction!";
                lblStatus.Text = Msg;
                MessageBox.Show(Msg, "SuDoKu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                sdkMain.Undo();
                sdkMain.Invalidate();
            }
        }

        public void Undo()
        {
            sdkMain.Undo();
            sdkMain.Invalidate();
            lblStatus.Text = "Undo";
        }

        private void Menu_New_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            New(int.Parse(mi.Text));
        }

        private void Menu_Sudoku_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            //MessageBox.Show(mi.Tag.ToString());
            sdkMain.SaveState();
            sdkMain[pntField.X, pntField.Y] = (int)mi.Tag;
            sdkMain.Invalidate();
            lblStatus.Text = "Setting " + pntField.ToString() + " to " + mi.Tag.ToString();
            if (miSolve.Checked) Solve();
        }

        private void sdkMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pntField = sdkMain.ToIndex(e.X, e.Y);
                //lblStatus.Text = pntField.ToString() + " clicked!";

                List<int> L = sdkMain.Options(pntField.X, pntField.Y);
                mnuChoice.Items.Clear();
                if (L.Count == 0)
                {
                    mnuChoice.Items.Add(new ToolStripMenuItem("No Option Left!"));
                }
                else
                {
                    foreach (int v in L)
                    {
                        ToolStripMenuItem mi = new ToolStripMenuItem(v.ToString(), null, this.Menu_Sudoku_Click);
                        mi.Tag = v;
                        mnuChoice.Items.Add(mi);
                    }
                }

                mnuChoice.Show(sdkMain.PointToScreen(e.Location));
            }
            else if (e.Button == MouseButtons.XButton1)
            {
                Undo();
            }
        }

        private void Menu_Undo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void Menu_Save_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                sdkMain.SaveToFile(dlgSave.FileName);
                lblStatus.Text = "Saved to "+dlgSave.FileName;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                sdkMain.LoadFromFile(dlgOpen.FileName);
                sdkMain.Invalidate();
                lblStatus.Text = "Loaded from "+dlgOpen.FileName;
            }
        }

        private void Menu_Normalize_Click(object sender, EventArgs e)
        {
            sdkMain.SaveState();
            sdkMain.Normalize();
            sdkMain.Invalidate();
        }

        private void Menu_History_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sdkMain.History, "SuDoKu History");
        }
    }
}
