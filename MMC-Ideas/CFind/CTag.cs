//********************************************************************
// CTag - Represents one line in a ctags file
// e.g.: <name>\t<file>\t<line>;"\t<type>
// (c) Okt 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;
using System.Text;

//********************************************************************
namespace CFind
{
    class CTag
    {
        private string _Name;
        private string _File;
        private int _Line;
        private bool _Comment;

        //************************************************************
        public CTag(string TagLine)
        {
            string[] split = TagLine.Split('\t');
            if (split.Length < 2) throw new InvalidOperationException("TagLine invalid!");
            _Name = split[0];
            _File = split[1];

            _Comment = _Name.StartsWith("!");
            if (_Comment)
            {
                _Name = _Name.Remove(0, 1);
                _Line = -1;
            }
            else
            {
                int len = split[2].Length;
                string Num = split[2].Remove(len - 2, 2);
                _Line = Int32.Parse(Num);
            }
        }

        //************************************************************
        public bool IsEqual(string Name) { return _Name.Equals(Name); }
        public bool Contains(string Name) { return _Name.Contains(Name); }

        //************************************************************
        // getter functions
        public string Name { get { return _Name; } }
        public string File { get { return _File; } }
        public int Line { get { return _Line; } }
        public bool IsComment { get { return _Comment; } }
    }
}

//********************************************************************
// END OF FILE CTag
//********************************************************************
