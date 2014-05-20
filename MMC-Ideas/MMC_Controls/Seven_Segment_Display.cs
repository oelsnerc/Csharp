using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMC_Controls
{
    public partial class Seven_Segment_Display : UserControl
    {
        // this will hold the polygons to draw each segment
        private struct Segment
        {
            public Point [] A;
            public Point [] B;
            public Point [] C;
            public Point [] D;
            public Point [] E;
            public Point [] F;
            public Point [] G;
        }

        private SolidBrush _Brush;
        private Segment[] _Areas;

        //------------------------------------------------------------
        public Seven_Segment_Display()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            _Brush = new SolidBrush(this.ForeColor);
            ReflectChanges();
        }

        //************************************************************
        protected void CalcSegements(int digit, Rectangle Area)
        {
            // calc the points for the 7 hexagons
            int X1 = Area.Left;
            int X2 = X1 + _Thickness / 2;
            int X3 = X1 + _Thickness;
            int X6 = Area.Right;
            int X5 = X6 - _Thickness / 2;
            int X4 = X6 - _Thickness;

            int Y1 = Area.Top;
            int Y2 = Y1 + _Thickness / 2;
            int Y3 = Y1 + _Thickness;
            
            int Y5 = (Area.Top + Area.Bottom) / 2;
            int Y4 = Y5 - _Thickness / 2;
            int Y6 = Y5 + _Thickness / 2;

            int Y9 = Area.Bottom;
            int Y8 = Y9 - _Thickness / 2;
            int Y7 = Y9 - _Thickness;

            Segment S = new Segment();
            S.A = new Point[6];
            S.A[0] = new Point(X2, Y2); S.A[1] = new Point(X3, Y1); S.A[2] = new Point(X4, Y1);
            S.A[3] = new Point(X5, Y2); S.A[4] = new Point(X4, Y3); S.A[5] = new Point(X3, Y3);

            S.B = new Point[6];
            S.B[0] = new Point(X5, Y2); S.B[1] = new Point(X6, Y3); S.B[2] = new Point(X6, Y4);
            S.B[3] = new Point(X5, Y5); S.B[4] = new Point(X4, Y4); S.B[5] = new Point(X4, Y3);

            S.C = new Point[6];
            S.C[0] = new Point(X5, Y5); S.C[1] = new Point(X6, Y6); S.C[2] = new Point(X6, Y7);
            S.C[3] = new Point(X5, Y8); S.C[4] = new Point(X4, Y7); S.C[5] = new Point(X4, Y6);

            S.D = new Point[6];
            S.D[0] = new Point(X2, Y8); S.D[1] = new Point(X3, Y7); S.D[2] = new Point(X4, Y7);
            S.D[3] = new Point(X5, Y8); S.D[4] = new Point(X4, Y9); S.D[5] = new Point(X3, Y9);

            S.E = new Point[6];
            S.E[0] = new Point(X2, Y5); S.E[1] = new Point(X3, Y6); S.E[2] = new Point(X3, Y7);
            S.E[3] = new Point(X2, Y8); S.E[4] = new Point(X1, Y7); S.E[5] = new Point(X1, Y6);

            S.F = new Point[6];
            S.F[0] = new Point(X2, Y2); S.F[1] = new Point(X3, Y3); S.F[2] = new Point(X3, Y4);
            S.F[3] = new Point(X2, Y5); S.F[4] = new Point(X1, Y4); S.F[5] = new Point(X1, Y3);

            S.G = new Point[6];
            S.G[0] = new Point(X2, Y5); S.G[1] = new Point(X3, Y4); S.G[2] = new Point(X4, Y4);
            S.G[3] = new Point(X5, Y5); S.G[4] = new Point(X4, Y6); S.G[5] = new Point(X3, Y6);

            _Areas[digit] = S;
        }

        //------------------------------------------------------------
        protected void ReflectChanges()
        {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // the segements for each digit
            _Areas = new Segment[_DigitCount];

            int DigitSpace = 2 * _Thickness;
            int SpaceLeft = this.Width - (_DigitCount - 1) * DigitSpace;
            int DigitWidth = SpaceLeft / _DigitCount;
            int X = (SpaceLeft - _DigitCount * DigitWidth) / 2;

            int Step = DigitWidth + DigitSpace;
            for (int i = _DigitCount-1; i >=0; i--)
            {
                CalcSegements(i, new Rectangle(X, 0, DigitWidth, this.Height));
                X += Step;
            }
        }

        //------------------------------------------------------------
        // we don't need this, we use ForeColor instead
        //private Color _Color=Color.Black;
        //[Category("Segment properties")]
        //[DefaultValue(typeof(Color), "Black")]
        //[Description("Defines the color of each segment")]
        //public Color SegmentColor
        //{
        //    get { return _Color; }
        //    set { _Color = value; this.Invalidate(); }
        //}

        //------------------------------------------------------------
        private int _Thickness=5;
        [Category("Segment properties")]
        [DefaultValue(5)]
        [Description("Defines how thick each segment is drawn")]
        public int Thickness
        {
            get { return _Thickness; }
            set
            {
                _Thickness = (value > 0) ? value : 1; ReflectChanges(); this.Invalidate();
            }
        }

        //------------------------------------------------------------
        private int _DigitCount = 1;
        [Category("Segment properties")]
        [DefaultValue(1)]
        [Description("Defines how many digits are displayed")]
        public int Digits
        {
            get { return _DigitCount; }
            set
            {
                _DigitCount = (value > 0) ? value : 1; ReflectChanges(); this.Invalidate();
            }
        }

        //------------------------------------------------------------
        private int _Value = 8888;
        [Category("Segment properties")]
        [DefaultValue(8888)]
        [Description("Defines the value to be displayed")]
        public int Value
        {
            get { return _Value ; }
            set
            {
                if (_Value != value)
                {
                    _Value = value; this.Invalidate();
                }
            }
        }

        //************************************************************
        protected override void OnSizeChanged(EventArgs e)
        {
            ReflectChanges();
            base.OnSizeChanged(e);
        }

        //------------------------------------------------------------
        protected override void OnForeColorChanged(EventArgs e)
        {
            _Brush = new SolidBrush(this.ForeColor);
            base.OnForeColorChanged(e);
        }

        //************************************************************
        //  ---A---     A = 1
        // |       |    B = 2
        // F       B    C = 4
        // |       |    D = 8
        //  ---G---     E = 16
        // |       |    F = 32
        // E       C    G = 64
        // |       |
        //  ---D---
        //************************************************************
        protected int Value2Code(int Value)
        {
            switch (Value)
            {
                case 0: return 0x3F;
                case 1: return 0x06;
                case 2: return 0x5B;
                case 3: return 0x4F;
                case 4: return 0x66;
                case 5: return 0x6D;
                case 6: return 0x7D;
                case 7: return 0x07;
                case 8: return 0x7F;
                case 9: return 0x6F;
            }
            return 0;
        }

        //------------------------------------------------------------
        protected void DrawDigit(Graphics g, int digit)
        {
            Segment Area = _Areas[digit];

            int Code = _Value;
            for (; digit > 0; digit--) { Code /= 10; }
            Code = Value2Code(Code % 10);

            if ((Code & 1) > 0) g.FillPolygon(_Brush, Area.A);
            if ((Code & 2) > 0) g.FillPolygon(_Brush, Area.B);
            if ((Code & 4) > 0) g.FillPolygon(_Brush, Area.C);
            if ((Code & 8) > 0) g.FillPolygon(_Brush, Area.D);
            if ((Code & 16) > 0) g.FillPolygon(_Brush, Area.E);
            if ((Code & 32) > 0) g.FillPolygon(_Brush, Area.F);
            if ((Code & 64) > 0) g.FillPolygon(_Brush, Area.G);
        }

        //************************************************************
        protected override void OnPaint(PaintEventArgs pe)
        {
            for (int i = 0; i < _DigitCount; i++)
            {
                DrawDigit(pe.Graphics, i);
            }

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            base.OnPaint(pe);
        }
    }
}
