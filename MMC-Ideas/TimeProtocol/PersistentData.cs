using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TimeProtocol
{
    public class PersistentData
    {
        protected const string Registry_FileName = "FileName";
        protected const string Registry_PositionX = "PositionX";
        protected const string Registry_PositionY = "PositionY";

        public bool isInitialized { get; private set; }

        private string m_FileName;
        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; isInitialized = String.IsNullOrEmpty(m_FileName); }
        }

        private Point m_Position;
        public Point Position
        {
            get { return m_Position; }
            set { m_Position = value; isInitialized = true; }
        }

        public PersistentData(string RegistryMainKey)
        {
            ReadData(RegistryMainKey);
        }

        public void ReadData(string RegistryMainKey)
        {
            isInitialized = false;

            m_FileName = (string)Registry.GetValue(RegistryMainKey, Registry_FileName, "");
            if (String.IsNullOrEmpty(m_FileName)) return;

            int x = (int)Registry.GetValue(RegistryMainKey, Registry_PositionX, int.MinValue);
            if (x == int.MinValue) return;

            int y = (int)Registry.GetValue(RegistryMainKey, Registry_PositionY, int.MinValue);
            if (y == int.MinValue) return;

            m_Position = new Point(x, y);

            isInitialized = true;
        }

        public void WriteData(string RegistryMainKey)
        {
            if (isInitialized)
            {
                Registry.SetValue(RegistryMainKey, Registry_FileName, FileName);
                Registry.SetValue(RegistryMainKey, Registry_PositionX, Position.X);
                Registry.SetValue(RegistryMainKey, Registry_PositionY, Position.Y);
            }
        }
    }
}
