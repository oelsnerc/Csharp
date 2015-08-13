//********************************************************************
// CNumber_Integer - <type description here>
// (c) Jan 2011 MMC
//********************************************************************
using System;
using System.Collections.Generic;

namespace MMC.Numbers
{
    public class CNumber_Integer : CNumber
    {
        protected List<byte> _Values;
        protected bool _Sign;

        public int size => _Values.Count;
        protected const long maxDigit = (long)(byte.MaxValue) + 1;
        protected const int bitsDigit = 8;
        protected const int intDigits = 4;

        //************************************************************
        // some helper functions
        //************************************************************

        //------------------------------------------------------------
        // to init everything
        protected virtual void Clear()
        {
            _Values = new List<byte>();
        }

        protected void setZero()
        {
            Clear();
            _Values.Add(0);
            _Sign = false;
        }

        //------------------------------------------------------------
        // to copy
        protected virtual void CopyFrom(CNumber_Integer other)
        {
            _Values = new List<byte>(other._Values);
            _Sign = other._Sign;
        }

        //------------------------------------------------------------
        // remove all leading zeros
        protected virtual void Trim()
        {
            int end = _Values.Count - 1;
            int cnt = end;
            while (cnt > 0 && _Values[cnt] == 0) --cnt;
            if (cnt < end) _Values.RemoveRange(cnt + 1, end - cnt);
        }

        //------------------------------------------------------------
        // add some digits
        protected virtual void AddDigits(uint count)
        {
            _Values.AddRange(new byte[count]);
        }

        protected virtual void AddDigit(byte value)
        {
            _Values.Add(value);
        }

        protected virtual void InsertDigits(uint count, int index = 0)
        {
            _Values.InsertRange(index, new byte[count]);
        }

        protected virtual void InsertDigit(byte value, int index = 0)
        {
            _Values.Insert(index, value);
        }

        protected virtual void resizeDigits(uint count)
        {
            if (count > size) _Values.AddRange(new byte[count-size]);
        }

        //------------------------------------------------------------
        // to convert into a string
        //------------------------------------------------------------
        // calc the binary algorithm
        public static uint log_bin(uint value)
        {
            uint result = 0;
            while (value > 1) { value >>= 1; ++result; };
            return result;
        }

        //------------------------------------------------------------
        // create a string to a base of a power of 2
        protected string toString_2ish_base(uint Base)
        {
            uint bits = log_bin(Base);

            // generate reversed string of digits
            CNumber_Integer tmp = (CNumber_Integer)Clone();
            string num = String.Empty;
            int cnt = 0;
            do
            {
                if (cnt == 4) { cnt = 0; num += '.'; }
                uint rem = tmp.ShiftRight(bits);
                num += (char)((rem > 9) ? (rem - 10 + 'A') : (rem + '0'));
                ++cnt;
            } while (!tmp.IsZero);
            return num;
        }

        //------------------------------------------------------------
        // create a string to a base that is not a power of 2
        protected string toString_odd_base(byte Base)
        {
            // numbers of digits separated by '.'
            int step = (Base == 10) ? 3 : 4;

            // generate reversed string of digits
            CNumber_Integer tmp = (CNumber_Integer)Clone();
            string num = String.Empty;
            int cnt = 0;
            do
            {
                if (cnt == step) { cnt = 0; num += '.'; }
                uint rem = tmp.Divide(Base);
                num += (char)((rem > 9) ? (rem - 10 + 'A') : (rem + '0'));
                cnt++;
            } while (!tmp.IsZero);
            return num;
        }

        //************************************************************
        // constructors
        protected CNumber_Integer() { setZero(); }

        //------------------------------------------------------------
        public CNumber_Integer(CNumber_Integer other)
        {
            if (other == null) { setZero(); }
            else { CopyFrom(other);  }
        }

        //------------------------------------------------------------
        public CNumber_Integer(int Value)
        {
            AsInteger = Value;
        }

        //------------------------------------------------------------
        public CNumber_Integer(long Value)
        {
            Clear();
            _Sign = (Value < 0.0);
            if (_Sign) Value = -Value;

            do
            {
                AddDigit((byte) Value);
                Value /= maxDigit;
            } while (Value >= 1);
        }

        //------------------------------------------------------------
        public CNumber_Integer(double Value)
        {
            Clear();
            _Sign = (Value < 0.0);
            if (_Sign) Value = -Value;

            do
            {
                AddDigit((byte) Value);
                Value /= maxDigit;
            } while (Value >= 1);
        }

        //------------------------------------------------------------
        public CNumber_Integer(string Str)
        {
            Clear();
            FromString(Str);
        }

        //------------------------------------------------------------
        // copy this one
        public override CNumber Clone() { return new CNumber_Integer(this); }

        //------------------------------------------------------------
        public override CNumberType MyType { get { return CNumberType.cnt_Integer; } }

        // generate the base indicator string
        public static string getBaseIndication(uint Base)
        {
            switch (Base)
            {
                case 2: return "0b";
                case 8: return "0o";
                case 10: return "";
                case 16: return "0x";
            }
            return '[' + Base.ToString() + ']';
        }

        //------------------------------------------------------------
        // convert into a string
        public override string ToString(uint Base)
        {
            // first the sign and the base indication
            string str = (_Sign) ? "-" : "" + getBaseIndication(Base);

            // if the base is a power of 2
            // let's calc the binary logarithm
            bool poweroftwo = ((Base & (Base - 1)) == 0);
            string num = (poweroftwo) ? toString_2ish_base(Base) : toString_odd_base((byte) Base);

            for (int i = num.Length - 1; i >= 0; i--) str += num[i];
            return str;
        }

        //------------------------------------------------------------
        public override bool FromString(ref string Input)
        {
            Clear();

            // Extract the base of the representation
            byte b = (byte) CalcBase(ref Input);
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
                    Addition((byte)n);
                }
                Input = Input.Remove(0, 1);
            };

            return true;
        }

        //------------------------------------------------------------
        // property to set or get the value as integer
        public override int AsInteger
        {
            get
            {
                int begin = ((size > intDigits) ? intDigits : size) - 1;
                int result = _Values[begin];

                for (int i = begin - 1; i >= 0; --i)
                {
                    result <<= bitsDigit;
                    result += _Values[i];
                }
                return (_Sign) ? -result : result;
            }

            set
            {
                Clear();
                _Sign = (value < 0.0);
                do
                {
                    AddDigit((byte) value);
                    value >>= bitsDigit;
                } while (value >= 1);
            }
        }

        //------------------------------------------------------------
        // the little helpers
        public override bool IsZero { get { return (size == 1 && _Values[0] == 0); } }

        public override bool IsNegative { get { return _Sign; } }

        public override bool Equals(MMC.Numbers.CNumber other)
        {
            if (other.MyType != MyType) return false;

            CNumber_Integer o = (CNumber_Integer)other;
            if (size != o.size) return false;

            for (int i = 0; i < size; i++)
            {
                if (_Values[i] != o._Values[i]) return false;
            }
            return true;
        }

        public override int Compare(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();

            CNumber_Integer o = (CNumber_Integer)other;

            int t_len = size;
            int o_len = o.size;
            if (o_len > t_len) return -1;
            if (o_len < t_len) return 1;

            --t_len;
            while (t_len > 0 && o._Values[t_len] == _Values[t_len]) --t_len;
            return Math.Sign(_Values[t_len] - o._Values[t_len]);
        }

        //------------------------------------------------------------
        // multiply by a power of 2
        // return the remainder
        public uint ShiftLeft(uint Count)
        {
            //TODO think of using the "InsertRange" more efficient
            byte rem = 0;
            uint Add = Count / bitsDigit;
            Count %= bitsDigit;

            if (Count > 0)
            {
                int NegCount = (int) (bitsDigit - Count);
                int cnt = size;
                for (int i = 0; i < cnt; ++i)
                {
                    int v = (int) _Values[i];
                    _Values[i] <<= (int)Count;
                    _Values[i] |= rem;
                    rem = (byte) (v >> NegCount);
                }
            }

            if (Add > 0) InsertDigits(Add);
            if (rem > 0) AddDigit(rem);

            return rem;
        }

        //------------------------------------------------------------
        // devide by a power of 2
        // return the remainder
        public uint ShiftRight(uint Count)
        {
            uint Remove = Count / bitsDigit;
            Count %= bitsDigit;

            if (Remove > 0)
            {
                if (Remove >= size)
                {
                    uint v = _Values[size - 1];
                    setZero();
                    return v;
                }
                _Values.RemoveRange(0, (int) Remove);
            }

            uint mask = (uint) ((1 << (int)Count) - 1);
            int NegCount = (int) (bitsDigit - Count);
            byte rem = 0;

            for (int i = _Values.Count-1; i >= 0; --i)
            {
                byte v = (byte) (_Values[i] & mask);
                _Values[i] >>= (int) Count;
                _Values[i] |= (byte) ((rem << NegCount));
                rem = v;
            }
            Trim();
            return rem;
        }

        //------------------------------------------------------------
        // add this value to the digits starting at position index
        // Value is ushort to
        // - be big enough to contain the product of 2 bytes
        // - and small enough to avoid an uint overflow when
        //   rem += _Values[index];
        protected void Addition(ushort Value, int index = 0)
        {
            if (Value == 0) return;

            resizeDigits((uint)index);  // make sure we have enough digits

            uint rem = Value;
            for (; (Value > 0) && (index < size); ++index)
            {
                rem+= _Values[index];
                _Values[index] = (byte) rem;
                rem >>= bitsDigit;
            }

            // and add the overflow
            while (rem > 0)
            {
                AddDigit((byte)rem);
                rem >>= bitsDigit;
            } ;
        }

        //------------------------------------------------------------
        // add a number
        // the native way to take care of the overflow
        // is checking after the summation if the sum is less than
        // one of the summands, this would mean overflow
        // but this would need some more conditional branches
        // so I choose a way with conversions from byte to uint
        // to let the .Net calc the overflow
        public void Addition(CNumber_Integer other)
        {
            resizeDigits((uint) other.size);                // make sure we are big enough for both

            uint rem = 0;                                   // the remainder
            int o_len = other.size;                         // the other length
            int t_len = size;                               // our own length
            int len = (t_len < o_len) ? t_len : o_len;      // calc the minimum length
            int i = 0;                                      // start with the lowest digit
            for (; i < len; ++i)                            // first add all common digits
            {
                rem += other._Values[i];                    // val = rem + val + other.val
                rem += _Values[i];
                _Values[i] = (byte)rem;
                rem >>= bitsDigit;                          // rem = overflow
            }

            for (; i < o_len; ++i)                          // now copy the remaining from the other
            {
                rem += other._Values[i];                    // don't forget the remainder
                _Values[i] = (byte) rem;
                rem >>= bitsDigit;
            }

            for (; (rem > 0) && (i < t_len); ++i)           // now increase our own remaining digits
            {                                               // as long as the remainder is non-zero
                rem += _Values[i];
                _Values[i] = (byte)rem;
                rem >>= bitsDigit;
            }
            if (rem > 0) AddDigit((byte)rem);               // still something left, just add it
        }

        //------------------------------------------------------------
        protected void Multiply(byte Value)
        {
            if (Value == 0) { Clear(); return; }
            if (Value == 1) return;

            uint rem = 0;
            for (int i = 0; i < _Values.Count; ++i)
            {
                uint v = ((uint) _Values[i]) * ((uint) Value) + rem;
                rem = (uint) (v >> bitsDigit);
                _Values[i] = (byte)v;
            }
            if (rem > 0) _Values.Add((byte) rem);
        }

        //------------------------------------------------------------
        // divide by an integer and return the remainder
        protected byte Divide(byte Value)
        {
            if (Value == 0) throw new CNumberException("Division by Zero!");
            if (Value == 1) return 0;

            int rem = 0;
            for (int i = _Values.Count-1; i >=0; i--)
            {
                int v = (rem << bitsDigit);
                v += _Values[i];
                _Values[i] = (byte) Math.DivRem(v, (int)Value, out rem);
            }
            Trim();
            return (byte) rem;
        }

        //------------------------------------------------------------
        // divide by another number and return the remainder
        protected virtual CNumber_Integer Divide(CNumber_Integer other)
        {
            CNumber_Integer Save = new CNumber_Integer(this);       // save the old value
            setZero();                                              // set the initial result to 0

            int o_len = other._Values.Count;
            int t_len = _Values.Count;
            int pos = t_len - o_len;
            if (pos < 0) return Save;                               // fewer digits means the result is zero and the remainder are just we

            CNumber_Integer Rem = new CNumber_Integer();            // set the remainder to 0
            int len = o_len - 1;
            while (pos >= 0)
            {
                Rem.InsertDigit(Save._Values[pos]);                 // take the next digit

                byte q = Rem._Values[len];
                q /= other._Values[len];                            // and make an estimate for the quotient 

                CNumber_Integer tmp = new CNumber_Integer(other);   // and calculate the product for this estimated value
                tmp._Sign = false;
                tmp.Multiply(q);
                Rem.Subtraction(tmp);                               // see how good we estimated
                if (Rem._Sign)                                      // if we are too low just correct by one
                {
                    --q;
                    Rem.add(other);
                }

                InsertDigit(q);                                     // and write down the quotients digit
                --pos;                                              // walk one further in our (this) digits
            }

            _Sign = (_Sign != other._Sign);
            Trim();
            return Rem;
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
                byte v = other._Values[i]; if (rem) ++v;
                rem = (v > _Values[i]);
                _Values[i]-=v;
            }
            for (; i < o_len; i++)                          // now copy the remaining from the other
            {
                byte v = other._Values[i]; if (rem) ++v;
                _Values.Add((byte) (-v));                   // = 0-v;
                rem = true;                                 // allways an overflow
            }

            for (; rem && (i < t_len); ++i)                 // now decrease our own remaining digits
            {
                rem = (_Values[i] == 0);
                --_Values[i];
            }

            if (rem)                                        // if we have an overflow we have to 
            {                                               // negate the result
                _Values[0] = (byte) (-_Values[0]);
                for (int k = 1; k < _Values.Count; k++)
                {
                    _Values[k] ^= 0xFF;
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
                _Values[i] ^= 0xFF;
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

        // written multiplication
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
                tmp._Values.InsertRange(0, new byte[i]);
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
            int cnt = _Values.Count;
            // calc the different interims (could be done in parallel)
            CNumber_Integer[] Results = new CNumber_Integer[cnt];
            for (int i = 0; i < cnt; ++i)
            {
                CNumber_Integer tmp = new CNumber_Integer(this);
                tmp.Multiply(_Values[i]);
                tmp._Values.InsertRange(0, new byte[i]);
                Results[i] = tmp;
            }

            // now add them all up
            CNumber_Integer res = new CNumber_Integer(Results[0]);
            for (int i = 1; i < cnt; i++)
            {
                res.Addition(Results[i]);
            }

            res.Trim();
            return res;
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
