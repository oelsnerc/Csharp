//********************************************************************
// CTerm_Functions - The base class for all functions that need parameters
// (c) Nov 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;
using System.Reflection;

//********************************************************************
namespace MMC.Calc.Functions
{
    abstract class CTerm_Functions : CTerm_Base
    {
        protected List<CTerm_Base> _Terms;  // contains the parameters
        protected int _TermCount;           // the number of parameters

        //------------------------------------------------------------
        // standard constructor
        public CTerm_Functions(string Name, int TermCount)
            : base(Name,TPriority.function,TTermType.Function)
        {
            _Terms = new List<CTerm_Base>();
            _TermCount = TermCount;
        }

        //------------------------------------------------------------
        // clone the function
        public override CTerm_Base Clone()
        {
            Type myType = GetType();
            Type[] types = new Type[1];
            types[0] = typeof(string);
            ConstructorInfo myConstructor = myType.GetConstructor(types);

            Object[] args = new Object[1];
            args[0] = _Name;

            CTerm_Functions Term = (CTerm_Functions) myConstructor.Invoke(args);

            foreach (CTerm_Base term in _Terms)
            {
                Term._Terms.Add(term.Clone());
            }

            return Term;
        }

        //------------------------------------------------------------
        // check if we're constant
        public override bool IsConstant
        {
            get 
            {
                foreach (CTerm_Base term in _Terms)
                {
                    if (!term.IsConstant) return false;
                }
                return true;
            }
        }

        //------------------------------------------------------------
        // check if we have all the terms
        public bool IsAssigned { get { return (_Terms.Count == _TermCount); } }

        //------------------------------------------------------------
        // make sure we can calc
        public void Check() { if (!IsAssigned) throw new CTermException(_Name + "not initialized!"); }

        //------------------------------------------------------------
        // try to replace by the calculated constant
        public override CTerm_Base Replace(CEnvironment Env)
        {
            if (!IsConstant) return this;
            return new CTerm_Number(Calc(Env));
        }

        //------------------------------------------------------------
        // extract the parameters
        protected override CTerm_Base Compile(ref string Term, Stack<CTerm_Base> Stack, CEnvironment Env)
        {
            _Terms = new List<CTerm_Base>();
            int StackSize = Stack.Count;

            CTerm_Base Pending = GetNextTerm(ref Term, Stack, Env, Priotity);
            StackSize = Stack.Count - StackSize;

            if (StackSize == 1 && Stack.Peek().Type == TTermType.Bracket)
            {
                CTerm_Bracket Brackets = (CTerm_Bracket)Stack.Pop();
                StackSize = Brackets.CopyTo(Stack);
                // Brackets = null;
            }

            if (StackSize != _TermCount)
            {
                string Msg = (StackSize < _TermCount) ? "missing operand(s)" : "too many operands";
                Msg += " for " + _Name + "!";
                throw new CTermException(Msg);
            }

            while (StackSize > 0)
            {
                _Terms.Add(Stack.Pop());
                StackSize--;
            }

            return Pending;
        }

    }
}

//********************************************************************
// END OF FILE CTerm_Functions
//********************************************************************
