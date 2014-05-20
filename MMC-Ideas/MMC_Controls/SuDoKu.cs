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
        SuDoKu_Field _Sudoku;

        //************************************************************
        // things you need to paint
        protected Pen _Line_Normal;                 // for the grid
        protected Pen _Line_Thick;                  // for the grid
        protected Brush _Brush;                     // the color of the numbers

        //************************************************************
        // Properties
        [Category("Sudoku properties")]
        [DefaultValue(3)]
        [Description("Defines the dimension of the Sudoku")]
        public int Dimension
        {
            get { return _Sudoku.Dimension; }
            set { _Sudoku.Dimension = value; this.Invalidate(); }
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
                _Line_Normal = new Pen(_GridColor, 2);
                _Line_Thick = new Pen(_GridColor, 4);
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

            _Brush = new SolidBrush(this.ForeColor);
            _Line_Normal = new Pen(_GridColor, 2);
            _Line_Thick = new Pen(_GridColor, 4);

            _Sudoku = new SuDoKu_Field(3);
        }

        //************************************************************
        public void SaveToFile(string FileName)
        {
            if (File.Exists(FileName)) File.Delete(FileName);

            using (StreamWriter fs = File.CreateText(FileName))
            {
                fs.Write(_Sudoku.ToString());
                fs.Close();
            };
        }

        //------------------------------------------------------------
        public void LoadFromFile(string FileName)
        {
            using (StreamReader fs = File.OpenText(FileName))
            {
                _Sudoku.FromString(fs.ReadToEnd());
                fs.Close();
            };
        }

        //************************************************************
        // draw the Sudoku
        protected void Draw(Graphics g)
        {
            int w = this.Width;
            int h = this.Height;
            int _Size = _Sudoku.Size;

            int w_Field = w / _Size;
            int h_Field = h / _Size;

            int x_Start = (w - _Size * w_Field) / 2;
            int y_Start = (h - _Size * h_Field) / 2;

            int x_End = w - x_Start;
            int y_End = h - y_Start;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // first draw the squares
            Font font = new Font("Arial", h_Field/2);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            for (int x = 0; x < _Size; x++)
                for (int y = 0; y < _Size; y++)
                {
                    Rectangle R = new Rectangle(x_Start + x * w_Field, y_Start + y * h_Field, w_Field, h_Field);
                    int Value = _Sudoku[x, y];
                    int cnt = _Sudoku.Options_Count(x, y);
                    if (cnt <= _Size / 2)
                    {
                        Brush B;
                        switch (cnt)
	                    {
                            case 3 : B = Brushes.LightGreen; break;
                            case 2 : B = Brushes.LightSalmon; break;
                            case 1 : B = Brushes.LightPink; break;
                            case 0 : B = (Value == 0) ? Brushes.DarkRed : Brushes.LightPink; break;
		                    default: B = Brushes.LightBlue; break;
                        }
                        g.FillRectangle(B, R);
                    }

                    if (Value > 0)
                    {
                        Brush B = (_Sudoku.IsCalculated(x,y)) ? Brushes.Red : _Brush;
                        g.DrawString(Value.ToString(), font, B, R, format);
                    }
                }

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // now the grid
            for (int x = x_Start; x <= x_End; x += w_Field)
            {
                g.DrawLine(_Line_Normal, x, y_Start, x, y_End);
            }

            for (int y = y_Start; y <= y_End; y += h_Field)
            {
                g.DrawLine(_Line_Normal, x_Start, y, x_End, y);
            }

            int _Dimension = _Sudoku.Dimension;
            for (int n = 1; n < _Dimension; n++)
            {
                int x = x_Start + n * w_Field * _Dimension;
                int y = y_Start + n * h_Field * _Dimension;
                g.DrawLine(_Line_Thick, x, y_Start, x, y_End);
                g.DrawLine(_Line_Thick, x_Start, y, x_End, y);
            }

        }

        //------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics);
            base.OnPaint(e);
        }

        protected override void OnForeColorChanged(System.EventArgs e)
        {
            _Brush = new SolidBrush(this.ForeColor);
            base.OnForeColorChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Invalidate();
            base.OnSizeChanged(e);
        }

        public Point ToIndex(int X, int Y)
        {
            int _Size = _Sudoku.Size;
            int w_Field = Width / _Size;
            int h_Field = Height / _Size;

            int x_Start = (Width - _Size * w_Field) / 2;
            int y_Start = (Height - _Size * h_Field) / 2;

            return new Point((X - x_Start) / w_Field, (Y - y_Start) / h_Field);
        }

        //************************************************************
        // Get and Set a value
        public int this[int X, int Y]
        {
            get { return _Sudoku[X, Y]; }
            set { _Sudoku[X, Y] = value; }
        }
        public List<int> Options(int x, int y) { return _Sudoku.Options(x,y); }
        public void SaveState() { _Sudoku.SaveState(); }
        public void Undo() { _Sudoku.Undo(); }
        public string History { get { return _Sudoku.CommandHistory; } }
        
        public bool Solve()
        {
            //return _Sudoku.Solve_One();
            return _Sudoku.Solve_Two(0);
        }

        public void Normalize()
        {
            _Sudoku.Normalize();
        }
    }
}

//********************************************************************
// END OF FILE SuDoKu
//********************************************************************
