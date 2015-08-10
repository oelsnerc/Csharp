//********************************************************************
// CTerm_Assignment - <type description here>
// (c) Dez 2010 MMC
//********************************************************************
using System.Collections.Generic;

//********************************************************************
namespace MMC.Calc.Functions
{
    //****************************************************************
    class CTerm_Assignment : CTerm_Base
    {
        protected CTerm_Base _Term;
        protected CTerm_Variable _Variable;

        //------------------------------------------------------------
        public CTerm_Assignment(string Name)
            : base(Name, TPriority.function, TTermType.Assignment)
        {
            _Term = null;
            _Variable = null;
        }

        //------------------------------------------------------------
        public override CTerm_Base Clone()
        {
            CTerm_Assignment other = new CTerm_Assignment(_Name);
            other._Term = (_Term == null) ? null : _Term.Clone();
            other._Variable = _Variable;
            return other;
        }

        //------------------------------------------------------------
        public override bool IsConstant  { get { return false; }  }
        public override CTerm_Base Replace(CEnvironment Env) { return this; }

        //------------------------------------------------------------
        protected override CTerm_Base Compile(ref string Term, Stack<CTerm_Base> Stack, CEnvironment Env)
        {
            _Term = null;
            if ((Stack.Count < 1) || (Stack.Peek().Type != TTermType.Variable))
                throw new CTermException(_Name + " needs a left-side operand!");

            _Variable = (CTerm_Variable)Stack.Pop();

            int count = Stack.Count;
            CTerm_Base Pending = GetNextTerm(ref Term, Stack, Env, TPriority.end);
            if (Stack.Count <= count)
                throw new CTermException(_Name + " needs a right-side operand!");

            _Term = Stack.Pop();

            return Pending;
        }

        //------------------------------------------------------------
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            if (_Term == null || _Variable == null)
                throw new CTermException(_Name + " not initialized!");

            _Variable.Set(_Term.Calc(Env)); // intialize the variable
            Env.SetVariable(_Variable);     // define or update it in the environment
            return _Variable.Calc(Env);     // return the value
        }
    }
}

//********************************************************************
// END OF FILE CTerm_Assignment
//********************************************************************
