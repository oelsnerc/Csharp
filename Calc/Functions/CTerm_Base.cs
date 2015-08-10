//********************************************************************
// CTerm_Base - <type description here>
// (c) Jan 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;

//********************************************************************
namespace MMC.Calc {

//********************************************************************
    [global::System.Serializable]
    public class CTermException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public CTermException() { }
        public CTermException(string message) : base(message) { }
        public CTermException(string message, Exception inner) : base(message, inner) { }
        protected CTermException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

//********************************************************************
    abstract class CTerm_Base
    {
        //------------------------------------------------------------
        public enum TPriority
        {
            none = 0,
            end,
            separator,
            group,
            ring,
            power,
            function,
            constant,
            begin,
            max
        };

        //------------------------------------------------------------
        public enum TTermType
	    {
            none = 0,
            Function,
            TwoOps,
            Variable,
            PostOps,
            Term,
            Constant,
            Number,
            Bracket,
            BracketClose,
            Assignment,
            Seperator,
            max
	    }

        //------------------------------------------------------------
        protected string _Name;
        protected TPriority _Priority;
        protected TTermType _TermType;

        
        //------------------------------------------------------------
        // construct this object with a function or variable name
        // e.g. "+" or "sin" or "pi"
        public CTerm_Base(string Name, TPriority Priority, TTermType TermType)
        {
            _Name = Name;
            _Priority = Priority;
            _TermType = TermType;
        }

        //------------------------------------------------------------
        // create a new instance of the same type
        public abstract CTerm_Base Clone();

        //------------------------------------------------------------
        // return the "name" of this operation
        public string Name { get { return _Name; } }

        //------------------------------------------------------------
        // return the algebraic priority
        public TPriority Priotity { get { return _Priority; } }

        //------------------------------------------------------------
        // return the term identifier
        public TTermType Type { get { return _TermType; } }

        //------------------------------------------------------------
        // returns if this term is not depending on any variables
        public abstract bool IsConstant { get; }

        //------------------------------------------------------------
        // check if we can replace this term by one "subterm"
        // e.g. (2+3) -> 2+3 -> 5
        public abstract CTerm_Base Replace(MMC.Calc.CEnvironment Env);

        //------------------------------------------------------------
        // translate the Input string into the Term tree
        protected abstract CTerm_Base Compile(ref string Term,
                                              Stack<CTerm_Base> Stack,
                                              MMC.Calc.CEnvironment Env);
        
        //------------------------------------------------------------
        // Calc the actual result
        public abstract MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env);


        //************************************************************
        // static helpers to generate a Term
        //************************************************************
        // try to extract the next term from the string
        // put the term on the stack
        protected static CTerm_Base GetNextTerm(ref string Term,
                                         Stack<CTerm_Base> Stack,
                                         MMC.Calc.CEnvironment Env,
                                         TPriority Prio)
        {
            CTerm_Base Result = null;
            int Stack_Count = Stack.Count;

            while (!String.IsNullOrEmpty(Term) || Result != null)
            {
                // if we didn't find a term yet
                if (Result == null)
                {
                    // find next operation
                    Result = Env.FindOp(ref Term);

                    // if we didn't find anything ...
                    if (Result == null) break;
                }

                // This term ends if prio is less or equal, i.e. 2-3+2 = (2-3)+2
                if (Result.Priotity <= Prio) break;

                // Let the Operation get their operands from stack or string
                CTerm_Base Next = Result.Compile(ref Term, Stack, Env);
                
                // and try to reduce the term as much as possible
                TTermType Type = Result.Type;
                Result = Result.Replace(Env);

                // check if we have to combine this Term with the TopOfStack
                // but only if the second term is not a number
                // e.g. "2sqrt2" = "2*sqrt2"
                if ((Type != TTermType.Number) && (Type != TTermType.Seperator) &&
                    (Stack.Count == Stack_Count + 1))
                {
                    Result = Env.Combine(Stack.Pop(), Result);
                    Result = Result.Replace(Env);
                }

                // put the Operation onto the stack
                Stack.Push(Result);

                // and proceed with the next term (or null if none)
                Result = Next;
            }
            return Result;
        }

        //------------------------------------------------------------
        public static CTerm_Base Compile(ref string Term, MMC.Calc.CEnvironment Env)
        {
            Stack<CTerm_Base> Stack = new Stack<CTerm_Base>();
            CTerm_Base Pending = GetNextTerm(ref Term, Stack, Env, TPriority.none);

            if (Pending != null) throw new CTermException("Could not compile completely!");
            if (Stack.Count != 1) throw new CTermException("Mathematical operation missing!");

            return Stack.Pop();
        }
    }

//********************************************************************
} // end of NameSpace MMC.Calc
//********************************************************************
// END OF FILE CTerm_Base
//********************************************************************
