//********************************************************************
// CTerm_Bracket - Implements the brackets in a term
// (c) Nov 2010 MMC
//********************************************************************
using System.Collections.Generic;

//********************************************************************
namespace MMC.Calc.Functions
{
    //********************************************************************
    class CTerm_Bracket : MMC.Calc.CTerm_Base
    {
        //------------------------------------------------------------
        protected Stack<CTerm_Base> _Stack;

        //------------------------------------------------------------
        // Constructor for Constants
        public CTerm_Bracket(string Name)
            : base(Name,TPriority.begin,TTermType.Bracket)
        {
            _Stack = new Stack<CTerm_Base>();
        }

        //------------------------------------------------------------
        // copies the terms in the stack to the target stack
        // returns the number of terms copied
        public int CopyTo(Stack<CTerm_Base> Stack)
        {
            foreach (CTerm_Base term in _Stack)
            {
                Stack.Push(term.Clone());
            }
            return _Stack.Count;
        }

        //------------------------------------------------------------
        public override CTerm_Base Clone()
        {
            CTerm_Bracket a = new CTerm_Bracket(_Name);
            CopyTo(a._Stack);
            return a;
        }

        //------------------------------------------------------------
        // replace this by the one and only subterm
        public override CTerm_Base Replace(CEnvironment Env)
        {
            if (_Stack.Count == 1) return _Stack.Pop();
            return this;
        }

        //------------------------------------------------------------
        // should never be evaluated
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            throw new CTermException("Brackets can not be evaluated!");
        }

        //------------------------------------------------------------
        // check if only one term and if this one is const
        public override bool IsConstant
        {
            get { return (_Stack.Count == 1 && _Stack.Peek().IsConstant); }
        }

        //------------------------------------------------------------
        // translate the Input string into the Term tree
        protected override CTerm_Base Compile(ref string Term, Stack<CTerm_Base> Stack, CEnvironment Env)
        {
            _Stack.Clear();
            CTerm_Base Pending = GetNextTerm(ref Term, _Stack, Env, TPriority.end);
            if (_Stack.Count < 1) throw new CTermException("missing operand in brackets!");

            if (Pending == null || Pending.Priotity != TPriority.end)
                throw new CTermException("missing closing bracket!");


            return null;
        }
    }

    //********************************************************************
    class CTerm_BracketClose : MMC.Calc.CTerm_Base
    {
        //------------------------------------------------------------
        // Constructor for Constants
        public CTerm_BracketClose(string Name) : base(Name,TPriority.end,TTermType.BracketClose) { }

        //------------------------------------------------------------
        public override CTerm_Base Clone() { return new CTerm_BracketClose(_Name); }

        //------------------------------------------------------------
        // this is the end ...
        public override CTerm_Base Replace(CEnvironment Env)
        {
            return this;
        }

        //------------------------------------------------------------
        // This is called when we miss a opening bracket
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            throw new CTermException("missing opening bracket!");
        }

        //------------------------------------------------------------
        // is allways const *g*
        public override bool IsConstant
        {
            get { return true; }
        }

        //------------------------------------------------------------
        // translate the Input string into the Term tree
        protected override CTerm_Base Compile(ref string Term, Stack<CTerm_Base> Stack, CEnvironment Env)
        {
            return null;
        }
    }


//********************************************************************
} // end of NameSpace MMC.Calc.Functions

//********************************************************************
// END OF FILE CTerm_Bracket
//********************************************************************
