//********************************************************************
// CTerm_2Ops - base class for the mathemtical operations which
//              take an operand before and after it
// (c) Jan 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;
using System.Reflection;

//********************************************************************
namespace MMC.Calc.Functions
{
//********************************************************************
    abstract class CTerm_2Ops : CTerm_Base
    {
        //------------------------------------------------------------
        // some working variables
        protected CTerm_Base _TermA;    // contains the term before
        protected CTerm_Base _TermB;    // contains the term after
        protected bool _bTermA;    // for compile: is TermA needed? (should be overwritten in the derived class)
        protected bool _bTermB;    // for compile: is TermB needed? (should be overwritten in the derived class)

        //------------------------------------------------------------
        // standard constructor for 2Ops
        public CTerm_2Ops(string Name, TPriority Priority, bool TermA, bool TermB)
            : base(Name,Priority,TTermType.TwoOps)
        {
            _TermA = null; _bTermA = TermA;
            _TermB = null; _bTermB = TermB;
        }

        //------------------------------------------------------------
        // clone the operation
        public override CTerm_Base Clone()
        {
            Type myType = GetType();
            Type[] types = new Type[1];
            types[0] = typeof(string);
            ConstructorInfo myConstructor = myType.GetConstructor(types);

            Object[] args = new Object[1];
            args[0] = _Name;

            CTerm_2Ops Term = (CTerm_2Ops)myConstructor.Invoke(args);

            Term._bTermA = _bTermA;
            Term._bTermB = _bTermB;
            Term._TermA = (_TermA != null) ? _TermA.Clone() : null;
            Term._TermB = (_TermB != null) ? _TermB.Clone() : null;

            return Term;
        }

        //------------------------------------------------------------
        // try to replace by the 2 subterms
        public override CTerm_Base Replace(CEnvironment Env)
        {
            if (!IsConstant) return this;
            return new CTerm_Number(Calc(Env));
        }

        //------------------------------------------------------------
        // translate the Input string into the Term tree
        protected override CTerm_Base Compile(ref string Term, Stack<CTerm_Base> Stack, CEnvironment Env)
        {
            _TermA = null; _TermB = null;

            if (_bTermA || Stack.Count > 0)    // take Term A, if it's needed or it's there anyway
            {
                if (Stack.Count < 1) throw new CTermException(_Name + " missing 1st operand!");
                _TermA = Stack.Pop();
                _bTermA = true;
            };

            CTerm_Base Result = GetNextTerm(ref Term, Stack, Env, Priotity);

            if (_bTermB)
            {
                if (Stack.Count < 1) throw new CTermException(_Name + " missing 2nd operand!");
                _TermB = Stack.Pop();
            }

            return Result;
        }

        //------------------------------------------------------------
        // properties to access to the 2 operands
        public CTerm_Base First 
        {
            get { return _TermA; }
            set { _TermA = value; }
        }

        public CTerm_Base Second
        {
            get { return _TermB; }
            set { _TermB = value; }
        }

        //------------------------------------------------------------
        // returns if this term is not dependent on any variables
        public override bool IsConstant
        {
            get
            {
                bool rc = true;
                if (_TermA != null) rc &= _TermA.IsConstant;
                if (_TermB != null) rc &= _TermB.IsConstant;
                return rc;
            }
        }
    }
//********************************************************************
} // end of NameSpace MMC.Calc.Functions

//********************************************************************
// END OF FILE CTerm_2Ops
//********************************************************************
