//********************************************************************
// CTagFile - Represents an interface to a ctags file
// (c) Okt 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//********************************************************************
namespace CFind
{
    //****************************************************************
    class CTagFile
    {
        private List<CTag> _Tags;
        private string _Prefix;

        //************************************************************
        public CTagFile(string FileName)
        {
            _Prefix = null;
            _Tags = new List<CTag>();
            LoadFile(FileName);
        }

        //************************************************************
        public void LoadFile(string FileName)
        {
            if (File.Exists(FileName))
            {
                using (StreamReader fs = File.OpenText(FileName))
                {
                    while (fs.Peek() >= 0)
                    {
                        CTag tag = new CTag(fs.ReadLine());
                        if (!tag.IsComment)
                        {
                            _Tags.Add(tag);
                            if (_Prefix == null)
                                _Prefix = tag.File;
                            else
                            {
                                if (!tag.File.StartsWith(_Prefix))
                                {
                                    string Path = tag.File;
                                    int idx = 0;
                                    int len = Path.Length;
                                    if (len < _Prefix.Length) len = _Prefix.Length;
                                    while (idx < len)
                                    {
                                        if (_Prefix[idx] != Path[idx]) break;
                                        idx++;
                                    }
                                    _Prefix = _Prefix.Substring(0, idx);
                                }
                            }
                        }
                    }
                    fs.Close();
                }
            }
        }

        //------------------------------------------------------------
        public List<CTag> Find(string Name)
        {
            List<CTag> result = new List<CTag>();
            foreach (CTag tag in _Tags)
            {
                if (tag.Contains(Name)) result.Add(tag);
            }
            return result;
        }

        //************************************************************
        public int Count { get { return _Tags.Count; } }
        public string Prefix { get { return _Prefix; } }
    }
}

//********************************************************************
// END OF FILE CTagFile
//********************************************************************
