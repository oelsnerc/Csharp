using System;
using System.IO;
using System.Text;

namespace TimeProtocol
{
    public class Logger
    {
        //************************************************************
        static string GetTimeStamp(DateTime dt)
        {
            //DateTime dt = DateTime.Now;
            string ts = dt.ToString();
            ts += ':';
            ts += dt.Millisecond.ToString("D3");
            return ts;
        }

        protected string m_FileName;
        protected StreamWriter m_FileStream;

        protected void write(string Text)
        {
            if (m_FileStream != null)
            {
                m_FileStream.Write(Text);
                m_FileStream.Flush();
            }
        }

        public Logger(string FileName)
        {
            m_FileName = FileName;
            m_FileStream = File.AppendText(m_FileName);
        }

        public void Close()
        {
            if (m_FileStream != null) m_FileStream.Close();
            m_FileStream = null;
            m_FileName = null;
        }

        public void log(string Message)
        {
            write(GetTimeStamp(DateTime.Now) + '\t' + Message + Environment.NewLine);
        }
    }
}
