//********************************************************************
// CTerm_Const - Implement the representation of a numerical value
//               as a mathematical term
// (c) Jan 2010 MMC
//********************************************************************
using System.Collections.Generic;

//********************************************************************
namespace MMC.Calc.Functions
{
//********************************************************************
    class CTerm_Const : MMC.Calc.CTerm_Base
    {
        //------------------------------------------------------------
        protected MMC.Numbers.CNumber _Value;

        //------------------------------------------------------------
        // Constructor for Constants
        public CTerm_Const(string Name, MMC.Numbers.CNumber Value)
            : base(Name,TPriority.constant,TTermType.Constant)
        {
            _Value = Value;
        }

        //------------------------------------------------------------
        public override CTerm_Base Clone()
        {
            return new CTerm_Const(_Name, _Value.Clone());
        }

        //------------------------------------------------------------
        // this is const ... no replacement possible
        public override CTerm_Base Replace(CEnvironment Env)
        {
            return this;
        }

        //------------------------------------------------------------
        // returns the Value of the Constant
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            return _Value;
        }

        //------------------------------------------------------------
        // a constant is constant
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

        //------------------------------------------------------------
        // to be able to change the value after the creation
        public MMC.Numbers.CNumber Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }

    //****************************************************************
    class CTerm_Number : MMC.Calc.Functions.CTerm_Const
    {
        //------------------------------------------------------------
        // Constructor for Constants
        public CTerm_Number(MMC.Numbers.CNumber Value)
            : base(null, Value)
        { 
            _TermType = TTermType.Number;
        }

        //------------------------------------------------------------
        public override CTerm_Base Clone()
        {
            return new CTerm_Number(_Value.Clone());
        }
    }

    //****************************************************************
    class CTerm_Variable : CTerm_Base
    {
        //------------------------------------------------------------
        protected MMC.Numbers.CNumber _Value;

        //------------------------------------------------------------
        public CTerm_Variable(string Name)
            : base(Name, TPriority.constant, TTermType.Variable)
        {
            _Value = null;
        }

        //------------------------------------------------------------
        public override CTerm_Base Clone()
        {
            CTerm_Variable other = new CTerm_Variable(_Name);
            other._Value = (_Value == null) ? null : _Value.Clone();
            return other;
        }

        //------------------------------------------------------------
        public override bool IsConstant { get { return false; } }
        public override CTerm_Base Replace(CEnvironment Env) { return this; }
        protected override CTerm_Base Compile(ref string Term, Stack<CTerm_Base> Stack, CEnvironment Env) { return null; }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            if (_Value == null) throw new CTermException(_Name + " not intialized!");
            return _Value;
        }

        //------------------------------------------------------------
        public void Set(MMC.Numbers.CNumber Value)
        {
            _Value = Value.Clone();
        }
    }
    
//********************************************************************
} // end of NameSpace MMC.Calc.Functions

//********************************************************************
// END OF FILE CTerm_Const
//********************************************************************
