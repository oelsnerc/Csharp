//********************************************************************
// MMC_Tree - <type description here>
// (c) Okt 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMC
{
    //****************************************************************
    class CNode<Type>
    {
        //------------------------------------------------------------
        private CNode<Type> _Father;
        private CNode<Type> _Brother_Left;
        private CNode<Type> _Brother_Right;
        private CNode<Type> _FirstSon;
        private Type _Value;

        public CNode(Type Value)
        {
            _Father = null;
            _Brother_Left = null;
            _Brother_Right = null;
            _FirstSon = null;
            _Value = Value;
        }

        //------------------------------------------------------------
        public Type Val
        {
            get { return _Value; }
            set { _Value = value; }
        }

        //------------------------------------------------------------
        // remove this node and its subtree from the tree
        public void Remove()
        {
            if (_Brother_Left != null) _Brother_Left._Brother_Right = _Brother_Right;
            if (_Brother_Right != null) _Brother_Right._Brother_Left = _Brother_Left;
            if (_Father != null && _Father._FirstSon == this) _Father._FirstSon = _Brother;
            
            _Father = null;
            _Brother_Left = null;
            _Brother_Right = null;
        }

        //------------------------------------------------------------
        public void SetFather(CNode<Type> Father)
        {

        }

    }

    //****************************************************************
    class CTree<Type>
    {
        //------------------------------------------------------------
        private CNode<Type> _Root;
    }
}

//********************************************************************
// END OF FILE MMC_Tree
//********************************************************************
