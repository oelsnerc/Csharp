//********************************************************************
// CNumber_Double - <type description here>
// (c) Dez 2010 MMC
//********************************************************************
using System;

//********************************************************************
namespace MMC.Numbers
{
    public class CNumber_Double : CNumber
    {
        //------------------------------------------------------------
        // the actual Value of this wrapper
        protected double _Value;

        //------------------------------------------------------------
        // default constructor
        public CNumber_Double() { _Value = 0.0; }
        
        //------------------------------------------------------------
        // constructor with initialize
        public CNumber_Double(double Value) { _Value = Value; }

        //------------------------------------------------------------
        // copy constructor
        public CNumber_Double(CNumber_Double other) { _Value = other._Value; }

        //------------------------------------------------------------
        // clone my self
        public override CNumber Clone() { return new CNumber_Double(this); }

        //------------------------------------------------------------
        // identify myself
        public override CNumberType MyType { get { return CNumberType.cnt_Double; } }
        
        //------------------------------------------------------------
        // convert into a string
        public override string ToString(uint Base)
        {
            // that is the result string
            string res = (_Value < 0.0) ? "-" : "";
            switch (Base)
            {
                case 2: res += "0b"; break;
                case 8: res += "0o"; break;
                case 10: break;
                case 16: res += "0x"; break;
                default: res += '[' + Base.ToString() + ']';
                    break;
            }

            // first the easy way out
            if (_Value == 0.0) return res + '0';

            // prepare conversion
            double FBase = (double)Base;
            double FVal = Math.Abs(_Value);
            double dExp = Math.Floor(Math.Log(FVal, FBase));
            double FCon = Math.Pow(FBase, dExp);
            int FExp = (int)dExp;

            // correct the starting base if the log was not exact
            if (FBase * FCon <= FVal) { FCon *= FBase; FExp++; }

            // Calc the number of exact digits by IEEE
            int FCnt = (int)(56.0 * Math.Log(2.0, FBase));

            // Insert the leading zeros if the FVal is less than one
            if (FExp < 0)
            {
                res += "0,";
                res = res.PadRight(res.Length - 1 - FExp, '0');
                FCnt -= FExp;
            }

            // now add the digits
            while (FCnt > 0)
            {
                // calc the next digit
                double q = Math.Floor(FVal / FCon);
                res += (char)(((q < 10.0) ? '0' : 'A' - 10) + q);
                //res += (char) ((q < 10.0) ? '0' : ('A' - 10)) + ((char)q);

                // adjust the value
                FCnt--;                 // one digit down
                FVal -= (q * FCon);     // calc the remainder
                if (FVal == 0.0) break; // and stop if nothing left

                // adjust the conversion parameters
                FCon /= FBase;
                FExp--;
                if (FExp == -1) res += ',';
            }

            if (FExp > 0)
            {
                res = res.PadRight(FExp + res.Length, '0');
            }
            else
            {
                res = res.TrimEnd('0');
            }

            return res;
        }

        //------------------------------------------------------------
        // extract from a string
        public override bool FromString(ref string Input)
        {
            // Extract the base of the representation
            uint b = CalcBase(ref Input);
            if (b == 0) return false;       // if illegal ... return nothing
            if (b == 1) { _Value = 0.0; return true; }

            double Base = (double)b;        // prepare for float calculations

            // calc the integer part (Input.Length >= 1)
            double I = 0.0;
            while (Input.Length >= 1)
            {
                int n = CalcValue(Input[0]);
                if (n<0 || n>(Base-1)) break;
                I*=Base;
                I+=n;
                Input = Input.Remove(0, 1);
            };

            // calc the fraction part
            double F = 0.0;
            if (Input.Length > 1 && Input[0] == ',')
            {
                double Power = 1.0;
                Input = Input.Remove(0, 1);
                while (Input.Length >= 1)
                {
                    int n = CalcValue(Input[0]);
                    if (n < 0 || n > (Base - 1)) break;
                    F *= Base;
                    F += n;
                    Power *= Base;
                    Input = Input.Remove(0, 1);
                };
                F /= Power;
            }

            _Value = I + F;
            return true;
        }

        //------------------------------------------------------------
        // convert into a simple int
        public override int AsInteger
        { 
            get { return (int)_Value; }
            set { _Value = (double)value; }
        }

        //------------------------------------------------------------
        // some little helpers
        public override bool IsZero { get { return (_Value == 0.0); } }
        public override bool IsNegative { get { return (_Value < 0.0); } }
        public override bool Equals(CNumber other)
        {
            if (other.MyType != MyType) return false;
            return (_Value == ((CNumber_Double)other)._Value);
        }

        //------------------------------------------------------------
        // which one is bigger?
        public override int Compare(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            return Math.Sign(_Value - ((CNumber_Double)other)._Value);
        }

        //------------------------------------------------------------
        // basic mathematical functions
        public override CNumber neg()
        {
            return new CNumber_Double(-_Value);
        }

        public override CNumber add(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            return new CNumber_Double(_Value + ((CNumber_Double)other)._Value);
        }

        public override CNumber sub(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            return new CNumber_Double(_Value - ((CNumber_Double)other)._Value);
        }

        public override CNumber mul(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            return new CNumber_Double(_Value * ((CNumber_Double)other)._Value);
        }

        public override CNumber div(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            return new CNumber_Double(_Value / ((CNumber_Double)other)._Value);
        }

        public override CNumber rem(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            
            long result;
            Math.DivRem((long) _Value, (long) (((CNumber_Double) other)._Value), out result);
            return new CNumber_Double(result);
        }

        //------------------------------------------------------------
        // the little operations
        public override CNumber pow(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            return new CNumber_Double(Math.Pow(_Value, ((CNumber_Double)other)._Value));
        }

        public override CNumber sqr()   { return new CNumber_Double(_Value * _Value); }
        public override CNumber sqrt()  { return new CNumber_Double(Math.Sqrt(_Value)); }
        public override CNumber sin()   { return new CNumber_Double(Math.Sin(_Value)); }
        public override CNumber sinh()  { return new CNumber_Double(Math.Sinh(_Value)); }
        public override CNumber cos()   { return new CNumber_Double(Math.Cos(_Value)); }
        public override CNumber cosh()  { return new CNumber_Double(Math.Cosh(_Value)); }
        public override CNumber tan()   { return new CNumber_Double(Math.Tan(_Value)); }
        public override CNumber tanh()  { return new CNumber_Double(Math.Tanh(_Value)); }
        public override CNumber rad()   { return new CNumber_Double(_Value * Math.PI / 180); }
        public override CNumber deg()   { return new CNumber_Double(_Value * 180/ Math.PI); }


        //------------------------------------------------------------
        // logical functions
        public override CNumber not()
        {
            return new CNumber_Double(((ulong)_Value) ^ ((ulong)0xFFFFFFFFFFFFFFFF));
        }
        
        public override CNumber and(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            return new CNumber_Double(((long)_Value) & ((long)((CNumber_Double)other)._Value));
        }

        public override CNumber or(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            return new CNumber_Double(((long)_Value) | ((long)((CNumber_Double)other)._Value));
        }

        public override CNumber xor(CNumber other)
        {
            if (other.MyType != MyType) throw new NotImplementedException();
            return new CNumber_Double(((long)_Value) ^ ((long)((CNumber_Double)other)._Value));
        }
    }

//********************************************************************
} // end of NameSpace MMC.Number

//********************************************************************
// END OF FILE CNumber_Double
//********************************************************************
