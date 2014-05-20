//********************************************************************
// Figure - <type description here>
// (c) Apr 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;

//********************************************************************
namespace Taptris
{
    class Figure : Object
    {
        // publish the actual list
        public List<Point> Points;
        
        //------------------------------------------------------------
        // constructors
        public Figure()
        {
            Points = new List<Point>();
        }
        public Figure(Figure other)
        {
            Points = new List<Point>();
            Clone(other);
        }

        //------------------------------------------------------------
        // clone the other
        public void Clone(Figure other)
        {
            Points.Clear();
            Points.AddRange(other.Points);
        }

        //------------------------------------------------------------
        // support look up in lists
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Figure other = (Figure)obj;
            if (Points.Count != other.Points.Count) return false;
            foreach (Point P in Points)
            {
                if (other.Points.IndexOf(P) < 0) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int rc = 0;
            foreach (Point P in Points)
            {
                rc ^= (P.X << 16) | P.Y;
            }
            return rc;
        }

        //------------------------------------------------------------
        // the usual helpers
        public int  Count             { get { return Points.Count; } }
        public void Clear()           { Points.Clear(); }
        public bool Contains(Point P) { return (Points.IndexOf(P) >= 0); }

        public bool IsIn(Rectangle Rect)
        {
            foreach (Point P in Points)
            {
                if (!Rect.Contains(P)) return false;
            }
            return true;
        }

        public Rectangle Boundaries
        {
            get
            {
                Rectangle R = new Rectangle();

                if (Points.Count > 0) R.Location = Points[0];

                foreach (Point P in Points)
                {
                    if (P.X < R.Left) R.X = P.X;
                    if (P.X > R.Right) R.Width = P.X-R.X;
                    if (P.Y < R.Top) R.Y = P.Y;
                    if (P.Y > R.Bottom) R.Height = P.Y-R.Y;
                }
                return R;
            }
        }

        public bool Add(Point P)
        { 
            if (Points.IndexOf(P) >= 0) return false;
            Points.Add(P);
            return true;
        }
        
        public bool Add(int column, int row) { return Add(new Point(column, row)); }

        public void Move(int col, int row)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Point P = Points[i];
                P.X += col;
                P.Y += row;
                Points[i] = P;
            }
        }

        public void Turn(bool Direction)
        {
            if (Points.Count == 0) return;

            Rectangle Before = Boundaries;

            int c = 0;
            int r = 0;
            foreach (Point P in Points)
            {
                c += P.X;
                r += P.Y;
            }
            c /= Points.Count;
            r /= Points.Count;

            for (int i = 0; i < Points.Count; i++)
            {
                Point P = Points[i];
                int x = P.X - c;
                int y = P.Y - r;

                if (Direction)
                {
                    P.X = c - y;
                    P.Y = r + x;
                }
                else
                {
                    P.X = c + y;
                    P.Y = r - x;
                }

                Points[i] = P;
            }

            Rectangle After = Boundaries;
            Move(Before.Left - After.Left, Before.Top - After.Top);
        }
    }
}

//********************************************************************
// END OF FILE Figure
//********************************************************************
