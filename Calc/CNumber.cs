//********************************************************************
// CNumber - Wrapper class for a number type
// (c) Jan 2010 MMC
//********************************************************************
using System;

//********************************************************************
namespace MMC.Numbers
{

    //********************************************************************
    [global::System.Serializable]
    public class CNumberException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public CNumberException() { }
        public CNumberException(string message) : base(message) { }
        public CNumberException(string message, Exception inner) : base(message, inner) { }
        protected CNumberException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

//********************************************************************
    public abstract class CNumber
    {
        
        //------------------------------------------------------------
        // to tell one from the other
        public enum CNumberType
        {
            cnt_Base,
            cnt_Double,
            cnt_Integer,
            cnt_Max
        }

        //------------------------------------------------------------
        // default constructor
        public CNumber() {}

        //------------------------------------------------------------
        // Create a number of the very same value
        public abstract CNumber Clone();

        //------------------------------------------------------------
        // identify my self
        public abstract CNumberType MyType { get; }

        //------------------------------------------------------------
        // convert into a string
        public abstract string ToString(uint Base);

        //------------------------------------------------------------
        // Default conversion to Hex
        public virtual string AsString { get {return ToString(16);} }

        //------------------------------------------------------------
        // tries to convert the Input string into the number
        // it starts with the first character and
        // returns the string starting with the first character
        // that could not be interpreted
        // returns true if a number could be extracted
        // when returning false, the original value is not changed
        public abstract bool FromString(ref string Input);
        public virtual bool FromString(string Input) { return FromString(ref Input); }

        //------------------------------------------------------------
        // extract the representation base from a string
        // if failing return 0
        // if the value is 0 return 1
        // else return the base
        protected virtual uint CalcBase(ref string Input)
        {
            //--------------------------------------------------------
            // make sure we have something to do
            if (Input != null) Input = Input.Trim();
            if (string.IsNullOrEmpty(Input)) { return 0; }

            //--------------------------------------------------------
            // check the base e.g. 0x...
            char c = Input[0];
            if (c > '0' && c <= '9') return 10;

            int Base = 0;
            if (c == '0')
            {
                Input = Input.Remove(0, 1);
                if (Input.Length < 2) { return 1; }
                switch (Input[0])
                {
                    case 'b':
                    case 'B': Base = 2; break;
                    case 'o':
                    case 'O': Base = 8; break;
                    case 'x':
                    case 'X': Base = 16; break;
                    default:
                        Base = 10;
                        break;
                }
                // if we changed the base, remove the base indicator
                if (Base != 10) Input = Input.Remove(0, 1);
            }
            else if (c == '[')
            {
                if (Input.Length < 3) return 0;
                int cnt = 1;
                while (cnt < Input.Length && Input[cnt] != ']') cnt++;
                if (cnt >= Input.Length || Input[cnt] != ']') return 0;
                if (!Int32.TryParse(Input.Substring(1, cnt - 1), out Base) || Base < 0) return 0;
                Input = Input.Remove(0, cnt+1);
            }
            return (uint) Base;
        }

        //------------------------------------------------------------
        // convert the first char into an integer value
        protected virtual int CalcValue(char c)
        {
            if (c <= '9' && c >= '0') return (c - '0');
            if (c <= 'z' && c >= 'a') return (c - 'a' + 10);
            if (c <= 'Z' && c >= 'A') return (c - 'A' + 10);
            return -1;
            // throw new CNumberException("This is not a valid digit " + c + "!");
        }

        //------------------------------------------------------------
        // convert into a simple integer
        public abstract int AsInteger { get; set; }

        //------------------------------------------------------------
        // some compare functions
        public abstract bool IsZero { get; }
        public abstract bool IsNegative { get; }
        public abstract int Compare(CNumber other);
        public virtual bool Equals(CNumber other) { return (Compare(other) == 0); }

        //------------------------------------------------------------
        // all mathematical functions should not change "this"
        // but return the result as a new instance
        //------------------------------------------------------------
        // basic mathematical functions
        public abstract CNumber neg();
        public abstract CNumber add(CNumber other);
        public abstract CNumber sub(CNumber other);
        public abstract CNumber mul(CNumber other);
        public abstract CNumber div(CNumber other);
        public abstract CNumber rem(CNumber other);

        //------------------------------------------------------------
        // the higher functions operations
        public abstract CNumber pow(CNumber a);
        public abstract CNumber sqr();
        public abstract CNumber sqrt();
        public abstract CNumber sin();
        public abstract CNumber sinh();
        public abstract CNumber cos();
        public abstract CNumber cosh();
        public abstract CNumber tan();
        public abstract CNumber tanh();
        public abstract CNumber rad();
        public abstract CNumber deg();

        //------------------------------------------------------------
        // logical functions
        public abstract CNumber not();
        public abstract CNumber xor(CNumber other);
        public abstract CNumber or(CNumber other);
        public abstract CNumber and(CNumber other);

        //------------------------------------------------------------
        // general helpers
        
        // Sets the seconds since 1/1/19070
        public virtual void Date(CNumber year, CNumber month, CNumber day)
        {
            DateTime dt = new DateTime(year.AsInteger, month.AsInteger, day.AsInteger);
            long dt_s = dt.ToFileTimeUtc();
            long bgn_s = 0x019db1ded53e8000;    // DateTime(1970, 1, 1).ToFileTimeUtc()

            long diff = dt_s - bgn_s;           // diff in 100 ns
            diff /= 10 * 1000 * 1000;           // diff in s
            AsInteger = (int) diff;
        }

        // Sets the day in the week (0: sunday .. 6:saturday)
        public virtual void Day(CNumber year, CNumber month, CNumber day)
        {
            DateTime dt = new DateTime(year.AsInteger, month.AsInteger, day.AsInteger);
            AsInteger = (int) dt.DayOfWeek;
        }

    }

//********************************************************************
} // end of NameSpace MMC.Number

//********************************************************************
// END OF FILE CNumber
//********************************************************************
