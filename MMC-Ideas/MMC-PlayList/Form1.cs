//********************************************************************
// Form1 - This is a simple .Net application to create m3u song lists
//         it is a sample of handling file names & Drag and Drop
// (c) Nov 2009 MMC
//********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

//********************************************************************
namespace MMC_PlayList
{
    public partial class Form1 : Form
    {
        private Rectangle rctDragBox;
        private const MouseButtons MButton = MouseButtons.Right;

        public Form1()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------
        // Update the Status bar (for now just show the current song number)
        private void Update_Status()
        {
            txtNumber.Text = lbFiles.SelectedItems.Count.ToString() + '/' +lbFiles.Items.Count.ToString();
        }

        //------------------------------------------------------------
        // add a string to the list
        // this could be
        // a) a directory name  - so include all files beneath
        // b) a mp3 song        - so add this to the list
        // c) a m3u songlist    - so add all songs from that list to this one
        private int Add(string Song)
        {
            int cnt = 0;
            if (Directory.Exists(Song))
            {
                cnt += Add(Directory.GetFileSystemEntries(Song));
            }
            else
            {
                switch (Path.GetExtension(Song))
                {
                    //case ".jpg" :
                    case ".mp3" :
                        {
                            int idx = lbFiles.Items.IndexOf(Song);
                            if (idx < 0)
                            {
                                idx = lbFiles.Items.Add(Song);
                                cnt++;
                            };
                            lbFiles.SelectedIndices.Add(idx);
                        };
                        break;
                    case ".m3u":
                        {
                            if (File.Exists(Song))
                            {
                                string OldDir = Directory.GetCurrentDirectory();
                                Directory.SetCurrentDirectory(Path.GetDirectoryName(Song));
                                using (StreamReader fs = File.OpenText(Song))
                                {
                                    while (fs.Peek() >= 0)
                                    {
                                        cnt+=Add(Path.GetFullPath(fs.ReadLine()));
                                    }
                                    fs.Close();
                                };
                                Directory.SetCurrentDirectory(OldDir);
                            };
                        };
                        break;
                };
            };
            return cnt;
        }

        //------------------------------------------------------------
        // add a list of files (see Add(string) for Details)
        private int Add(string[] List)
        {
            int cnt = 0;
            foreach (string song in List)
            {
                cnt += Add(song);
            };
            return cnt;
        }

        //------------------------------------------------------------
        // Remove the currently selected items
        private void Delete()
        {
            int cnt = lbFiles.SelectedIndices.Count;
            if (cnt > 0)
            {
                lbFiles.BeginUpdate();
                for (int i = cnt - 1; i >= 0; i--)
                {
                    int idx = lbFiles.SelectedIndices[i];
                    lbFiles.Items.RemoveAt(idx);
                };
                lbFiles.EndUpdate();
            };
            //Update_Status();
        }

        //------------------------------------------------------------
        // If the user is selecting something ...
        // or we have changed the list
        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Update_Status();
        }

        //------------------------------------------------------------
        // Handle the Add button event
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                lbFiles.BeginUpdate();
                lbFiles.SelectedIndices.Clear();
                Add(dlgOpen.FileNames);
                lbFiles.EndUpdate();
            };
            //Update_Status();
        }

        //------------------------------------------------------------
        private void btnUp_Click(object sender, EventArgs e)
        {
            int cnt = lbFiles.SelectedIndices.Count;
            if (cnt > 0)
            {
                lbFiles.BeginUpdate();
                int idx_start = 0;
                for (int i=0; i < cnt; i++)
                {
                    int idx = lbFiles.SelectedIndices[i];
                    if (idx > idx_start)
                    {
                        lbFiles.SelectedIndices.Remove(idx);
                        object Item = lbFiles.Items[idx];
                        lbFiles.Items.RemoveAt(idx);
                        lbFiles.Items.Insert(idx - 1, Item);
                        lbFiles.SelectedIndices.Add(idx - 1);
                        idx_start = idx;
                    }
                    else
                    {
                        idx_start = idx+1;
                    };
                };
                lbFiles.EndUpdate();
            };
        }

        //------------------------------------------------------------
        private void btnDown_Click(object sender, EventArgs e)
        {
            int cnt = lbFiles.SelectedIndices.Count;
            if (cnt > 0)
            {
                lbFiles.BeginUpdate();
                int idx_start = lbFiles.Items.Count-1;
                for (int i = cnt-1; i >= 0; i--) 
                {
                    int idx = lbFiles.SelectedIndices[i];
                    if (idx < idx_start)
                    {
                        lbFiles.SelectedIndices.Remove(idx);
                        object Item = lbFiles.Items[idx];
                        lbFiles.Items.RemoveAt(idx);
                        lbFiles.Items.Insert(idx + 1, Item);
                        lbFiles.SelectedIndices.Add(idx + 1);
                        idx_start = idx;
                    }
                    else
                    {
                        idx_start = idx - 1;
                    };
                };
                lbFiles.EndUpdate();
            };
        }

        //------------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        //------------------------------------------------------------
        // calculate the realative path of a file
        private string RelativeFileName(string Origin, string Absolut)
        {
            string Org_Path = Path.GetDirectoryName(Origin);
            string Abs_Path = Path.GetDirectoryName(Absolut);
            string Name = Path.GetFileName(Absolut);
            if ( Org_Path == Abs_Path) return Name;

            string[] Org_Dirs = Org_Path.Split(Path.DirectorySeparatorChar);
            string[] Abs_Dirs = Abs_Path.Split(Path.DirectorySeparatorChar);
            
            int len = (Org_Dirs.Length<Abs_Dirs.Length) ? Org_Dirs.Length : Abs_Dirs.Length;
            int idx = 0;
            while ((idx < len) && (Org_Dirs[idx] == Abs_Dirs[idx])) idx++;

            string ret = "";
            for(int i = idx; i < Org_Dirs.Length; i++) ret += ".." + Path.DirectorySeparatorChar;
            for(int i = idx; i < Abs_Dirs.Length; i++) ret += Abs_Dirs[i] + Path.DirectorySeparatorChar;

            ret += Name;

            return ret;
        }

        //------------------------------------------------------------
        // save this list into a file
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                string file = dlgSave.FileName;
                if (File.Exists(file)) File.Delete(file);

                using (StreamWriter fs = File.CreateText(file))
                {
                    foreach (string song in lbFiles.Items)
                    {
                        fs.WriteLine(RelativeFileName(file, song));
                    };
                    fs.Close();
                };
            };
        }

        //------------------------------------------------------------
        // Handle the completion of a Drag&Drop on the listbox
        private void lbFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                lbFiles.BeginUpdate();
                lbFiles.SelectedIndices.Clear();
                Add((string[])e.Data.GetData(DataFormats.FileDrop));
                lbFiles.EndUpdate();
            }
            else
            if (e.Data.GetDataPresent(typeof(string[])))
            {
                lbFiles.BeginUpdate();
                lbFiles.SelectedIndices.Clear();
                Add((string[]) e.Data.GetData(typeof(string[])));
                lbFiles.EndUpdate();
            }
            //Update_Status();
        }

        //------------------------------------------------------------
        // Handle the request for Drag&Drop permission
        private void lbFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(typeof(string[])))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        //------------------------------------------------------------
        // Prepare the Drag&Drop operation as a SOURCE
        private void lbFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MButton) == MButton)
            {
                int idx = lbFiles.IndexFromPoint(e.X, e.Y);
                if (lbFiles.SelectedIndices.Contains(idx))
                {
                    Size DragSize = SystemInformation.DragSize;
                    rctDragBox = new Rectangle(new Point(e.X - (DragSize.Width / 2), e.Y - (DragSize.Height / 2)), DragSize);
                }
            }
            else
            {
                rctDragBox = Rectangle.Empty;
            }
        }

        //------------------------------------------------------------
        // this should end the drag and drop
        private void lbFiles_MouseUp(object sender, MouseEventArgs e)
        {
            rctDragBox = Rectangle.Empty;
        }

        //------------------------------------------------------------
        // start the Drag&Drop as a SOURCE
        private void lbFiles_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MButton) == MButton)
            {
                if (rctDragBox.Contains(e.X, e.Y))
                {
                    int cnt = lbFiles.SelectedItems.Count;
                    if (cnt > 0)
                    {
                        string[] Songs = new string[cnt];
                        for (int i = 0; i < cnt; i++) Songs[i] = (string) lbFiles.SelectedItems[i];

                        DragDropEffects dropEffect = lbFiles.DoDragDrop(Songs, DragDropEffects.All);
                        if (dropEffect == DragDropEffects.Move)
                        {
                            Delete();
                        };
                    }
                }
            }
        }
    }
}

//********************************************************************
// END OF FILE Form1
//********************************************************************
