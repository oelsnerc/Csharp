//********************************************************************
// ULong - Provides an unsigned long integer arithmetic
// (c) Jan 2010 MMC
//********************************************************************
using System.Collections.Generic;

//********************************************************************
namespace MMC {
//********************************************************************
    public class ULong
    {
        protected List<uint> _Value;

        //------------------------------------------------------------
        // subtract one subvector from the other
        protected uint Subtract(List<uint> Src1, List<uint> Src2, int len)
        {
            uint rem = 0;
            for (int i = 0; i < len; i++)
            {
                uint s = Src2[i] + rem;
                if (s > 0)
                {
                    rem = (s > Src1[i]) ? 1U : 0U;
                    _Value[i] = Src1[i] - s;
                }
            }
            return rem;
        }

        //------------------------------------------------------------
        // standard constructor
        public ULong(uint Value)
        {
            _Value = new List<uint>();
            _Value.Add(Value);
        }

        //------------------------------------------------------------
        // copy constructor
        public ULong(ULong other)
        {
            _Value = new List<uint>();
            foreach (uint v in other._Value)
            {
                _Value.Add(v);
            }
        }

        //------------------------------------------------------------
        // destructor
        ~ULong() { }

        //------------------------------------------------------------
        // remove leading zero's
        protected void Trim()
        {
            int len = _Value.Count - 1;
            while (len > 0 && _Value[len] == 0) len--;
            _Value.RemoveRange(len, _Value.Count - 1 - len);
        }

        //************************************************************
        // Arithmetic
        //************************************************************
        // Increment by one starting from Index
        protected void Inc(int Index)
        {
            int len = _Value.Count;
            for (; Index < len; Index++)
            {
                _Value[Index]++;
                if (_Value[Index] != 0) break;
            }
            if (Index == len) _Value.Add(1);
        }

        //------------------------------------------------------------
        // Decrement by one from starting Index
        protected void Dec(int Index)
        {
            int len = _Value.Count;
            for (; Index < len; Index++)
            {
                uint v = _Value[Index];
                _Value[Index]--;
                if (v > 0) break;
            }
            if ((len > 1) && (_Value[len - 1] == 0)) _Value.RemoveAt(len - 1);
        }

        //------------------------------------------------------------
        // Increment by one
        public void Inc() { Inc(0); }

        //------------------------------------------------------------
        // Decrement by one
        public void Dec() { Dec(0); }

        //------------------------------------------------------------
        // add another ULong
        public void Add(ULong other)
        {
            int mylen = _Value.Count;
            int olen = other._Value.Count;

            // add the common part
            int len = (mylen <= olen) ? mylen : olen; // take the minimum
            uint rem = 0;
            for (int i = 0; i < len; i++)
            {
                uint r = _Value[i] + other._Value[i] + rem; // add all together
                //set the remainder
                // is the result == _Value[i]
                // => other._Value[i] +  rem = 0
                // => a) other._Value[i] = -1 & rem = 1 => rem = 1
                // => b) other._Value[i] = 0  & rem = 0 => rem = 0
                // => in either case => keep the remainder
                // check for result != _Value[i]
                if (r < _Value[i])
                    rem = 1;
                else if (r > _Value[i])
                    rem = 0;
                _Value[i] = r;
            }

            // copy the "left-over"
            while (mylen < olen)
            {
                _Value.Add(other._Value[mylen]);
                mylen++;
            }

            // and correct the remainder
            if (rem > 0) Inc(len);
        }

        //------------------------------------------------------------
        // add unsigned 32bits starting from Index
        protected void Add(uint Value, int Index)
        {
            if (Value == 0) return;

            // Check upper boundary
            if (Index < _Value.Count)
            {   // normal addition
                _Value[Index] += Value;                     // add it to the first digit
                if (_Value[Index] < Value) Inc(Index + 1);  // correct the overflow
                return;                                     // and done
            }

            // append new digits
            while (_Value.Count < Index) _Value.Add(0);
            _Value.Add(Value);
        }

        //------------------------------------------------------------
        // add an unsigned 32 bits integer
        public void Add(uint Value)
        {
            _Value[0] += Value;
            if (_Value[0] < Value) Inc(1);
        }

        //------------------------------------------------------------
        // Subtracts an unsigned 32 bit value
        public void Sub(uint Value)
        {
            uint v = _Value[0];
            _Value[0] -= Value;
            if (_Value[0] > v) Dec(1);
        }

        //------------------------------------------------------------
        // Subtracts another ULong from this one
        // returns true if an underflow happened
        public bool Sub(ULong other)
        {
            int mylen = _Value.Count;
            int olen = other._Value.Count;

            if (mylen > olen)
            {
                uint rem = Subtract(_Value, other._Value, olen);
                if (rem > 0) Dec(olen);
                return false;

            } // if (mylen > olen)
            else if (mylen < olen)
            {
                uint rem = Subtract(other._Value, _Value, mylen);

                // copy the "left-over"
                while (mylen < olen)
                {
                    _Value.Add(other._Value[mylen]);
                    olen--;
                }

                if (rem > 0) Dec(mylen);
                return true;

            } // if (mylen < olen)
            else
            {   // mylen == olen
                uint rem = Subtract(_Value, other._Value, mylen);
                Trim();
                if (rem > 0) return true;
            }
            return false;
        }

        //------------------------------------------------------------
        // arithmetical operators
        public static ULong operator +(ULong a, ULong b)
        {
            ULong res = new ULong(a);
            res.Add(b);
            return res;
        }

        public static ULong operator +(ULong a, uint b)
        {
            ULong res = new ULong(a);
            res.Add(b);
            return res;
        }

        //************************************************************
        // Logic operations
        //************************************************************
        //------------------------------------------------------------
        // multiply by the <Count>-th power of 2
        public void ShiftLeft(int Count)
        {
            int bits = Count % 32;
            if (bits > 0)
            {
                uint rem = 0;
                int len = _Value.Count;
                for (int i = 0; i < len; i++)
                {
                    uint r = _Value[i] >> (32 - bits);
                    _Value[i] = (_Value[i] << bits) | rem;
                    rem = r;
                }
                if (rem > 0) _Value.Add(rem);
            }

            int digits = Count / 32;
            while (digits > 0)
            {
                _Value.Insert(0, 0);
                digits--;
            }
        }

        //------------------------------------------------------------
        public void OR(ULong other)
        {
            int len = (_Value.Count > other._Value.Count) ? other._Value.Count : _Value.Count;
            for (int i = 0; i < len; i++)
            {
                _Value[i] |= other._Value[i];
            }
        }

        public void XOR(ULong other)
        {
            int len = (_Value.Count > other._Value.Count) ? other._Value.Count : _Value.Count;
            for (int i = 0; i < len; i++)
            {
                _Value[i] ^= other._Value[i];
            }
        }

        public void AND(ULong other)
        {
            int len = (_Value.Count > other._Value.Count) ? other._Value.Count : _Value.Count;
            for (int i = 0; i < len; i++)
            {
                _Value[i] &= other._Value[i];
            }
        }
        //************************************************************
        // public Helper functions
        //************************************************************
        //------------------------------------------------------------
        // convert into a string
        public override string ToString()
        {
            string str = "";
            for (int i = _Value.Count - 1; i >= 0; i--)
            {
                str += _Value[i].ToString("X8");
            }
            return str;
        }
    } // end of class ULong

//********************************************************************
} // end of NameSpace MMC

//********************************************************************
// END OF FILE ULong
//********************************************************************
