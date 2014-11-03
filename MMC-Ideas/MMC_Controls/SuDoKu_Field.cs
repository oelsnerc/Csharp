//********************************************************************
// SuDoKu_Field - <type description here>
// (c) Mrz 2011 MMC
//********************************************************************
using System.Collections.Generic;

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

        public SuDoKu_Field(SuDoKu_Field other)
        {
            ivDimension = other.ivDimension;
            ivGroupSize = other.ivGroupSize;

            ivCells = new SuDoKu_Cell[ivGroupSize, ivGroupSize];
            for (int row = 0; row < ivGroupSize; row++)
            {
                for (int column = 0; column < ivGroupSize; column++)
                {
                    ivCells[row, column] = new SuDoKu_Cell(other.ivCells[row, column]);
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
                    rc += "\n" + ivCells[row, column].ToString();
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
            for (int row = 0; row < ivGroupSize; ++row)
            {
                for (int column = 0; column < ivGroupSize; ++column)
                {
                    ivCells[row, column] = new SuDoKu_Cell(Fields[i]);
                    ++i;
                }
            }
        }

        //************************************************************
        public void set(int row, int column, int value)
        {
            // remove in the same row
            for (int c = 0; c < ivGroupSize; ++c) ivCells[row, c].Options_REMOVE(value);

            // remove in the same column
            for (int r = 0; r < ivGroupSize; ++r) ivCells[r, column].Options_REMOVE(value);

            // remove in the same square
            int row_bgn = (row / ivDimension) * ivDimension;
            int col_bgn = (column / ivDimension) * ivDimension;
            int row_end = row_bgn + ivDimension;
            int col_end = col_bgn + ivDimension;

            for(int r = row_bgn; r < row_end; ++r) 
            {
                for(int c = col_bgn; c < col_end; ++c)
                {
                    ivCells[r, c].Options_REMOVE(value);
                }
            }

            ivCells[row, column].Value = value;
        }

        //************************************************************
        public int Dimension { get { return ivDimension; } }
        public int GroupSize { get { return ivGroupSize; } }
        public int Size { get { return ivGroupSize*ivGroupSize; } }

        public List<int> Options(int row, int column) { return ivCells[row, column].Options_List; }
        public int Options_Count(int row, int column) { return ivCells[row, column].Options_Count; }
        public bool Options_available(int row, int column) { return !ivCells[row, column].isEmpty; }
        public bool isCalculated(int row, int column) { return ivCells[row, column].isCalculated; }
        public void setCalculated(int row, int column, bool IsCalulated) { ivCells[row, column].isCalculated = IsCalulated; }
        public int this[int row, int column]
        {
            get { return ivCells[row, column].Value; }
            set { set(row, column,value); }
        }

        //------------------------------------------------------------
        public bool isValid
        {
            get
            {
                for (int row = 0; row < ivGroupSize; ++row)
                    for (int column = 0; column < ivGroupSize; ++column)
                    {
                        if (ivCells[row, column].isEmpty) return false;
                    }
                return true;
            }
        }

        //------------------------------------------------------------
        // Set the value for all cells that have only one option less
        // return true if the state is still valid
        public bool Solve_Ones()
        {
            for (int row = 0; row < ivGroupSize; ++row )
            {
                for (int column = 0; column < ivGroupSize; column++)
                {
                    SuDoKu_Cell c = ivCells[row, column];
                    if (! c.isSet)
                    {
                        int v = c.Value;
                        if (v < 0) return false;
                        if (v>0)
                        {
                            set(row, column, v);
                            return Solve_Ones();
                        }
                    }
                }
            }
            return true;
        }

    }
}

//********************************************************************
// END OF FILE SuDoKu_Field
//********************************************************************
