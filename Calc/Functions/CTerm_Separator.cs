//********************************************************************
// CTerm_Separator - <type description here>
// (c) Dez 2010 MMC
//********************************************************************

namespace MMC.Calc.Functions
{
    //****************************************************************
    class CTerm_Separator : CTerm_Base
    {
        protected CTerm_Base _Term;

        public CTerm_Separator(string Name)
            : base(Name, TPriority.separator, TTermType.Seperator)
        {
            _Term = null;
        }

        public override CTerm_Base Clone()
        {
            CTerm_Separator Term = new CTerm_Separator(_Name);
            Term._Term = (_Term == null) ? null : _Term.Clone();
            return Term;
        }

        public override bool IsConstant
        {
            get { return (_Term != null && _Term.IsConstant);  }
        }

        public override CTerm_Base Replace(CEnvironment Env)
        {
            return (_Term == null) ? this : _Term.Replace(Env);
        }

        protected override CTerm_Base Compile(ref string Term, System.Collections.Generic.Stack<CTerm_Base> Stack, CEnvironment Env)
        {
            _Term = null;
            int size = Stack.Count;
            CTerm_Base Pending = GetNextTerm(ref Term, Stack, Env, TPriority.separator);
            if (Stack.Count <= size) throw new CTermException("a separator has to followed by an operand!");
            _Term = Stack.Pop();
            return Pending;
        }

        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            if (_Term == null) throw new CTermException(_Name + " not initialized!");
            return _Term.Calc(Env);
        }
    }
}
//********************************************************************
// END OF FILE CTerm_Separator
//********************************************************************
