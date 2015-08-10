//********************************************************************
// CTerm_Add - Implements the Add term
// (c) Jan 2010 MMC
//********************************************************************

namespace MMC.Calc.Functions
{
//********************************************************************
    class CTerm_Add : CTerm_2Ops
    {
        //------------------------------------------------------------
        // Constructor for both Terms
        public CTerm_Add(string Name) : base(Name,TPriority.group,false,true) {}

        //------------------------------------------------------------
        // simple add the both terms
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            if (!_bTermB) throw new CTermException("Add not initialized!");
            if (!_bTermA) return _TermB.Calc(Env);
            return _TermA.Calc(Env).add(_TermB.Calc(Env));
        }
    }

    //****************************************************************
    class CTerm_Sub : CTerm_2Ops
    {
        //------------------------------------------------------------
        // Constructor for both Terms
        public CTerm_Sub(string Name) : base(Name,TPriority.group,false,true) {}

        //------------------------------------------------------------
        // simple substract the both terms
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            if (!_bTermB) throw new CTermException("Sub not initialized!");
            if (!_bTermA) return (_TermB.Calc(Env).neg());
            return (_TermA.Calc(Env).sub(_TermB.Calc(Env)));
        }
    }

    //****************************************************************
    class CTerm_Mul : CTerm_2Ops
    {
        //------------------------------------------------------------
        // Constructor for both Terms
        public CTerm_Mul(string Name) : base(Name,TPriority.ring,true,true) {}
        public CTerm_Mul(string Name, CTerm_Base TermA, CTerm_Base TermB)
            : base(Name,TPriority.ring,true,true)
        {
            _TermA = TermA;
            _TermB = TermB;
        }

        //------------------------------------------------------------
        // simple multiply the both terms
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            if (!(_bTermA && _bTermB)) throw new CTermException("Mul not initialized!");
            return (_TermA.Calc(Env).mul(_TermB.Calc(Env)));
        }
    }

    //****************************************************************
    class CTerm_Div : CTerm_2Ops
    {
        //------------------------------------------------------------
        // Constructor for both Terms
        public CTerm_Div(string Name) : base(Name,TPriority.ring, true, true) { }

        //------------------------------------------------------------
        // simple divide the both terms
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            if (!(_bTermA && _bTermB)) throw new CTermException("Div not initialized!");
            MMC.Numbers.CNumber B = _TermB.Calc(Env);
            if (B.IsZero) throw new CTermException("Division by zero!");
            return (_TermA.Calc(Env).div(B));
        }
    }

    //****************************************************************
    class CTerm_Mod : CTerm_2Ops
    {
        //------------------------------------------------------------
        // Constructor for both Terms
        public CTerm_Mod(string Name) : base(Name, TPriority.ring, true, true) { }

        //------------------------------------------------------------
        // simple divide the both terms
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            if (!(_bTermA && _bTermB)) throw new CTermException("Mod not initialized!");
            MMC.Numbers.CNumber B = _TermB.Calc(Env);
            if (B.IsZero) throw new CTermException("Division by zero!");
            return (_TermA.Calc(Env).rem(B));
        }
    }

    //****************************************************************
    class CTerm_Power : CTerm_2Ops
    {
        //------------------------------------------------------------
        // Constructor for both Terms
        public CTerm_Power(string Name) : base(Name,TPriority.power, true, true) { }

        //------------------------------------------------------------
        // simple divide the both terms
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            if (!(_bTermA && _bTermB)) throw new CTermException("Power not initialized!");
            return (_TermA.Calc(Env).pow(_TermB.Calc(Env)));
        }
    }

    //****************************************************************
    // logical functions
    //****************************************************************
    class CTerm_AND : CTerm_2Ops
    {
        //------------------------------------------------------------
        // Constructor for both Terms
        public CTerm_AND(string Name) : base(Name, TPriority.group, true, true) { }

        //------------------------------------------------------------
        // simple divide the both terms
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            if (!(_bTermA && _bTermB)) throw new CTermException("AND not initialized!");
            return (_TermA.Calc(Env).and(_TermB.Calc(Env)));
        }
    }

    //----------------------------------------------------------------
    class CTerm_OR : CTerm_2Ops
    {
        //------------------------------------------------------------
        // Constructor for both Terms
        public CTerm_OR(string Name) : base(Name, TPriority.group, true, true) { }

        //------------------------------------------------------------
        // simple divide the both terms
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            if (!(_bTermA && _bTermB)) throw new CTermException("AND not initialized!");
            return (_TermA.Calc(Env).or(_TermB.Calc(Env)));
        }
    }

    //----------------------------------------------------------------
    class CTerm_XOR : CTerm_2Ops
    {
        //------------------------------------------------------------
        // Constructor for both Terms
        public CTerm_XOR(string Name) : base(Name, TPriority.group, true, true) { }

        //------------------------------------------------------------
        // simple divide the both terms
        public override MMC.Numbers.CNumber Calc(MMC.Calc.CEnvironment Env)
        {
            if (!(_bTermA && _bTermB)) throw new CTermException("AND not initialized!");
            return (_TermA.Calc(Env).xor(_TermB.Calc(Env)));
        }
    }

//********************************************************************
} // end of NameSpace MMC.Calc.Functions

//********************************************************************
// END OF FILE CTerm_Add
//********************************************************************
