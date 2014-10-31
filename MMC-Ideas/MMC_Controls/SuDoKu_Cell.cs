//********************************************************************
// Sudoku_Field - Represents the State of a Sudoku Field
// (c) Mrz 2011 MMC
//********************************************************************
using System.Collections.Generic;

namespace MMC_Controls
{
    //****************************************************************
    // the idea is that one cell represents a set of options
    // This implementation will use an uint to represent at most 32 options
    // each bit represents an option, i.e. set means available
    // if the over all value is 0 no options is available
    // if there is only one bit set, this is the only option left
    // NOTE: maximum 32 options are supported
    //----------------------------------------------------------------
    // having this a class makes it a reference type
    public class SuDoKu_Cell
    {
        private uint ivValue;           // the options, each bit represents one option
        private bool ivCalculated;      // If this Value was user set or calculated
        private int  ivOptions_Count;   // the number of options left

        //------------------------------------------------------------
        // constructor
        public SuDoKu_Cell(int MaxOption)
        {
            ivValue = (1u << MaxOption) - 1;
            ivCalculated = false;
            ivOptions_Count = MaxOption;
        }

        //------------------------------------------------------------
        // copy constructor
        public SuDoKu_Cell(SuDoKu_Cell other)
        {
            CopyFrom(other);
        }

        //------------------------------------------------------------
        // construct from a saved string
        public SuDoKu_Cell(string Encoded)
        {
            FromString(Encoded);
        }

        //------------------------------------------------------------
        // explicit copy
        public void CopyFrom(SuDoKu_Cell other)
        {
            ivValue = other.ivValue;
            ivCalculated = other.ivCalculated;
            ivOptions_Count = other.ivOptions_Count;
        }

        //------------------------------------------------------------
        // exchange 2 cells
        public void SwapWith(SuDoKu_Cell other)
        {
            uint v = ivValue;
            ivValue = other.ivValue;
            other.ivValue = v;

            bool c = ivCalculated;
            ivCalculated = other.ivCalculated;
            other.ivCalculated = c;

            int cnt = ivOptions_Count;
            ivOptions_Count = other.ivOptions_Count;
            other.ivOptions_Count = cnt;
        }

        //------------------------------------------------------------
        // return the number of possible options
        // NOTE: we use lazy evaluation
        public int Options_Count
        {
            get
            {
                if (ivOptions_Count < 0)
                {
                    ivOptions_Count = 0;
                    uint v = ivValue;
                    while (v > 0)
                    {
                        ivOptions_Count++;
                        v &= v - 1; // reset the least significant bit
                    }
                }
                return ivOptions_Count;
            }
        }

        //------------------------------------------------------------
        // some basic manipulation functions
        public void Options_AND(SuDoKu_Cell other)
        {
            ivValue &= other.ivValue;
            ivOptions_Count = -1;
        }

        public void Options_OR(SuDoKu_Cell other)
        {
            ivValue |= other.ivValue;
            ivOptions_Count = -1;
        }

        public void Options_REMOVE(SuDoKu_Cell other)
        {
            ivValue &= (~other.ivValue);
            ivOptions_Count = -1;
        }

        public void Options_REMOVE(int Index)
        {
            uint v = (1u << (Index - 1));
            ivValue &= (~v);
            ivOptions_Count--;
        }

        public void Options_SET(int Index)
        {
            ivValue = (1u << (Index - 1));
            ivOptions_Count = 1;
        }

        //------------------------------------------------------------
        // basic io functions
        //------------------------------------------------------------
        // convert to a string to be able to save it into a file
        public override string ToString()
        {
            string rc = ivValue.ToString();
            rc += "," + ivCalculated.ToString();
            return rc;
        }

        //------------------------------------------------------------
        // deserialize from a string
        public void FromString(string Data)
        {
            string[] Fields = Data.Split(',');
            ivValue = uint.Parse(Fields[0]);
            ivCalculated = bool.Parse(Fields[1]);
            ivOptions_Count = -1;
        }

        //------------------------------------------------------------
        // return the first available options
        // NOTE: 0 means no option available
        public int Options_First
        {
            get
            {
                uint value = (ivValue ^ (ivValue - 1)) >> 1;    // Set traling 0's to 1's and clear the rest
                int res = 0;
                while (value > 0) { value >>= 1; res++; }
                return res;
            }
        }

        //------------------------------------------------------------
        // return the list of ALL available options
        public List<int> Options_List
        {
            get
            {
                List<int> rc = new List<int>();
                uint value = ivValue;
                for (int pos = 1; value > 0; ++pos )
                {
                    if ((value & 1) != 0) rc.Add(pos);
                    value >>= 1;
                }
                return rc;
            }
        }

        //------------------------------------------------------------
        // flag to be set by user
        public bool isCalculated
        { 
            get { return ivCalculated; }
            set { ivCalculated = value; }
        }

        public int Value
        {
            get
            {
                if (Options_Count != 1) return 0;
                return Options_First;
            }
            set
            {
                Options_SET(value);
            }
        }
    }
}

//********************************************************************
// END OF FILE Sudoku_Value
//********************************************************************
