//********************************************************************
// ULong - Provides an unsigned long integer arithmetic
// (c) Jan 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;

//********************************************************************
namespace MMC.Numbers
{
//********************************************************************
    public class ULong
    {
        protected List<uint> _Value;

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
            if (len < _Value.Count) _Value.RemoveRange(len, _Value.Count - len);
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
        public void Sub(ULong other)
        {
            int mylen = _Value.Count;
            int olen = other._Value.Count;

            // subtract the common part
            int len = (mylen <= olen) ? mylen : olen; // take the minimum
            uint rem = 0;
            for (int i = 0; i < len; i++)
            {
                // Check for underflow
                uint s = other._Value[i] + rem;
                // s==0 =>
                // a) o == 0  && rem == 0 => rem = 0
                // b) o == -1 && rem == 1 => rem = 1
                if (s > 0)
                {
                    rem = (s > _Value[i]) ? 1U : 0U;
                    _Value[i] -= s;
                }
            }

            // copy the "left-over"
            while (mylen < olen)
            {
                _Value.Add(other._Value[mylen]);
                mylen++;
            }

            // and correct the remainder
            if (rem > 0) Dec(len);
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
        // Conversion Helpers
        public string ToHex()
        {
            string str = "0x" + _Value[_Value.Count-1].ToString("X");
            for (int i = _Value.Count - 2; i >= 0; i--)
            {
                str += _Value[i].ToString("X8");
            }
            return str;
        }

        protected static int ToBin(char[] bits, uint Value)
        {
            int pos = 31;
            do
            {
                bits[pos] = (char)('0' + ((char)(Value & 1)));
                Value >>= 1;
                pos--;
            } while (Value > 0);
            pos++;
            for (int i = 0; i < pos; i++) { bits[i] = '0'; }
            return pos;
        }

        public string ToBin()
        {
            char[] bits = new char[32];
            int pos = ToBin(bits, _Value[_Value.Count - 1]);
            string str = "0b" + new string(bits, pos, 32 - pos);
            for (int i = _Value.Count - 2; i >= 0; i--)
            {
                ToBin(bits, _Value[i]);
                str += bits;
            }
            return str;
        }

        public string ToDec()
        {
            string str = string.Empty;
            str += "Hallo";
            return str;
        }
        
        //------------------------------------------------------------
        // convert into a string
        public string ToString(uint Base)
        {
            switch (Base)
            {
                case  2 :   return ToBin();
                case 10 :   return ToDec();
                case 16 :   return ToHex();
                default:
                    throw new System.NotImplementedException("Can not convert ULong to base " + Base.ToString());
            }
        }

        //------------------------------------------------------------
        // Default conversion to Hex
        public override string ToString()
        {
            return ToString(16);
        }
    } // end of class ULong

//********************************************************************
} // end of NameSpace MMC.Numbers

//********************************************************************
// END OF FILE ULong
//********************************************************************
