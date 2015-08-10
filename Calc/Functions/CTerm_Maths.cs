//********************************************************************
// CTerm_Maths - implement the higher functions like sqr,sqrt,sin, etc.
// (c) Dez 2010 MMC
//********************************************************************

namespace MMC.Calc.Functions
{
    //****************************************************************
    // functions with 1 parameter
    class CTerm_NOT : CTerm_Functions
    {
        public CTerm_NOT(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).not();
        }
    }

    class CTerm_Square : CTerm_Functions
    {
        public CTerm_Square(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).sqr();
        }
    }

    class CTerm_SquareRoot : CTerm_Functions
    {
        public CTerm_SquareRoot(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).sqrt();
        }
    }

    class CTerm_Sinus : CTerm_Functions
    {
        public CTerm_Sinus(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).sin();
        }
    }

    class CTerm_SinusHyperbolicus : CTerm_Functions
    {
        public CTerm_SinusHyperbolicus(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).sinh();
        }
    }

    class CTerm_Cosinus : CTerm_Functions
    {
        public CTerm_Cosinus(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).cos();
        }
    }

    class CTerm_CosinusHyperbolicus : CTerm_Functions
    {
        public CTerm_CosinusHyperbolicus(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).cosh();
        }
    }

    class CTerm_Tangens : CTerm_Functions
    {
        public CTerm_Tangens(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).tan();
        }
    }

    class CTerm_TangensHyperbolicus : CTerm_Functions
    {
        public CTerm_TangensHyperbolicus(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).tanh();
        }
    }

    class CTerm_Radiant : CTerm_Functions
    {
        public CTerm_Radiant(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).rad();
        }
    }

    class CTerm_Degree : CTerm_Functions
    {
        public CTerm_Degree(string Name) : base(Name, 1) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            return _Terms[0].Calc(Env).deg();
        }
    }

    //****************************************************************
    // functions with 2 parameters
    class CTerm_Diagonale : CTerm_Functions
    {
        public CTerm_Diagonale(string Name) : base(Name, 2) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            MMC.Numbers.CNumber A = _Terms[0].Calc(Env).sqr();
            MMC.Numbers.CNumber B = _Terms[1].Calc(Env).sqr();
            A.add(B);
            return A.sqrt();
        }
    }

    //****************************************************************
    // function with more than 2 parameters
    class CTerm_Date : CTerm_Functions
    {
        public CTerm_Date(string Name) : base(Name, 3) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            MMC.Numbers.CNumber res = Env.NewNumber(0.0);
            res.Date(_Terms[0].Calc(Env), _Terms[1].Calc(Env), _Terms[2].Calc(Env));
            return res;
        }
    }
    class CTerm_Day : CTerm_Functions
    {
        public CTerm_Day(string Name) : base(Name, 3) { }
        public override MMC.Numbers.CNumber Calc(CEnvironment Env)
        {
            Check();
            MMC.Numbers.CNumber res = Env.NewNumber(0.0);
            res.Day(_Terms[0].Calc(Env), _Terms[1].Calc(Env), _Terms[2].Calc(Env));
            return res;
        }
    }
}

//********************************************************************
// END OF FILE CTerm_Maths
//********************************************************************
