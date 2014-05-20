using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Taptris
{
    public partial class frmMain : Form
    {
        //------------------------------------------------------------
        // Some Constants
        private const int Columns = 10;         // Field array
        private const int Rows = 20;

        //------------------------------------------------------------
        // the state variables
        private int w;                          // temp for current Width
        private int h;                          // temp for current Height
        private int Score;                      // the score
        private int Delay;                      // Counter in ticks before the selection moves one more
        private int Delay_Max;                  // ResetValue of the Delay;
        private bool Delay_Valid;               // when false, let the Selection fall free
        private Random rand;                    // for a little entropy
        private Brush[] Colors;                 // The colors to be used;
        private Brush[,] Fields;                // The colors of the fields
        private bool[] Falling;                 // decides if this column is to fall free 
        private List<Figure> Figures;           // The figure to let fall
        private Figure Selection;               // will hold the bricks that are currently falling
        private Figure TempSelection;           // used for calculations
        private Figure Deletion;                // will hold the fields to be cleared
        private List<Brush>  Selection_Colors;  // the color of the selection
        private Rectangle Desk;                 // the boundary Rectangle in Columns by Rows

        //------------------------------------------------------------
        public frmMain()
        {
            InitializeComponent();

            // Initilize the randomizer
            rand = new Random();

            // Init the fields
            Fields = new Brush[Columns, Rows];
            Falling = new bool[Columns];
            Colors = new Brush[] { Brushes.Pink, Brushes.Blue, Brushes.Yellow };
            Deletion = new Figure();
            Selection = new Figure();
            TempSelection = new Figure();
            Selection_Colors = new List<Brush>();

            Figures = new List<Figure>();
            FillFigures();

            // create a new desktop
            Desk = new Rectangle(0, 0, Columns, Rows);
            Update_Size();

            Reset();
        }

        //------------------------------------------------------------
        // Reset to Default
        public void Reset()
        {
            for (int c = 0; c < Columns; c++)
            {
                Falling[c] = false;
                for (int r = 0; r < Rows; r++)
                {
                    Fields[c, r] = null;
                }
            }
            Selection.Clear();
            Selection_Colors.Clear();

            Score = 0;

            // enable the timer
            Delay = 0;
            Delay_Max = 50;
            Delay_Valid = true;
            tmTick.Interval = 10;
            tmTick.Enabled = true;
        }

        public void FillFigures()
        {
            Figure f;

            int c = Columns / 2;
            Figures.Clear();
            
            // the bar
            f = new Figure();
            f.Add(c - 1, 0);
            f.Add(c, 0);
            f.Add(c + 1, 0);
            f.Add(c + 2, 0);
            Figures.Add(f);

            // the square
            f = new Figure();
            f.Add(c , 0);
            f.Add(c , 1);
            f.Add(c + 1, 0);
            f.Add(c + 1, 1);
            Figures.Add(f);

            // the left L
            f = new Figure();
            f.Add(c - 1, 0);
            f.Add(c - 1, 1);
            f.Add(c, 0);
            f.Add(c + 1, 0);
            Figures.Add(f);

            // the right L
            f = new Figure();
            f.Add(c - 1, 0);
            f.Add(c, 0);
            f.Add(c + 1, 0);
            f.Add(c + 1, 1);
            Figures.Add(f);

            // the left Hook
            f = new Figure();
            f.Add(c, 0);
            f.Add(c, 1);
            f.Add(c + 1, 1);
            f.Add(c + 1, 2);
            Figures.Add(f);

            // the right Hook
            f = new Figure();
            f.Add(c+1, 0);
            f.Add(c+1, 1);
            f.Add(c, 1);
            f.Add(c, 2);
            Figures.Add(f);

            // the half Cross
            f = new Figure();
            f.Add(c - 1, 0);
            f.Add(c , 0);
            f.Add(c+1, 0);
            f.Add(c, 1);
            Figures.Add(f);

        }

        //------------------------------------------------------------
        // adjust the drawing bitmap to the current size of the window
        private void Update_Size()
        {
            w = pbDesk.Width;
            h = pbDesk.Height;
            pbDesk.Image = new Bitmap(w, h);
        }

        //------------------------------------------------------------
        // little Helpers
        private int ToX(int col) { return col * w / Columns; }
        private int ToY(int row) { return row * h / Rows; }
        private int ToCol(int x) { return x * Columns / w; }
        private int ToRow(int y) { return y * Rows / h; }

        //------------------------------------------------------------
        // Draw the desktop
        private void Update_Desk()
        {
            Graphics g = Graphics.FromImage(pbDesk.Image);
            g.Clear(pbDesk.BackColor);

            // draw the grid
            // start with the columns
            for (int c = 1; c < Columns; c++)
            {
                int x = ToX(c);
                g.DrawLine(Pens.LightGray, x, 0, x, h);
            }

            // now the rows
            for (int r = 0; r < Rows; r++)
            {
                int y = ToY(r);
                g.DrawLine(Pens.LightGray, 0, y, w, y);
            }

            // now fill the fields
            int Number = 0; // Count the bricks
            int SizeX = ToX(1) - 1;
            int SizeY = ToY(1)-1;
            for (int c = 0; c < Columns; c++)
                for (int r = 0; r < Rows; r++)
                {
                    if (Fields[c, r] != null)
                    {
                        g.FillRectangle(Fields[c, r], ToX(c) + 1, ToY(r) + 1, SizeX, SizeY);
                        Number++;
                    };
                }

            for (int i = 0; i < Selection.Count; i++)
            {
                Point Brick = Selection.Points[i];
                g.FillRectangle(Selection_Colors[i], ToX(Brick.X) + 1, ToY(Brick.Y) + 1, SizeX, SizeY);
                Number++;
            }

            g.Dispose();
            pbDesk.Invalidate();
            lblBricks.Text = Number.ToString();
            lblScore.Text = Score.ToString();
        }

        //------------------------------------------------------------
        // react to a customer changing the size of the window
        private void pbDesk_SizeChanged(object sender, EventArgs e)
        {

            tmTick.Enabled = false;
            Update_Size();
            Update_Desk();
            tmTick.Enabled = true;
        }

        //------------------------------------------------------------
        // check if this Figure is still in the desk
        // and if it does not touch any bricks
        private bool CheckFigure(Figure Fig)
        {
            if (!Fig.IsIn(Desk)) return false;

            foreach (Point P in Fig.Points)
            {
                if (Fields[P.X, P.Y] != null) return false;
            }
            return true;
        }

        //------------------------------------------------------------
        // combine the selection with the desk
        private int StampSelection()
        {
            for (int i = 0; i < Selection.Count; i++)
            {
                Point Brick = Selection.Points[i];
                Fields[Brick.X, Brick.Y] = Selection_Colors[i];
            }
            Selection.Clear();
            Selection_Colors.Clear();
            return 0;
        }

        //------------------------------------------------------------
        // move the current selection falling
        private void MoveSelection(int ByColumns)
        {
            TempSelection.Clone(Selection);
            TempSelection.Move(ByColumns, 0);

            if (!CheckFigure(TempSelection)) return;

            Selection.Clone(TempSelection);
        }

        //------------------------------------------------------------
        // turn the current selection
        private void TurnSelection(bool Direction)
        {
            TempSelection.Clone(Selection);
            TempSelection.Turn(Direction);

            if (!CheckFigure(TempSelection)) return;

            Selection.Clone(TempSelection);
        }

        //------------------------------------------------------------
        // let the bricks fall
        private int FallSelection()
        {
            Delay--;
            if (!Delay_Valid || Delay <= 0 )
            {
                Delay = Delay_Max;
                TempSelection.Clone(Selection);
                TempSelection.Move(0, 1);

                if (!CheckFigure(TempSelection)) return StampSelection();

                Selection.Clone(TempSelection);
            }
            return Selection.Count;
        }

        //------------------------------------------------------------
        // let the ground be solid again
        private int FallFields()
        {
            int count = 0;
            for (int c = 0; c < Columns; c++)
            {
                if (Falling[c])
                {
                    for (int r = Rows-2; r >=0; r--)
                    {
                        if ((Fields[c, r] != null) && (Fields[c, r + 1] == null))
                        {
                            Fields[c, r + 1] = Fields[c, r];
                            Fields[c, r] = null;
                            count++;
                        }
                    }
                }
                else
                {
                    if (Fields[c, Rows - 1] == null) continue;

                    for (int r = Rows - 2; r >= 0; r--)
                    {
                        if (Fields[c, r] == null) break;
                        if (Fields[c, r + 1] == null)
                        {
                            Fields[c, r + 1] = Fields[c, r];
                            Fields[c, r] = null;
                            count++;
                        }
                    }
                }
            }
            //for (int y = Rows - 2; y >= 0; y--)
            //    for (int x = 0; x < Columns; x++)
            //    {
            //        if ((Fields[x, y] != null) && (Fields[x, y + 1] == null))
            //        {
            //            Fields[x, y + 1] = Fields[x, y];
            //            Fields[x, y] = null;
            //            count++;
            //        }
            //    }
            return count;
        }

        //------------------------------------------------------------
        // check if a line is filled with bricks
        private int CheckLine()
        {
            int cnt = 0;
            for (int r = 0; r < Rows; r++)
            {
                bool full = true;
                for (int c = 0; c < Columns; c++)
                {
                    if (Fields[c, r] == null)
                    {
                        full = false;
                        break;
                    }
                }
                if (full)
                {
                    cnt++;
                    for (int c = 0; c < Columns; c++)
                    {
                        Falling[c] = true;
                        Fields[c, r] = null;
                    }
                }
            }
            return cnt;
        }

        //------------------------------------------------------------
        // Calc the next state
        private void tmTick_Tick(object sender, EventArgs e)
        {
            // Let the bricks fall
            if ( FallSelection() + FallFields() == 0) // nothing has moved
            {
                for (int c = 0; c < Columns; c++)
                {
                    Falling[c] = false;
                }

                // Check if we have full lines
                int cnt = CheckLine();
                if (cnt > 0) Score += Columns * (1 << (cnt - 1));

                // and add some new ;-)
                Delay_Valid = true;
                Add();
            }
            Update_Desk();
        }

        //------------------------------------------------------------
        private void GameOver()
        {
            tmTick.Enabled = false;
            MessageBox.Show("Game over!\nScore: " + Score.ToString());
            Reset();
        }

        //------------------------------------------------------------
        // let it snow
        private void Add()
        {
            int f = rand.Next(Figures.Count);
            if (!CheckFigure(Figures[f])) GameOver();

            Selection.Clone(Figures[f]);
            Selection_Colors.Clear();
            for (int i = 0; i < Selection.Count; i++)
            {
                Selection_Colors.Add(Colors[rand.Next(Colors.Length)]);
            }
        }
        
        //------------------------------------------------------------
        // functions to clear the fields
        private void AddToDeletion(int col, int row, Brush Color)
        {
            if (col >= 0 && col < Columns && row >= 0 && row < Rows)
            {
                if (Color == null) Color = Fields[col, row];
                if ((Fields[col, row] == Color) && Deletion.Add(col,row))
                {
                    AddToDeletion(col, row - 1, Color);
                    AddToDeletion(col, row + 1, Color);
                    AddToDeletion(col - 1, row, Color);
                    AddToDeletion(col + 1, row, Color);
                };
            }
        }

        private void Delete(int col, int row)
        {
            Deletion.Clear();
            AddToDeletion(col, row, null);
            if (Deletion.Count > 3)
            {
                foreach (Point Brick in Deletion.Points)
                {
                    Fields[Brick.X, Brick.Y] = null;
                }

                Score += Deletion.Count * 5;
            }
        }

        //------------------------------------------------------------
        // MouseButton clicked
        private void pbDesk_MouseUp(object sender, MouseEventArgs e)
        {
            int c = ToCol(e.X);
            int r = ToRow(e.Y);

            if (c >= 0 && c < Columns && r >= 0 && r < Rows)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (Fields[c, r] != null) Delete(c, r);
                        break;
                    case MouseButtons.Right:
                        Delay_Valid = false;
                        break;
                }
            }
        }
        
        //------------------------------------------------------------
        // a key was pressed
        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case ' ': Delay_Valid = false; break;
                case 'a': MoveSelection(-1); break;
                case 'd': MoveSelection(1); break;
                case 'w': TurnSelection(true); break;
                case 's': TurnSelection(false); break;
                default:
                    break;
            }
        }
    }
}
