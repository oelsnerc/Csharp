//********************************************************************
// SuDoKu - Represents the states of all fields of a Sudoku
// Note: A normal Sudoku (9x9) has the dimension 3
// (c) Mrz 2011 MMC
//********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

//********************************************************************
namespace MMC_Controls
{
    public partial class SuDoKu : UserControl
    {
        SuDoKu_Field ivSudoku;

        //************************************************************
        // things you need to paint
        protected Pen ivLine_Normal;                // for the grid
        protected Pen ivLine_Thick;                 // for the grid
        protected Brush ivBrush;                    // the color of the numbers
        protected Rectangle ivFieldUpperLeft;       // the field upper left
        protected Font ivFont;                      // the font of the numbers in the fields
        protected StringFormat ivFormat;            // how to print the numbers within the fields
        protected Stack<SuDoKu_Field> ivHistory;    // store the last Sudokus

        //************************************************************
        // Properties
        [Category("Sudoku properties")]
        [DefaultValue(3)]
        [Description("Defines the dimension of the Sudoku")]
        public int Dimension
        {
            get { return ivSudoku.Dimension; }
            set
            {
                ivSudoku = new SuDoKu_Field(value);
                ivHistory.Clear();
                this.Invalidate();
            }
        }

        //------------------------------------------------------------
        protected Color _GridColor = Color.Black;   // The Color of the Grid
        [Category("Sudoku properties")]
        [DefaultValue(typeof(Color), "Black")]
        [Description("Defines the grid color")]
        public Color GridColor
        {
            get { return _GridColor; }
            set
            {
                _GridColor = value;
                ivLine_Normal = new Pen(_GridColor, 2);
                ivLine_Thick = new Pen(_GridColor, 4);
                this.Invalidate();
            }
        }

        //************************************************************
        // the constructor
        public SuDoKu()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            ivBrush = new SolidBrush(this.ForeColor);
            ivLine_Normal = new Pen(_GridColor, 2);
            ivLine_Thick = new Pen(_GridColor, 4);

            ivFormat = new StringFormat(StringFormatFlags.NoClip);
            ivFormat.Alignment = StringAlignment.Center;
            ivFormat.LineAlignment = StringAlignment.Center;

            ivSudoku = new SuDoKu_Field(3);

            ivHistory = new Stack<SuDoKu_Field>();

            UpdateDimensions();
        }

        //************************************************************
        public void SaveToFile(string FileName)
        {
            if (File.Exists(FileName)) File.Delete(FileName);

            using (StreamWriter fs = File.CreateText(FileName))
            {
                fs.Write(ivSudoku.ToString());
                fs.Close();
            };
        }

        //------------------------------------------------------------
        public void LoadFromFile(string FileName)
        {
            using (StreamReader fs = File.OpenText(FileName))
            {
                ivSudoku.FromString(fs.ReadToEnd());
                fs.Close();
            };
        }

        //------------------------------------------------------------
        protected void UpdateDimensions()
        {
            int w = this.Width;
            int h = this.Height;
            int _Size = ivSudoku.GroupSize;

            int w_Field = w / _Size;
            int h_Field = h / _Size;

            int x_Start = (w - _Size * w_Field) / 2;
            int y_Start = (h - _Size * h_Field) / 2;

            ivFieldUpperLeft = new Rectangle(x_Start, y_Start, w_Field, h_Field);
            ivFont = new Font("Arial", h_Field / 2);
        }

        //------------------------------------------------------------
        protected void Draw_Squares(Graphics g)
        {
            int _Size = ivSudoku.GroupSize;
            Rectangle RectToDraw = new Rectangle(ivFieldUpperLeft.Location, ivFieldUpperLeft.Size);

            for (int row = 0; row < _Size; ++row)
            {
                for (int column = 0; column < _Size; ++column)
                {
                    int cnt = ivSudoku.Options_Count(row, column);
                    if (cnt <= _Size / 2)
                    {
                        Brush B;
                        switch (cnt)
                        {
                            case 3: B = Brushes.LightGreen; break;
                            case 2: B = Brushes.LightSalmon; break;
                            case 1: B = Brushes.LightPink; break;
                            case 0: B = Brushes.DarkRed; break;
                            default: B = Brushes.LightBlue; break;
                        }
                        g.FillRectangle(B, RectToDraw);
                    }

                    if (cnt == 1)
                    {
                        Brush B = (ivSudoku.isCalculated(row, column)) ? Brushes.Red : ivBrush;
                        g.DrawString(ivSudoku[row,column].ToString(), ivFont, B, RectToDraw, ivFormat);
                    }
                    RectToDraw.X += ivFieldUpperLeft.Width;
                }
                RectToDraw.Y += ivFieldUpperLeft.Height;
                RectToDraw.X = ivFieldUpperLeft.X;
            }
        }

        //------------------------------------------------------------
        protected void Draw_Grid(Graphics g)
        {
            int x_end = this.Width - ivFieldUpperLeft.X;
            int y_end = this.Height - ivFieldUpperLeft.Y;

            // Draw Columns
            for (int x = ivFieldUpperLeft.X; x <= x_end; x += ivFieldUpperLeft.Width)
            {
                g.DrawLine(ivLine_Normal, x, ivFieldUpperLeft.Y, x, y_end);
            }

            // draw Rows
            for (int y = ivFieldUpperLeft.Y; y <= y_end; y += ivFieldUpperLeft.Height)
            {
                g.DrawLine(ivLine_Normal, ivFieldUpperLeft.X, y, x_end, y);
            }

            // draw the groups
            int _Dimension = ivSudoku.Dimension;
            for (int n = 1; n < _Dimension; n++)
            {
                int x = ivFieldUpperLeft.X + n * ivFieldUpperLeft.Width * _Dimension;
                int y = ivFieldUpperLeft.Y + n * ivFieldUpperLeft.Height * _Dimension;
                g.DrawLine(ivLine_Thick, x, ivFieldUpperLeft.Y, x, y_end);
                g.DrawLine(ivLine_Thick, ivFieldUpperLeft.X, y, x_end, y);
            }

        }

        //************************************************************
        // draw the Sudoku
        protected void Draw(Graphics g)
        {
            Draw_Squares(g);
            Draw_Grid(g);
        }

        //------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics);
            base.OnPaint(e);
        }

        protected override void OnForeColorChanged(System.EventArgs e)
        {
            ivBrush = new SolidBrush(this.ForeColor);
            base.OnForeColorChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateDimensions();
            Invalidate();
            base.OnSizeChanged(e);
        }

        public Point ToIndex(int X, int Y)
        {
            int column = (X - ivFieldUpperLeft.X) / ivFieldUpperLeft.Width;
            int row = (Y - ivFieldUpperLeft.Y) / ivFieldUpperLeft.Height;

            return new Point(row, column);
        }

        //************************************************************
        // Get and Set a value
        //public int this[int row, int column]
        //{
        //    get { return ivSudoku[row, column]; }
        //    set
        //    {
        //        ivSudoku[row, column] = value;
        //        ivSudoku.setCalculated(row, column, false);
        //        ivSudoku.Solve_Ones();
        //    }
        //}
        public bool setValue(int row, int column, int value)
        {
            ivSudoku[row, column] = value;
            ivSudoku.setCalculated(row, column, false);
            return ivSudoku.Solve_Ones();
        }

        public List<int> Options(int x, int y) { return ivSudoku.Options(x, y); }

        //************************************************************
        // History
        public void SaveState()
        {
            ivHistory.Push(new SuDoKu_Field(ivSudoku));
        }

        public void Undo()
        { 
            if (ivHistory.Count > 0)
            {
                ivSudoku = ivHistory.Pop();
                Invalidate();
            }
        }
    }
}

//********************************************************************
// END OF FILE SuDoKu
//********************************************************************
