//********************************************************************
// frmMain - Main Window
// (c) Apr 2011 MMC
//********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

//********************************************************************
namespace BladeConfig
{
    public partial class frmMain : Form
    {
        private XmlDocument _Doc;
        public frmMain()
        {
            InitializeComponent();
            _Doc = null;
        }

        //------------------------------------------------------------
        // adds to the end of the Tree the current Node
        private TreeNode NODE_Add(TreeNodeCollection Tree, XmlNode Node, string Name)
        {
            TreeNode TNode = Tree.Add(Name, Name);
            TNode.Tag = Node;
            if (Node.Attributes != null)
                foreach (XmlAttribute Attr in Node.Attributes)
                {
                    string Text = Attr.Name;
                    TNode.Nodes.Add(Text,Text);
                }
            return TNode;
        }

        //------------------------------------------------------------
        // add the Subtree of Node
        private void XML_Add(TreeNodeCollection Tree, XmlNode Node)
        {
            while (Node != null)
            {
                if (Node.NodeType == XmlNodeType.Element)
                {
                    TreeNode TNode = NODE_Add(Tree, Node, Node.Name);
                    if (Node.HasChildNodes)
                    {
                        XML_Add(TNode.Nodes, Node.FirstChild);
                    }
                }
                Node = Node.NextSibling;
            }
        }

        //------------------------------------------------------------
        // adds only the Nodes with Name = "CLASS" to the Tree
        private void CLASS_Add(XmlNode Node)
        {
            if (Node != null)
            {
                XmlAttributeCollection Attributes = Node.Attributes;
                if (Attributes != null)
                {
                    XmlAttribute Attr_Name = Attributes["NAME"];
                    XmlAttribute Attr_Parent = Attributes["PARENT"];
                    TreeNodeCollection Tree = treClasses.Nodes;
                    if (Attr_Parent != null)
                    {
                        TreeNode[] Parents = Tree.Find(Attr_Parent.Value, true);
                        if (Parents.Length > 0) Tree = Parents[0].Nodes;
                    }
                    NODE_Add(Tree, Node, Attr_Name.Value);
                }
            }
        }

        //------------------------------------------------------------
        // synch the XML Document with the View
        private void XML_Update()
        {
            treClasses.BeginUpdate();
            treClasses.Nodes.Clear();
            if (_Doc != null)
            {
                XmlNodeList ClassNodes = _Doc.GetElementsByTagName("CLASS");
                foreach (XmlNode Node in ClassNodes)
                {
                    CLASS_Add(Node);
                }
            }
            treClasses.EndUpdate();
        }

        //************************************************************
        public void XML_Read(string FileName)
        {
            _Doc = new XmlDocument();
            _Doc.Load(FileName);
            XML_Update();
        }

        //************************************************************
        private void mnuOpen_Clicked(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                XML_Read(dlgOpen.FileName);
            }
        }

        //------------------------------------------------------------
        private void treClasses_Select(object sender, TreeViewEventArgs e)
        {
            TreeNode TNode = ((TreeView)sender).SelectedNode;
            if (TNode != null)
            {
                XmlNode Node = (XmlNode)TNode.Tag;
                if (Node == null)
                {
                    Node = (XmlNode)TNode.Parent.Tag;
                    XmlAttributeCollection Attributes = Node.Attributes;
                    if (Attributes != null)
                    {
                        XmlAttribute Attr = Attributes[TNode.Text];
                        txtValue.Text = Text + " = " + Attr.Value;
                    }
                }

                if (sender != treClass)
                {
                    treClass.BeginUpdate();
                    treClass.Nodes.Clear();

                    TNode = NODE_Add(treClass.Nodes, Node, Node.Name);
                    XML_Add(TNode.Nodes, Node.FirstChild);

                    treClass.ExpandAll();
                    treClass.EndUpdate();
                }
            }
        }
    }
}

//********************************************************************
// END OF FILE frmMain
//********************************************************************
