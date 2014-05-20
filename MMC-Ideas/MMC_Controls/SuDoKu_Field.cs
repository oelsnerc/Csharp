//********************************************************************
// SuDoKu_Field - <type description here>
// (c) Mrz 2011 MMC
//********************************************************************
using System.Collections.Generic;
using System.Linq;

namespace MMC_Controls
{
    public class SuDoKu_Field
    {
        protected SuDoKu_Cell[,] Cells;             // all the fields
        protected Stack<SuDoKu_Cell[,]> _History;   // keep all previous states
        protected int _Dimension;                   // the Sudokus Dimension
        protected int _Size;                        // the number of fields in a row/column/square

        protected string _Commands;                 // the string-presentation of executed changes
        protected enum SuDoKu_Commands
	    {
            None = 0,
            Set,
            Swap_Values,
            Swap_Rows,
            Swap_SquareRows,
            Max
	    }

        //************************************************************
        public SuDoKu_Field(int MyDimension)
        {
            _History = new Stack<SuDoKu_Cell[,]>();
            Dimension = MyDimension;                // This will call ReflectChanges();
        }

        //************************************************************
        protected void ReflectChanges()
        {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // init the properties
            _History.Clear();
            _Commands = "";
            _Dimension = Dimension;
            _Size = _Dimension * _Dimension;
            Cells = new SuDoKu_Cell[_Size, _Size];

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // init the Fields
            for (int x = 0; x < _Size; x++)
                for (int y = 0; y < _Size; y++)
                {
                    Cells[x, y] = new SuDoKu_Cell(_Size);
                }
        }

        //------------------------------------------------------------
        // Keep History
        public void SaveState()
        {
            _History.Push(Copy(Cells));
        }

        //------------------------------------------------------------
        public void Undo()
        {
            if (_History.Count > 0)
            {
                Cells = _History.Pop();
            }
        }

        //------------------------------------------------------------
        public static SuDoKu_Cell[,] Copy(SuDoKu_Cell[,] other)
        {
            int MySize = other.GetLength(0);
            SuDoKu_Cell[,] MyCells = new SuDoKu_Cell[MySize,MySize];
            for (int x = 0; x < MySize; x++)
            {
                for (int y = 0; y < MySize; y++)
                {
                    MyCells[x, y].CopyFrom(ref other[x, y]);
                }
            }
            return MyCells;
        }

        //------------------------------------------------------------
        public override string ToString()
        {
            string rc = _Dimension.ToString();
            for (int x = 0; x < _Size; x++)
            {
                for (int y = 0; y < _Size; y++)
                {
                    rc += "\n" + Cells[x, y].ToString();
                }
            }
            return rc;
        }

        //------------------------------------------------------------
        public void FromString(string Data)
        {
            string[] Fields = Data.Split('\n');
            _Dimension = int.Parse(Fields[0]);
            ReflectChanges();
            int i = 1;
            for (int x = 0; x < _Size; x++)
            {
                for (int y = 0; y < _Size; y++)
                {
                    Cells[x, y].FromString(Fields[i]);
                    i++;
                }
            }
        }

        //------------------------------------------------------------
        public string CommandHistory { get { return _Commands; } }
        protected void Command_Add(SuDoKu_Commands cmd, params object[] args)
        {
            _Commands += ((int)cmd).ToString();
            foreach (object o in args) _Commands += ',' + o.ToString();
            _Commands += ';';
        }

        //************************************************************
        public int Dimension
        {
            get { return _Dimension; }
            set { _Dimension = (value > 1) ? value : 3; ReflectChanges(); }
        }
        public int Size { get { return _Size; } }
        public List<int> Options(int X, int Y) { return Cells[X, Y].Options_List; }
        public int Options_Count(int X, int Y) { return Cells[X,Y].Options_Count; }
        public bool IsCalculated(int X, int Y) { return Cells[X, Y].Calculated; }
        public int this[int X, int Y]
        {
            get { return Cells[X, Y].Value; }
            set
            {
                Command_Add(SuDoKu_Commands.Set, X, Y, value);
                SetValue(X, Y, value);
            }
        }

        //************************************************************
        private void SetValue(int X,int Y,int Value)
        {
            if (Value < 1 || Cells[X, Y].Value > 0)
            {   //Unset the Value
                Option_Set(X, Y, Cells[X, Y].Value, true);
            }

            if (Value > 0)
            {   // Set the Value
                Option_Set(X, Y, Value, false);
            }
        }

        //------------------------------------------------------------
        // Set the option in all relevant neighbours
        // i.e. Set the value if Option == false
        // or set it to zero if Option == true
        private void Option_Set(int X, int Y, int Value, bool Option)
        {
            Cells[X, Y].Value = Option ? 0 : Value;
            
            int X_Left = (X / _Dimension) * _Dimension;
            int X_Right = X_Left + _Dimension;
            int Y_Top = (Y / _Dimension) * _Dimension;
            int Y_Bottom = Y_Top + _Dimension;
            Value--;

            // remove horizontal
            for (int x = 0; x < X_Left; x++) Cells[x, Y].Options_Set(Value,Option);
            for (int x = X_Right; x < _Size; x++) Cells[x, Y].Options_Set(Value, Option);
            // remove vertical
            for (int y = 0; y < Y_Top; y++) Cells[X, y].Options_Set(Value, Option);
            for (int y = Y_Bottom; y < _Size; y++) Cells[X, y].Options_Set(Value, Option);
            // remove squarical *g*
            for (int x = X_Left; x < X_Right; x++)
                for (int y = Y_Top; y < Y_Bottom; y++)
                    Cells[x, y].Options_Set(Value, Option);
        }

        //------------------------------------------------------------
        public bool Valid
        {
            get
            {
                for (int x = 0; x < _Size; x++)
                    for (int y = 0; y < _Size; y++)
                    {
                        if (Cells[x, y].Options_Count == 0 && Cells[x, y].Value == 0) return false;
                    }
                return true;
            }
        }

        //************************************************************
        // Try to solve it for the fields that have only one option left
        public bool Solve_One()
        {
            int cnt;
            do
            {
                cnt = 0;
                for (int x = 0; x < _Size; x++)
                    for (int y = 0; y < _Size; y++)
                    {
                        if (Cells[x, y].Value == 0)
                        {
                            int ocount = Cells[x, y].Options_Count;
                            if (ocount == 0) return false;
                            if (ocount == 1)
                            {
                                cnt++;
                                int Value = Cells[x, y].Options_First;
                                Option_Set(x, y, Value, false);
                                Cells[x, y].Calculated = true;
                            }
                        }
                    }
            } while (cnt > 0);
            return true;
        }

        //------------------------------------------------------------
        // Try to solve it with all field having 2 options left
        public bool Solve_Two(int depth)
        {
            // keep the calculation time short
            // I found a depth of 2 is enough
            depth++; if (depth > 2) return Solve_One();

            int cnt;
            do
            {
                if (!Solve_One()) return false;
                cnt = 0;
                for (int x = 0; x < _Size; x++)
                    for (int y = 0; y < _Size; y++)
                    {
                        if (Cells[x, y].Value == 0)
                        {
                            int ocount = Cells[x, y].Options_Count;
                            if (ocount == 0) return false;
                            if (ocount == 2)
                            {
                                List<int> L = Cells[x, y].Options_List;

                                // Check First Value
                                SuDoKu_Cell[,] Old = Cells;         // Save the old state
                                Cells = Copy(Cells);                // work on the copy
                                Option_Set(x, y, L[0], false);      // set the value
                                Cells[x, y].Calculated = true;
                                bool r1 = Solve_Two(depth);         // and solve it
                                
                                // Check the 2nd Value
                                SuDoKu_Cell[,] New = Cells;         // keep the result
                                Cells = Copy(Old);                  // reset to the original state
                                Option_Set(x, y, L[1], false);      // set the value
                                Cells[x, y].Calculated = true;
                                bool r2 = Solve_Two(depth);         // and solve it

                                // now analyze the results
                                if (r2 == r1)                       // we have a solution if one of the 2 lead to a contradiction
                                {
                                    Cells = Old;                    // no conclusion possible .. so reset the old state
                                    if (!r1) return false;          // there is no valid way out of here
                                    //return r2;                    // stop searching here
                                }
                                else
                                {
                                    if (r1) Cells = New;            // if r1 was valid take that (else leave the state as is)
                                    cnt++;
                                }
                            }
                        }
                    }
            } while (cnt > 0);
            return true;
        }

        //************************************************************
        // Normalize helper
        protected void Swap_Values(int V1, int V2)
        {
            if (V1 != V2)
            {
                Command_Add(SuDoKu_Commands.Swap_Values, V1, V2);
                for (int x = 0; x < _Size; x++)
                    for (int y = 0; y < _Size; y++)
                        Cells[x, y].Option_Swap(V1, V2);
            }
        }

        protected void Swap_Rows(int Y1, int Y2)
        {
            if (Y1 != Y2)
            {
                Command_Add(SuDoKu_Commands.Swap_Rows, Y1, Y2);
                for (int x = 0; x < _Size; x++)
                    Cells[x, Y1].SwapWith(ref Cells[x, Y2]);
            }
        }

        protected void Swap_SquareRows(int Y1, int Y2)
        {
            if (Y1 != Y2)
            {
                Command_Add(SuDoKu_Commands.Swap_SquareRows, Y1, Y2);
                for (int i = 0; i < _Dimension; i++)
                    Swap_Rows(Y1 + i, Y2 + i);
            }
        }

        protected int FindRowMin(int Y_Bgn, int Y_End)
        {
            int idx = Y_Bgn;
            int min = Cells[0,idx].Value;
            for (int y = Y_Bgn+1; y < Y_End; y++)
			{
                int v = Cells[0,y].Value;
                if (v == 0) return Y_Bgn;
                if (v < min) { idx = y; min = v; }
			}
            return idx;
        }

        protected int FindSquareMin(int Y_Bgn)
        {
            int idx = Y_Bgn;
            int min = Cells[0, idx].Value;
            for (int y = Y_Bgn + _Dimension; y < _Size; y+=_Dimension)
            {
                int v = Cells[0, y].Value;
                if (v == 0) return Y_Bgn;
                if (v < min) { idx = y; min = v; }
            }
            return idx;
        }

        public void Normalize()
        {
            // rename the first _Size fields to [1,..,_Size]
            for (int X = 0; X < _Size; X++)
            {
                int O1 = X + 1;
                int O2 = Cells[X,0].Value;
                if (O2 > 0) Swap_Values(O1, O2);
            }

            // now make the left-most number in each row the lowest possible
            for (int Y = 0; Y < _Size; Y += _Dimension)
            {
                int Y_End = Y + _Dimension;
                for (int y = Y; y < Y_End; y++)
                {
                    int idx = FindRowMin(y, Y_End);
                    Swap_Rows(y, idx);
                }
            }

            // make the top-left number in for each squarerow the minimum
            for (int Y = 0; Y < _Size; Y += _Dimension)
            {
                int idx = FindSquareMin(Y);
                Swap_SquareRows(Y, idx);
            }
        }
    }
}

//********************************************************************
// END OF FILE SuDoKu_Field
//********************************************************************
