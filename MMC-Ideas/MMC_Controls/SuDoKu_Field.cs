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
        protected SuDoKu_Cell[,]        ivCells;        // all the fields
        protected int                   ivDimension;    // the Sudokus Dimension
        protected int                   ivGroupSize;    // the number of fields in a row/column/square

        //************************************************************
        public SuDoKu_Field(int MyDimension)
        {
            ivDimension = MyDimension;
            ivGroupSize = ivDimension * ivDimension;

            ivCells = new SuDoKu_Cell[ivGroupSize, ivGroupSize];
            for (int row = 0; row < ivGroupSize; row++)
            {
                for (int column = 0; column < ivGroupSize; column++)
                {
                    ivCells[row, column] = new SuDoKu_Cell(ivGroupSize);
                }
            }
        }

        //------------------------------------------------------------
        public override string ToString()
        {
            string rc = ivDimension.ToString();
            for (int row = 0; row < ivGroupSize; row++)
            {
                for (int column = 0; column < ivGroupSize; column++)
                {
                    rc += "\n" + ivCells[row, column].ToString()
                }
            }
            return rc;
        }

        //------------------------------------------------------------
        public void FromString(string Data)
        {
            string[] Fields = Data.Split('\n');
            ivDimension = int.Parse(Fields[0]);
            ivGroupSize = ivDimension * ivDimension;

            ivCells = new SuDoKu_Cell[ivGroupSize, ivGroupSize];

            int i = 0;
            for (int row = 0; row < ivGroupSize; row++)
            {
                for (int column = 0; column < ivGroupSize; column++)
                {
                    ivCells[row, column] = new SuDoKu_Cell(Fields[i]);
                    ++i;
                }
            }
        }

        //************************************************************
        public int Dimension { get { return ivDimension; } }
        public int GroupSize { get { return ivGroupSize; } }
        public int Size { get { return ivGroupSize*ivGroupSize; } }

        public List<int> Options(int row, int column) { return ivCells[row, column].Options_List; }
        public int Options_Count(int row, int column) { return ivCells[row, column].Options_Count; }
        public bool isCalculated(int row, int column) { return ivCells[row, column].isCalculated; }
        public int this[int row, int column]
        {
            get { return ivCells[row, column].Value; }
            set { ivCells[row, column].Value = value; }
        }

        //------------------------------------------------------------
        public bool Valid
        {
            get
            {
                for (int x = 0; x < _Size; x++)
                    for (int y = 0; y < _Size; y++)
                    {
                        if (Cells[x, y].ivOptions_Count == 0 && Cells[x, y].ivValue == 0) return false;
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
                        if (Cells[x, y].ivValue == 0)
                        {
                            int ocount = Cells[x, y].ivOptions_Count;
                            if (ocount == 0) return false;
                            if (ocount == 1)
                            {
                                cnt++;
                                int Value = Cells[x, y].Options_First;
                                Option_Set(x, y, Value, false);
                                Cells[x, y].ivCalculated = true;
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
                        if (Cells[x, y].ivValue == 0)
                        {
                            int ocount = Cells[x, y].ivOptions_Count;
                            if (ocount == 0) return false;
                            if (ocount == 2)
                            {
                                List<int> L = Cells[x, y].Options_List;

                                // Check First Value
                                SuDoKu_Cell[,] Old = Cells;         // Save the old state
                                Cells = Copy(Cells);                // work on the copy
                                Option_Set(x, y, L[0], false);      // set the value
                                Cells[x, y].ivCalculated = true;
                                bool r1 = Solve_Two(depth);         // and solve it
                                
                                // Check the 2nd Value
                                SuDoKu_Cell[,] New = Cells;         // keep the result
                                Cells = Copy(Old);                  // reset to the original state
                                Option_Set(x, y, L[1], false);      // set the value
                                Cells[x, y].ivCalculated = true;
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
            int min = Cells[0,idx].ivValue;
            for (int y = Y_Bgn+1; y < Y_End; y++)
			{
                int v = Cells[0,y].ivValue;
                if (v == 0) return Y_Bgn;
                if (v < min) { idx = y; min = v; }
			}
            return idx;
        }

        protected int FindSquareMin(int Y_Bgn)
        {
            int idx = Y_Bgn;
            int min = Cells[0, idx].ivValue;
            for (int y = Y_Bgn + _Dimension; y < _Size; y+=_Dimension)
            {
                int v = Cells[0, y].ivValue;
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
                int O2 = Cells[X,0].ivValue;
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
