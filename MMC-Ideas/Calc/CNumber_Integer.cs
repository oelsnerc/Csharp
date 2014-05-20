//********************************************************************
// CNumber_Integer - <type description here>
// (c) Jan 2011 MMC
//********************************************************************
using System;
using System.Collections.Generic;

namespace MMC.Numbers
{
    public class CNumber_Integer : MMC.Numbers.CNumber
    {
        protected List<uint> _Values;
        protected bool _Sign;

        //------------------------------------------------------------
        // to init everything
        protected virtual void Clear()
        {
            _Values.Clear();
            _Values.Add(0);
            _Sign = false;
        }

        //************************************************************
        // constructors
        protected CNumber_Integer()
        {
            _Values = new List<uint>();
            Clear();
        }

        //------------------------------------------------------------
        protected CNumber_Integer(MMC.Numbers.CNumber_Integer other)
        {
            if (other == null)
            {
                _Values = new List<uint>();
                Clear();
            }
            else
            {
                _Values = new List<uint>(other._Values);
                _Sign = other._Sign;
            }
        }

        //------------------------------------------------------------
        public CNumber_Integer(double Value)
        {
            _Values = new List<uint>();
            _Sign = (Value < 0.0);
            _Values.Add((uint)(_Sign ? -Value : Value));
        }

        //------------------------------------------------------------
        public CNumber_Integer(string Str)
        {
            _Values = new List<uint>();
            FromString(Str);
        }

        //************************************************************
        // some helper functions
        //************************************************************
        
        //------------------------------------------------------------
        // remove all leading zeros
        protected virtual void Trim()
        {
            int end = _Values.Count-1;
            int cnt = end;
            while (cnt > 0 && _Values[cnt] == 0) cnt--;
            if (cnt < end) _Values.RemoveRange(cnt+1, end - cnt);
        }

        //------------------------------------------------------------
        // copy this one
        public override MMC.Numbers.CNumber Clone() { return new CNumber_Integer(this); }

        //------------------------------------------------------------
        public override MMC.Numbers.CNumber.CNumberType MyType { get { return CNumberType.cnt_Integer; } }

        //------------------------------------------------------------
        // convert into a string
        public override string ToString(uint Base)
        {
            // first the sign
            string str = (_Sign) ? "-" : "";

            // numbers of digits separated by '.'
            int step = 4;

            // now the base-indication
            switch (Base)
            {
                case 2: str += "0b"; break;
                case 8: str += "0o"; break;
                case 10: step = 3;  break;
                case 16: str += "0x"; break;
                default: str += '[' + Base.ToString() + ']'; break;
            }

            // if the base is a power of 2
            // let's calc the binary logarithm
            bool poweroftwo = ((Base & (Base - 1)) == 0);
            if (poweroftwo)
            {
                uint b = Base;
                Base = 0;
                while (b > 1) { b >>= 1; Base++; };
            }

            // generate reversed string of digits
            CNumber_Integer tmp = (CNumber_Integer)Clone();
            string num = String.Empty;
            int cnt = 0;
            do
            {
                if (cnt == step) { cnt = 0; num += '.'; }
                uint rem = (poweroftwo) ? tmp.ShiftRight((int) Base) : tmp.Divide(Base);
                num += (char)((rem > 9) ? (rem - 10 + 'A') : (rem + '0'));
                cnt++;
            } while (!tmp.IsZero);

            for (int i = num.Length - 1; i >= 0; i--) str += num[i];
            return str;
        }

        //------------------------------------------------------------
        public override bool FromString(ref string Input)
        {
            Clear();

            // Extract the base of the representation
            uint b = CalcBase(ref Input);
            if (b == 0) return false;       // if illegal ... return nothing
            if (b == 1) return true;        // only a zero

            while (Input.Length > 0)
            {
                char c = Input[0];
                if (c != '.')
                {
                    int n = CalcValue(Input[0]);
                    if (n < 0 || n > (b - 1)) break;
                    Multiply(b);
                    Addition((uint)n);
                }
                Input = Input.Remove(0, 1);
            };

            return true;
        }

        //------------------------------------------------------------
        // to set or get the value as integer
        public override int AsInteger
        {
            get
            {
                return (_Sign) ? (-((int) _Values[0])) : ((int) _Values[0]);
            }
            set
            {
                Clear();
                _Sign = (value < 0);
                _Values[0] = (uint) ((_Sign) ? -value : value);
            }
        }

        //------------------------------------------------------------
        // the little helpers
        public override bool IsZero { get { return (_Values.Count == 1 && _Values[0] == 0); } }

        public override bool IsNegative { get { return _Sign; } }

        public override bool Equals(MMC.Numbers.CNumber other)
        {
            if (other.MyType != MyType) return false;

            CNumber_Integer o = (CNumber_Integer)other;
            if (_Values.Count != o._Values.Count) return false;

            for (int i = 0; i < _Values.Count; i++)
            {
                if (_Values[i] != o._Values[i]) return false;
            }
            return true;
        }

        public override int Compare(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();

            CNumber_Integer o = (CNumber_Integer)other;

            int t_len = _Values.Count;
            int o_len = o._Values.Count;
            if (o_len > t_len) return -1;
            if (o_len < t_len) return 1;

            t_len--;
            while (t_len > 0 && o._Values[t_len] == _Values[t_len]) t_len--;
            return Math.Sign(_Values[t_len] - o._Values[t_len]);
        }

        //------------------------------------------------------------
        // move all one digit to the left and insert the value
        protected virtual void Insert(uint Value) { _Values.Insert(0, Value); }

        //------------------------------------------------------------
        // multiply by a power of 2
        // return the remainder
        protected virtual uint ShiftLeft(int Count)
        {
            //TODO think of using the "InsertRange" more efficient
            uint rem = 0;
            int Add = 0;
            while (Count >= 32) { Add++; Count -= 32; }

            if (Count > 0)
            {
                int NegCount = 32 - Count;
                int cnt = _Values.Count;
                for (int i = 0; i < cnt; i++)
                {
                    uint v = _Values[i];
                    _Values[i] = (v << Count) | rem;
                    rem = v >> NegCount;
                }
            }

            if (Add > 0) _Values.InsertRange(0, new uint[Add]);
            if (rem > 0) _Values.Add(rem);

            return rem;
        }

        //------------------------------------------------------------
        // devide by a power of 2
        // return the remainder
        protected virtual uint ShiftRight(int Count)
        {
            uint mask = ((uint)1 << Count) - 1;
            int NegCount = 32 - Count;
            uint rem = 0;
            for (int i = _Values.Count-1; i >= 0; i--)
            {
                uint v = (_Values[i] & mask);
                _Values[i] >>= Count;
                _Values[i] |= (rem << NegCount);
                rem = v;
            }
            Trim();
            return rem;
        }

        //------------------------------------------------------------
        protected virtual void Addition(uint Value)
        {
            if (Value == 0) return;

            _Values[0] = _Values[0] + Value;
            bool rem = (_Values[0] < Value);
            for (int i = 1; rem && (i < _Values.Count); i++)
            {
                _Values[i]++;
                rem = (_Values[i] == 0);
            }
            if (rem) _Values.Add(1);
        }

        //------------------------------------------------------------
        protected virtual void Multiply(uint Value)
        {
            if (Value == 0) { Clear(); return; }
            if (Value == 1) return;

            uint rem = 0;
            for (int i = 0; i < _Values.Count; i++)
            {
                ulong v = ((ulong) _Values[i]) * ((ulong) Value) + rem;
                rem = (uint) (v >> 32);
                _Values[i] = (uint)v;
            }
            if (rem > 0) _Values.Add(rem);
        }

        //------------------------------------------------------------
        // divide by an integer and return the remainder
        protected virtual uint Divide(uint Value)
        {
            if (Value == 0) throw new CNumberException("Division by Zero!");
            if (Value == 1) return 0;

            long rem = 0;
            for (int i = _Values.Count-1; i >=0; i--)
            {
                long v = (rem << 32) + (long) _Values[i];
                _Values[i] = (uint) Math.DivRem(v, (long)Value, out rem);
            }
            Trim();
            return (uint) rem;
        }

        //------------------------------------------------------------
        // divide by another number and return the remainder
        protected virtual CNumber_Integer Divide(CNumber_Integer other)
        {
            CNumber_Integer Save = new CNumber_Integer(this);       // save the old value
            Clear();                                                // set the initial result to 0

            int o_len = other._Values.Count;
            int t_len = _Values.Count;
            int pos = t_len - o_len;
            if (pos < 0) return Save;                               // fewer digits means the result is zero and the remainder are just we

            CNumber_Integer Rem = new CNumber_Integer();            // set the remainder to 0
            int len = o_len - 1;
            while (pos >= 0)
            {
                Rem.Insert(Save._Values[pos]);                           // take the next digit

                uint q = Rem._Values[len] / other._Values[len];     // and make an estimate for the quotient 
                CNumber_Integer tmp = new CNumber_Integer(other);   // and calculate the product for this estimated value
                tmp._Sign = false;
                tmp.Multiply(q);
                Rem.Subtraction(tmp);                               // see how good we estimated
                if (Rem._Sign)                                      // if we are too low just correct by one
                {
                    q--;
                    Rem.add(other);
                }

                Insert(q);                                          // and write down the quotients digit
                pos--;                                              // walk one further in our (this) digits
            }

            _Sign = (_Sign != other._Sign);
            Trim();
            return Rem;
        }

        //------------------------------------------------------------
        // add a number
        // the native way to take care of the overflow
        // is checking after the summation if the sum is less than
        // one of the summands, this would mean overflow
        // but this would need some more conditional branches
        // so I choose a way with conversions from uint to ulong
        // to let the .Net calc the overflow
        protected virtual void Addition(CNumber_Integer other)
        {
            ulong rem = 0;                                  // the remainder (either 0 or 1) (ulong for the overflow)
            int o_len = other._Values.Count;                // the other length
            int t_len = _Values.Count;                      // our own length
            int len = (t_len  < o_len) ? t_len : o_len;     // calc the minimum length
            int i = 0;                                      // start with the lowest digit
            for (; i < len; i++)                            // first add all common digits
            {
                rem += (ulong)other._Values[i];             // val = rem + val + other.val
                rem += (ulong)_Values[i];
                _Values[i] = (uint)rem;
                rem >>= 32;                                 // rem = overflow
            }
            for (; i < o_len; i++)                          // now copy the remaining from the other
            {
                rem += (ulong)other._Values[i];             // don't forget the remainder
                _Values.Add((uint)rem);
                rem >>= 32;
            }
            for (; (rem>0) && (i < t_len); i++)             // now increase our own remaining digits
            {                                               // as long as the remainder is non-zero
                rem += (ulong)_Values[i];
                _Values[i] = (uint)rem;
                rem >>= 32;
            }
            if (rem > 0) _Values.Add((uint)rem);            // still something left, just add it
        }

        //------------------------------------------------------------
        // subtract a number of shorter or equal length
        protected virtual void Subtraction(CNumber_Integer other)
        {
            bool rem = false;                               // the overflow state
            int o_len = other._Values.Count;                // the other length
            int t_len = _Values.Count;                      // our own length
            int len = (t_len < o_len) ? t_len : o_len;      // calc the minimum length
            int i = 0;                                      // start with the lowest digit
            for (; i < len; i++)
            {
                uint v = other._Values[i]; if (rem) v++;
                rem = (v > _Values[i]);
                _Values[i]-=v;
            }
            for (; i < o_len; i++)                          // now copy the remaining from the other
            {
                uint v = other._Values[i]; if (rem) v++;
                _Values.Add((uint) (-v));                            // = 0-v;
                rem = true;                                 // allways an overflow
            }

            for (; rem && (i < t_len); i++)                 // now decrease our own remaining digits
            {
                rem = (_Values[i] == 0);
                _Values[i]--;
            }

            if (rem)                                        // if we have an overflow we have to 
            {                                               // negate the result
                _Values[0] = (uint)(-_Values[0]);
                for (int k = 1; k < _Values.Count; k++)
                {
                    _Values[k] ^= 0xFFFFFFFF;
                }
                _Sign = !_Sign;
            }
            Trim();                                         // could have created some leading zeros
        }

        //------------------------------------------------------------
        // bit wise invert
        protected virtual void Not()
        {
            for (int i = 0; i < _Values.Count; i++)
            {
                _Values[i] ^= 0xFFFFFFFF;
            }
        }

        //------------------------------------------------------------
        // logical functions
        public override CNumber not()
        {
            CNumber_Integer res = new CNumber_Integer(this);
            res.Not();
            res.Trim();
            return res;
        }

        public override CNumber and(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            CNumber_Integer res = new CNumber_Integer(this);
            CNumber_Integer o = (CNumber_Integer)other;
            int o_len = o._Values.Count;                    // the other length
            int t_len = _Values.Count;                      // our own length
            int len = (t_len < o_len) ? t_len : o_len;      // calc the minimum length
            int i = 0;                                      // start with the lowest digit
            for (; i < len; i++)                            // first handle the common digits
            {
                res._Values[i] &= o._Values[i];
            }
            for (; i < t_len; i++)                          // zero out the rest
            {
                res._Values[i] = 0;
            }
            res.Trim();
            return res;
        }

        public override CNumber or(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            CNumber_Integer res = new CNumber_Integer(this);
            CNumber_Integer o = (CNumber_Integer)other;
            int o_len = o._Values.Count;                    // the other length
            int t_len = _Values.Count;                      // our own length
            int len = (t_len < o_len) ? t_len : o_len;      // calc the minimum length
            int i = 0;                                      // start with the lowest digit
            for (; i < len; i++)                            // first handle the common digits
            {
                res._Values[i] |= o._Values[i];
            }
            for (; i < o_len; i++)                          // and add the rest
            {
                res._Values.Add(o._Values[i]);
            }
            return res;
        }

        public override CNumber xor(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            CNumber_Integer res = new CNumber_Integer(this);
            CNumber_Integer o = (CNumber_Integer)other;
            int o_len = o._Values.Count;                    // the other length
            int t_len = _Values.Count;                      // our own length
            int len = (t_len < o_len) ? t_len : o_len;      // calc the minimum length
            int i = 0;                                      // start with the lowest digit
            for (; i < len; i++)                            // first handle the common digits
            {
                res._Values[i] ^= o._Values[i];
            }
            res.Trim();
            return res;
        }

        //------------------------------------------------------------
        // return a negative version of this one
        public override MMC.Numbers.CNumber neg()
        {
            CNumber_Integer res = new CNumber_Integer(this);
            if (!IsZero) res._Sign = !_Sign;
            return res;
        }
        
        //------------------------------------------------------------
        // Arithmetics
        public override MMC.Numbers.CNumber add(MMC.Numbers.CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            CNumber_Integer res = new CNumber_Integer(this);
            if (_Sign == ((CNumber_Integer)other)._Sign)
            {
                res.Addition((CNumber_Integer)other);
            }
            else
            {
                res.Subtraction((CNumber_Integer)other);
            }
            return res;
        }

        public override MMC.Numbers.CNumber sub(MMC.Numbers.CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            CNumber_Integer res = new CNumber_Integer(this);
            if (_Sign != ((CNumber_Integer)other)._Sign)
            {
                res.Addition((CNumber_Integer)other);
            }
            else
            {
                res.Subtraction((CNumber_Integer)other);
            }
            return res;
        }

        public override MMC.Numbers.CNumber mul(MMC.Numbers.CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();

            CNumber_Integer o = (CNumber_Integer) other;
            int cnt = o._Values.Count;
            
            // calc the different interims (could be done in parallel)
            CNumber_Integer[] Results = new CNumber_Integer[cnt];
            for (int i = 0; i < cnt; i++)
            {
                CNumber_Integer tmp = new CNumber_Integer(this);
                tmp.Multiply(o._Values[i]);
                tmp._Values.InsertRange(0, new uint[i]);
                Results[i] = tmp;
            }

            // now add them all up
            CNumber_Integer res = new CNumber_Integer(Results[0]);
            for (int i = 1; i < cnt; i++)
            {
                res.Addition(Results[i]);
            }

            //CNumber_Integer mul = (CNumber_Integer)other;
            //for (int i = mul._Values.Count - 1; i >= 0; i--)
            //{
            //    CNumber_Integer tmp = (CNumber_Integer) Clone();
            //    tmp.Multiply(mul._Values[i]);
            //    res.Insert(0);
            //    res.Addition(tmp);
            //}

            res._Sign = (_Sign != o._Sign);
            res.Trim();
            return res;
        }

        public override MMC.Numbers.CNumber div(MMC.Numbers.CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();

            CNumber_Integer Res = new CNumber_Integer(this);
            Res.Divide((CNumber_Integer)other);

            return Res;
        }

        public override MMC.Numbers.CNumber rem(MMC.Numbers.CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();

            CNumber_Integer Res = new CNumber_Integer(this);
            return Res.Divide((CNumber_Integer)other);
        }

        public override MMC.Numbers.CNumber pow(MMC.Numbers.CNumber a)
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber sqr()
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber sqrt()
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber sin()
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber sinh()
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber cos()
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber cosh()
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber tan()
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber tanh()
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber rad()
        {
            throw new NotImplementedException();
        }

        public override MMC.Numbers.CNumber deg()
        {
            throw new NotImplementedException();
        }
    }
}

//********************************************************************
// END OF FILE CNumber_Integer
//********************************************************************
