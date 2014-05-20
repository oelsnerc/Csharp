using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Microsoft.Win32;  // for the registry access

namespace CFind
{
    public partial class frmMain : Form
    {
        private CTagFile _Tags;
        private string _Prefix;
        private string[] _Files;
        private int[] _TabStops;

        //************************************************************
        public frmMain()
        {
            InitializeComponent();
            _Tags = null;
            _Prefix = null;
            _Files = null;
            _TabStops = null;

            lblWait.Parent = this;
            lblWait.BringToFront();
        }

        //************************************************************
        // Helpers
        //************************************************************
        private void SetTabStops(RichTextBox Box)
        {
            Box.SelectAll();
            Box.SelectionTabs = _TabStops;
            Box.SelectionLength = 0;
        }

        //------------------------------------------------------------
        private void SetPage(TabPage Page)
        {
            RichTextBox Box = (RichTextBox)Page.Controls[0];

            int bgn = Page.Text.IndexOf('(')+1;
            int end = Page.Text.IndexOf(')');
            int cnt = Box.Lines.Length;
            if (cnt > 0) cnt--;

            string Template = Page.Text.Remove(bgn,end-bgn);
            Page.Text = Template.Insert(bgn, cnt.ToString());

            SetTabStops(Box);
        }

        //------------------------------------------------------------
        private void SetAllPages(TabControl Tab)
        {
            foreach (TabPage Page in Tab.TabPages)
	        {
                SetPage(Page);
	        }
        }

        //------------------------------------------------------------
        private void FindFiles(string FileName)
        {
            txtResults_Files.Clear();
            if (FileName.Length < 3) return;

            if (_Files == null)
            {
                lblWait.Visible = true;
                Status_Text.Text = "Loading Filelist ...";
                Update();

                try
                {
                    Directory.SetCurrentDirectory(_Prefix);
                    _Files = Directory.GetFiles(_Prefix, "*", SearchOption.AllDirectories);
                    Status_Text.Text = "Files listed (" + _Files.Length.ToString() + ")";
                }
                catch (Exception ex)
                {
                    Status_Text.Text = "Loading Filelist failed!";
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                lblWait.Visible = false;
                Update();
            }

            if (_Files != null)
                foreach (string MyPath in _Files)
            {
                string MyFile = Path.GetFileName(MyPath);
                if (MyFile.StartsWith(FileName))
                {
                    MyFile = HidePrefix(MyFile);
                    txtResults_Files.AppendText(HidePrefix(MyPath) + '\n');
                }
            }
        }

        //------------------------------------------------------------
        private void SetResults(List<CTag> Results)
        {
            txtResults_C.Clear();
            txtResults_H.Clear();
            txtResults_IDL.Clear();
            txtResults_Unknown.Clear();

            int Count = Results.Count;
            if (Count > 0)
            {
                CTag Tag_File = Results[0];
                CTag Tag_Line = Results[0];
                for (int idx = 0; idx < Count; idx++)
			    {
                    CTag tag = Results[idx];
                    if (tag.File.Length > Tag_File.File.Length) Tag_File=tag;
                    if (tag.Line > Tag_Line.Line) Tag_Line=tag;

                    String Line = HidePrefix(tag.File) + "\t" + tag.Line.ToString() + "\t" + tag.Name + "\n";
                    switch (Path.GetExtension(tag.File))
                    {
                        case ".c"   :
                        case ".cpp" :
                            txtResults_C.AppendText(Line);
                            break;
                        case ".h"   :
                        case ".hpp" :
                            txtResults_H.AppendText(Line);
                            break;
                        case ".idl" :
                            txtResults_IDL.AppendText(Line);
                            break;
                        default :
                            txtResults_Unknown.AppendText(Line);
                            break;
                    }
                }

                Size Size_File = TextRenderer.MeasureText(HidePrefix(Tag_File.File), txtResults_C.Font);
                Size Size_Line = TextRenderer.MeasureText(Tag_Line.Line.ToString(), txtResults_C.Font);
                _TabStops = new int[] { Size_File.Width + 10, Size_Line.Width + 10 };
                Status_Text.Text = "Locations found: " + Count.ToString();
            }
            else
            {
                _TabStops = null;
                Status_Text.Text = "No matches found!";
            }
            SetAllPages(tabResults);
        }

        //------------------------------------------------------------
        private string Registry_GetTagFile()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("Software\\CO\\CFind", false);
            if (reg == null) return null;
            return reg.GetValue("TagFile").ToString();
        }

        //------------------------------------------------------------
        private void Registry_SetTagFile(string FileName)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\CO\\CFind");
            if (reg != null)
            {
                reg.SetValue("TagFile", FileName);
            }
        }

        //------------------------------------------------------------
        private void LoadTagFile(string FileName)
        {
            if (FileName == null)
            {
                if (dlgOpen.ShowDialog() != DialogResult.OK) return;
                FileName = dlgOpen.FileName;
            }
            Registry_SetTagFile(FileName);

            _Tags = null;
            lblWait.Visible = true;
            Status_Text.Text = "Loading Tag File: " + FileName;
            Update();

            try
            {
                _Tags = new CTagFile(FileName);
                _Prefix = _Tags.Prefix;
                txtPrefix.Text = _Prefix;
                Status_Text.Text = "Tag File Loaded (" + _Tags.Count.ToString() + ")";
            }
            catch (Exception ex)
            {
                Status_Text.Text = "Tag File Loading Failed";
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }

            lblWait.Visible = false;
            Update();
        }

        //------------------------------------------------------------
        private void NewSearch(string Tag)
        {
            if (_Tags == null)
            {
                LoadTagFile(Registry_GetTagFile());
            }

            if (_Tags != null && Tag.Length > 3)
            {
                if (_Files != null) FindFiles(Tag);
                List<CTag> results = _Tags.Find(Tag);
                SetResults(results);
            }
            else
            {
                FindFiles(String.Empty);
                SetResults(new List<CTag>());
            }
        }

        //------------------------------------------------------------
        private string HidePrefix(string File)
        {
            if (_Prefix != null && File.StartsWith(_Prefix)) return File.Substring(_Prefix.Length);
            return File;
        }

        //************************************************************
        // Events
        //************************************************************
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            NewSearch(txtInput.Text);
        }

        //------------------------------------------------------------
        private void btnTagFile_Click(object sender, EventArgs e)
        {
            LoadTagFile(null);
        }

        //------------------------------------------------------------
        private void btnHide_Click(object sender, EventArgs e)
        {
            if (txtPrefix.Text.Length > 0)
            {
                _Prefix = txtPrefix.Text;
            }
            else
            {
                _Prefix = null;
            }

            NewSearch(txtInput.Text);
        }

        //------------------------------------------------------------
        // this event is from the RichTextBoxes only
        private void txtResults_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Right)
            {
                RichTextBox Box = (RichTextBox)sender;

                int idx = Box.GetCharIndexFromPosition(e.Location);
                int line = Box.GetLineFromCharIndex(idx);
                if (line < Box.Lines.Length)
                {
                    string[] Words = Box.Lines[line].Split('\t');
                    Words[0] = _Prefix + Words[0];
                    idx = 0;
                    if (_TabStops != null)
                    {
                        int Pos = 0;
                        while (idx < _TabStops.Length)
                        {
                            Pos += _TabStops[idx];
                            if (e.X < Pos) break;
                            idx++;
                        }
                    }
                    if (idx < Words.Length)
                    {
                        Clipboard.SetText(Words[idx]);
                        MessageBox.Show(Words[idx], "Copied to Clipboard");
                    }
                }
            }
        }

        private void tabResults_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_Tags == null)
            {
                MessageBox.Show("Please open a tag file first", "CFind");
                e.Cancel = true;
                return;
            }

            if (e.TabPage == tabResults_Files)
            {
                FindFiles(txtInput.Text);
            }
        }
    }
}
