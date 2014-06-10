﻿using System;
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

        protected string m_FileName = null;
        protected StreamWriter m_FileStream = null;

        private bool m_isEnabled;
        public bool isEnabled
        {
            get { return m_isEnabled; }
            set
            {
                if (m_isEnabled != value)
                {
                    if (value)
                    {
                        m_isEnabled = true;
                        log("logging enabled");
                    }
                    else
                    {
                        log("logging disabled");
                        m_isEnabled = false;
                    }
                }
            }
        } 

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
            isEnabled = true;
            setFileName(FileName);
        }

        public void Close()
        {
            if (m_FileStream != null)
            {
                log("Close");
                m_FileStream.Close();
            }

            m_FileStream = null;
            m_FileName = null;
        }

        public void log(string Message)
        {
            if (isEnabled)
                write(GetTimeStamp(DateTime.Now) + '\t' + Message + Environment.NewLine);
        }

        public void setFileName(string FileName)
        {
            if (m_FileStream != null)
            {
                log("continued in " + FileName);
                Close();
            }

            m_FileName = FileName;
            m_FileStream = File.AppendText(m_FileName);
            
            log("Open");
        }
    }
}