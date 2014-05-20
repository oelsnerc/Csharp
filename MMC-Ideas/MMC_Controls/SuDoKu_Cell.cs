//********************************************************************
// Sudoku_Field - Represents the State of a Sudoku Field
// (c) Mrz 2011 MMC
//********************************************************************
using System.Collections.Generic;

namespace MMC_Controls
{
    //****************************************************************
    public struct SuDoKu_Cell
    {
        public int Value;           // the actual Value of the field (0 = unset)
        public bool Calculated;     // If this Value was user set or calculated
        public int Options_Count;   // the number of options left
        private bool[] Options;   // Holds all the possible options for this field

        public SuDoKu_Cell(int MaxOption)
        {
            Value = 0;
            Calculated = false;
            Options = new bool[MaxOption];
            for (int i = 0; i < MaxOption; i++)
            {
                Options[i] = true;
            }
            Options_Count = MaxOption;
        }

        public SuDoKu_Cell(ref SuDoKu_Cell other)
        {
            Value = other.Value;
            Calculated = other.Calculated;
            Options_Count = other.Options_Count;
            Options = (bool[])other.Options.Clone();
        }

        public void CopyFrom(ref SuDoKu_Cell other)
        {
            Value = other.Value;
            Calculated = other.Calculated;
            Options_Count = other.Options_Count;
            Options = (bool[]) other.Options.Clone();
        }

        public void SwapWith(ref SuDoKu_Cell other)
        {
            int v = Value;
            Value = other.Value;
            other.Value = v;

            bool c = Calculated;
            Calculated = other.Calculated;
            other.Calculated = c;

            int cnt = Options_Count;
            Options_Count = other.Options_Count;
            other.Options_Count = cnt;

            bool[] o = Options;
            Options = other.Options;
            other.Options = o;
        }

        public override string ToString()
        {
            string rc = Value.ToString();
            rc += "," + Calculated.ToString();
            rc += "," + Options.Length.ToString();
            for (int i = 0; i < Options.Length; i++)
            {
                if (Options[i]) rc += "," + i.ToString();
            }
            return rc;
        }

        public void FromString(string Data)
        {
            string[] Fields = Data.Split(',');
            Value = int.Parse(Fields[0]);
            Calculated = bool.Parse(Fields[1]);
            int MaxOption = int.Parse(Fields[2]);
            Options_Count = 0;
            Options = new bool[MaxOption];
            for (int i = 3; i < Fields.Length; i++)
            {
                int v = int.Parse(Fields[i]);
                Options[v] = true;
                Options_Count++;
            }
        }

        public void Options_Set(int Index, bool Value)
        {
            if (Options[Index] != Value)
            {
                Options[Index] = Value;
                if (Value)
                    Options_Count++;
                else
                    Options_Count--;
            }
        }

        public int Options_First
        {
            get
            {
                for (int i = 0; i < Options.Length; i++)
                {
                    if (Options[i]) return (i+1);
                }
                return 0;
            }
        }

        public List<int> Options_List
        {
            get
            {
                List<int> rc = new List<int>(Options.Length);
                for (int i = 0; i < Options.Length; i++)
                {
                    if (Options[i]) rc.Add(i+1);
                }
                return rc;
            }
        }

        public void Option_Swap(int O1, int O2)
        {
            bool t = Options[O1-1];
            Options[O1-1] = Options[O2-1];
            Options[O2-1] = t;
            if (Value == O1)
                Value = O2;
            else if (Value == O2)
                Value = O1;
        }
    }
    //****************************************************************
}

//********************************************************************
// END OF FILE Sudoku_Value
//********************************************************************
