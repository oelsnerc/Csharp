//********************************************************************
// frmMain - The Main Form
// (c) Okt 2010 MMC
//********************************************************************
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

//********************************************************************
namespace TimeProtocol
{
    public partial class frmMain : Form
    {
        //************************************************************
        protected const string Registry_Key = "HKEY_CURRENT_USER\\Software\\MMC\\TimeProtocol";
        protected const string Registry_FileName = "FileName";

        protected Logger m_Log;
        protected bool   m_Log_enabled;
        protected string m_FileName;

        protected int _MouseDownX;
        protected int _MouseDownY;

        //------------------------------------------------------------
        public frmMain()
        {
            InitializeComponent();

            m_Log = null;
            m_FileName = null;
            m_Log_enabled = false;

            ssdSeconds.Value = 0;
            ssdMinutes.Value = 0;
            ssdHours.Value = 0;
        }

        //------------------------------------------------------------
        public bool canRun()
        {
            m_FileName = (string)Registry.GetValue(Registry_Key, Registry_FileName, null);

            if (m_FileName == null)
            {   // now let the user define the name
                if (dlgFileSave.ShowDialog() == DialogResult.OK)
                {
                    m_FileName = dlgFileSave.FileName;
                }
            }

            return (m_FileName != null);
        }

        //------------------------------------------------------------
        private void frmMain_Load(object sender, EventArgs e)
        {
            setLogFile(m_FileName);

            m_Log.log("Start");
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            tmrClock.Enabled = true;

            setLogEnabled(true);
        }

        //------------------------------------------------------------
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Log.log("Close");
            m_Log.Close();
            if (m_FileName != null)
            {
                Registry.SetValue(Registry_Key, Registry_FileName, m_FileName);
            }
        }

        //************************************************************
        private void setLogFile(string FileName)
        {
            if (m_Log != null)
            {
                m_Log.log("continued in " + FileName);
                m_Log.Close();
            }
            m_FileName = FileName;

            m_Log = new Logger(m_FileName);
            m_Log.log("Open");
        }

        //------------------------------------------------------------
        public void setLogEnabled(bool Enabled)
        {
            if (Enabled)
            {
                btnRun.ImageIndex = 1;
                SystemTray.Text = "Time protocol: Started!";
                m_Log.log("log enabled");
                m_Log_enabled = true;
            }
            else
            {
                btnRun.ImageIndex = 0;
                SystemTray.Text = "Time protocol: Stopped!";
                m_Log.log("log stopped");
                m_Log_enabled = false;
            }
        }

        //************************************************************
        public void SystemEvents_PowerModeChanged(Object sender, PowerModeChangedEventArgs e)
        {
            if (m_Log_enabled)
            {
                switch (e.Mode)
                {
                    case PowerModes.Resume:
                        m_Log.log("Resume");
                        break;
                    case PowerModes.StatusChange:
                        // m_Log.log("PowerChange");  // ignore this one due to flooding
                        break;
                    case PowerModes.Suspend:
                        m_Log.log("Suspend");
                        break;
                    default:
                        m_Log.log("Unknown PowerModeChange event");
                        break;
                }
            }
        }

        //------------------------------------------------------------
        public void SystemEvents_SessionSwitch(Object sender, SessionSwitchEventArgs e)
        {
            if (m_Log_enabled)
            {
                switch (e.Reason)
                {
                    case SessionSwitchReason.ConsoleConnect:
                        m_Log.log("ConsoleConnect");
                        break;
                    case SessionSwitchReason.ConsoleDisconnect:
                        m_Log.log("Console Disconnect");
                        break;
                    case SessionSwitchReason.RemoteConnect:
                        m_Log.log("RemoteConnect");
                        break;
                    case SessionSwitchReason.RemoteDisconnect:
                        m_Log.log("RemoteDisconnect");
                        break;
                    case SessionSwitchReason.SessionLock:
                        m_Log.log("SessionLock");
                        break;
                    case SessionSwitchReason.SessionLogoff:
                        m_Log.log("SessionLogoff");
                        break;
                    case SessionSwitchReason.SessionLogon:
                        m_Log.log("SessionLogon");
                        break;
                    case SessionSwitchReason.SessionRemoteControl:
                        m_Log.log("SessionRemoteControl");
                        break;
                    case SessionSwitchReason.SessionUnlock:
                        m_Log.log("SessionUnlock");
                        break;
                    default:
                        m_Log.log("Unknown Session Switch event");
                        break;
                }
            }
        }

        //------------------------------------------------------------
        private void btnRun_Click(object sender, EventArgs e)
        {
            setLogEnabled(btnRun.ImageIndex == 0);
        }

        //------------------------------------------------------------
        private void tmrClock_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            ssdHours.Value = now.Hour;
            ssdMinutes.Value = now.Minute;
            ssdSeconds.Value = now.Second;
        }

        //------------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //------------------------------------------------------------
        private void frmMain_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Hide();
        }

        //------------------------------------------------------------
        private void SystemTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        //************************************************************
        // Menu items
        //------------------------------------------------------------
        private void mnuNewFile_Click(object sender, EventArgs e)
        {
            dlgFileSave.InitialDirectory = Path.GetDirectoryName(m_FileName);
            dlgFileSave.FileName = Path.GetFileName(m_FileName);
            if (dlgFileSave.ShowDialog() == DialogResult.OK)
            {
                setLogFile(dlgFileSave.FileName);
            }
        }

        //------------------------------------------------------------
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(m_FileName);
        }

        //************************************************************
        // now the Mouse movement
        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            _MouseDownX = e.X;
            _MouseDownY = e.Y;
        }

        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // actually we want to use this.Location.Offset(e.X - _MouseDownX, e.Y - _MouseDownY);
                // but this will not call the "Setter"
                // so we're working on the reference and call the "Setter" explicitely
                Point Cur = this.Location;
                Cur.Offset(e.X - _MouseDownX, e.Y - _MouseDownY);
                this.Location = Cur;
            }
        }
    }
}

//********************************************************************
// END OF FILE frmMain
//********************************************************************
