//********************************************************************
// CEnvironment - <type description here>
// (c) Feb 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;

//********************************************************************
namespace MMC.Calc
{

//********************************************************************
    class CEnvironment
    {
        //************************************************************
        // class to provide easy access to a map of operations
        protected class COperations
        {
            private SortedDictionary<string, MMC.Calc.CTerm_Base> _Operations;
            int _LengthMax;
            public COperations()
            {
                _Operations = new SortedDictionary<string, CTerm_Base>();
                _LengthMax = 0;
            }

            //--------------------------------------------------------
            // add or replace a definition
            public void Set(MMC.Calc.CTerm_Base Term)
            {
                if (Term != null)
                {
                    string Name = Term.Name;
                    _Operations[Name] = Term;
                    if (Name.Length > _LengthMax) _LengthMax = Name.Length;
                }
            }

            //------------------------------------------------------------
            // find the longest match starting at pos in Term
            public MMC.Calc.CTerm_Base Get(string Term, int pos)
            {
                int len = Term.Length - pos;
                MMC.Calc.CTerm_Base rc = null;
                for (int i = (len > _LengthMax) ? _LengthMax : len; i > 0; i--)
                {
                    if (_Operations.TryGetValue(Term.Substring(pos, i),out rc)) break;
                }
                return rc;
            }
        }

        //------------------------------------------------------------
        // the various lists
        protected MMC.Calc.Functions.CTerm_Const _Result;   // holds the result of the last calculation
        protected COperations _Operations;  // the LookUpTable for the pre defined operations
        protected COperations _Constants;   // the LookUpTable for the pre defined constants
        protected COperations _Support;     // the LookUpTable for the support operations (e.g. ();=)
        protected COperations _Variables;   // the LookUpTable for the user defined variables
        protected MMC.Numbers.CNumber.CNumberType _NumberType; // the type of numbers to create

        //------------------------------------------------------------
        // standard constructor
        public CEnvironment(MMC.Numbers.CNumber.CNumberType NumberType)
        {
            //--------------------------------------------------------
            // set up the type of numbers to generate
            _NumberType = NumberType;

            //--------------------------------------------------------
            // initialize the last result with 0
            _Result = new MMC.Calc.Functions.CTerm_Const("RES", NewNumber(0.0));
            
            //--------------------------------------------------------
            // Add the functions
            _Operations = new COperations();
            _Operations.Set(new MMC.Calc.Functions.CTerm_AND("AND"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_OR("OR"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_XOR("XOR"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_NOT("NOT"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Add("+"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Sub("-"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Mul("*"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Div("/"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Mod("%"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Power("^"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Square("sqr"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_SquareRoot("sqrt"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Sinus("sin"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_SinusHyperbolicus("sinh"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Cosinus("cos"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_CosinusHyperbolicus("cosh"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Tangens("tan"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_TangensHyperbolicus("tanh"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Radiant("rad"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Degree("deg"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Diagonale("diag"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Date("date"));
            _Operations.Set(new MMC.Calc.Functions.CTerm_Day("day"));

            //--------------------------------------------------------
            // add the initial constants
            _Constants = new COperations();
            _Constants.Set(new MMC.Calc.Functions.CTerm_Const("pi", NewNumber(Math.PI)));
            _Constants.Set(new MMC.Calc.Functions.CTerm_Const("e", NewNumber(Math.E)));
            _Constants.Set(_Result);

            //--------------------------------------------------------
            // add the supporting cast
            _Support = new COperations();
            _Support.Set(new MMC.Calc.Functions.CTerm_Bracket("("));
            _Support.Set(new MMC.Calc.Functions.CTerm_BracketClose(")"));
            //_Support.Set(new MMC.Calc.Functions.CTerm_Separator(",")); // possible decimal separator!
            _Support.Set(new MMC.Calc.Functions.CTerm_Separator(";"));
            _Support.Set(new MMC.Calc.Functions.CTerm_Assignment("="));

            //--------------------------------------------------------
            // create the table for the user defined variables
            _Variables = new COperations();
        }

        //------------------------------------------------------------
        // helper to create a new number
        public MMC.Numbers.CNumber NewNumber(double Value)
        {
            MMC.Numbers.CNumber res = null;
            switch (_NumberType)
            {
                case MMC.Numbers.CNumber.CNumberType.cnt_Double:
                    res = new MMC.Numbers.CNumber_Double(Value);
                    break;
                case MMC.Numbers.CNumber.CNumberType.cnt_Integer:
                    res = new MMC.Numbers.CNumber_Integer(Value);
                    break;
                default:
                    break;
            }
            return res;
        }

        //------------------------------------------------------------
        // add a new or replace a function/variable
        public void SetVariable(MMC.Calc.CTerm_Base Term) { _Variables.Set(Term); }

        //------------------------------------------------------------
        // combine two terms, e.g. "2sqrt2" = "2*sqrt2"
        public MMC.Calc.CTerm_Base Combine(MMC.Calc.CTerm_Base A, MMC.Calc.CTerm_Base B)
        {

            return new MMC.Calc.Functions.CTerm_Mul("*", A, B);
        }
        
        //------------------------------------------------------------
        // analyse the string and return the operation
        public MMC.Calc.CTerm_Base FindOp(ref string Term)
        {
            //--------------------------------------------------------
            // make sure we have something to do
            if (Term != null)
            {
                int length = Term.Length;
                int idx = 0;
                while (idx < length)
                {
                    char c = Term[idx];
                    if (c != ' ' && c != '\t' && c != '\n' && c != '\r') break;
                    idx++;
                }
                Term = Term.Substring(idx);
            }
            if (string.IsNullOrEmpty(Term)) { return null; }

            //--------------------------------------------------------
            // find the match in the various operation maps
            MMC.Calc.CTerm_Base Op = _Operations.Get(Term, 0);
            if (Op != null)
            {
                Term = Term.Remove(0, Op.Name.Length);
                return Op.Clone();
            }

            Op = _Support.Get(Term, 0);
            if (Op != null)
            {
                Term = Term.Remove(0, Op.Name.Length);
                return Op.Clone();
            }

            Op = _Constants.Get(Term, 0);
            if (Op != null)
            {
                Term = Term.Remove(0, Op.Name.Length);
                return Op;  // Do not clone a constant
            }

            Op = _Variables.Get(Term, 0);
            if (Op != null)
            {
                Term = Term.Remove(0, Op.Name.Length);
                return Op;  // Do not clone a variable
            }

            //--------------------------------------------------------
            // is it a number
            MMC.Numbers.CNumber N = NewNumber(0.0);
            if (N.FromString(ref Term)) return new MMC.Calc.Functions.CTerm_Number(N);

            //--------------------------------------------------------
            // check for an assignment
            for (int pos = 1; pos < Term.Length; pos++)
            {
                Op = _Support.Get(Term, pos);
                if (Op != null && Op.Type == CTerm_Base.TTermType.Assignment)
                {
                    MMC.Calc.Functions.CTerm_Variable Var = new MMC.Calc.Functions.CTerm_Variable(Term.Substring(0, pos));
                    Term = Term.Remove(0, pos);
                    return Var;
                }
            }

            //--------------------------------------------------------
            // nothing found so something's wrong somewhere
            throw new CTermException("Unknown Function or Variable!");
        }

        //------------------------------------------------------------
        // add a term to the Environment
        public MMC.Numbers.CNumber Evaluate(string NewTerm)
        {
            CTerm_Base Term = CTerm_Base.Compile(ref NewTerm,this);
            _Result.Value = (Term != null) ? Term.Calc(this) : null;
            return _Result.Value;
        }

        //------------------------------------------------------------
        // return the result of the last term
        public MMC.Numbers.CNumber LastResult { get { return (_Result != null) ? _Result.Value : null; } }
    }
}

//********************************************************************
// END OF FILE CEnvironment
//********************************************************************
