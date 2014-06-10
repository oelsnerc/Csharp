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

        protected Logger m_Log;
        protected PersistentData m_Data;

        protected int _MouseDownX;
        protected int _MouseDownY;

        //------------------------------------------------------------
        public frmMain()
        {
            InitializeComponent();

            m_Data = new PersistentData(Registry_Key);

            m_Log = null;

            ssdSeconds.Value = 0;
            ssdMinutes.Value = 0;
            ssdHours.Value = 0;
        }

        //------------------------------------------------------------
        public bool canRun()
        {
            if (m_Data.isInitialized)
                return true;

            if (dlgFileSave.ShowDialog() != DialogResult.OK)
                return false;

            m_Data.FileName = dlgFileSave.FileName;
            m_Data.Position = this.Location;
            return true;
        }

        //------------------------------------------------------------
        private void frmMain_Load(object sender, EventArgs e)
        {
            // read the position from the registry
            this.Location = m_Data.Position;
            m_Log = new Logger(m_Data.FileName);

            m_Log.log("Start");
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            tmrClock.Enabled = true;

            setLogEnabled(true);
        }

        //------------------------------------------------------------
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;

            m_Log.Close();
            m_Data.Position = this.Location;
            m_Data.WriteData(Registry_Key);
        }

        //************************************************************
        public void setLogEnabled(bool Enabled)
        {
            m_Log.isEnabled = Enabled;
            if (Enabled)
            {
                btnRun.ImageIndex = 1;
                SystemTray.Text = "Time protocol: Started!";
            }
            else
            {
                btnRun.ImageIndex = 0;
                SystemTray.Text = "Time protocol: Stopped!";
            }
        }


        //************************************************************
        public void SystemEvents_PowerModeChanged(Object sender, PowerModeChangedEventArgs e)
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

        //------------------------------------------------------------
        public void SystemEvents_SessionSwitch(Object sender, SessionSwitchEventArgs e)
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
            dlgFileSave.InitialDirectory = Path.GetDirectoryName(m_Data.FileName);
            dlgFileSave.FileName = Path.GetFileName(m_Data.FileName);
            if (dlgFileSave.ShowDialog() == DialogResult.OK)
            {
                m_Log.setFileName(dlgFileSave.FileName);
            }
        }

        //------------------------------------------------------------
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(m_Data.FileName);
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
