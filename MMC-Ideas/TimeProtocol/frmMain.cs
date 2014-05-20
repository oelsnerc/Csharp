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
        static string GetTimeStamp(DateTime dt)
        {
            //DateTime dt = DateTime.Now;
            string ts = dt.ToString();
            ts += ':';
            ts += dt.Millisecond.ToString("D3");
            return ts;
        }

        //************************************************************
        const int Height_Show = 473;
        const int Height_Hide = 114;

        protected string _FileName;
        protected StreamWriter _FileStream;
        protected bool _Log_enabled;
        protected DateTime _Log_Last;
        protected DateTime _Log_First;
        protected int _MouseDownX;
        protected int _MouseDownY;

        public frmMain()
        {
            InitializeComponent();
            _FileName = null;
            _FileStream = null;
            _Log_enabled = false;
            _Log_First = DateTime.MinValue;
            _Log_Last = DateTime.MinValue;

            ssdSeconds.Value = 0;
            ssdMinutes.Value = 0;
            ssdHours.Value = 0;

            ssdLog_Seconds.Value = 0;
            ssdLog_Minutes.Value = 0;
            ssdLog_Hours.Value = 0;
            
            LogHide();

            tmrClock.Enabled = true;
        }

        //------------------------------------------------------------
        private void frmMain_Load(object sender, EventArgs e)
        {
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        //************************************************************
        protected void File_Open(string FileName)
        {
            _FileName = FileName;
            _FileStream = File.AppendText(_FileName);
        }

        //------------------------------------------------------------
        protected void File_Close()
        {
            if (_FileStream != null) _FileStream.Close();
            _FileStream = null;
            _FileName = null;
        }
        
        //************************************************************
        public void WriteText(string Text)
        {
            txtProtocol.AppendText(Text);
            if (_FileStream != null)
            {
                _FileStream.Write(Text);
                _FileStream.Flush();
            }
        }

        //------------------------------------------------------------
        public void Log(string Message)
        {
            DateTime log = DateTime.Now;
            if (_Log_Last.Date < log.Date)
            {
                WriteText("*********************************************\n");
                TimeSpan span = _Log_Last - _Log_First;
                if (span.Seconds > 0)
                {
                    WriteText("TimeSpan: " + span.ToString() + '\n');
                    WriteText("*********************************************\n");
                }
                _Log_First = log;
            }
            WriteText(GetTimeStamp(log) + '\t' + Message + '\n');
            _Log_Last = log;
        }

        public void LogHide()
        {
            this.Height = Height_Hide;
            btnHide.Text = "ShowLog";
        }

        public void LogShow()
        {
            this.Height = Height_Show;
            btnHide.Text = "HideLog";
        }

        //************************************************************
        public void SystemEvents_PowerModeChanged(Object sender, PowerModeChangedEventArgs e)
        {
            if (_Log_enabled)
            {
                switch (e.Mode)
                {
                    case PowerModes.Resume:
                        Log("Resume");
                        break;
                    case PowerModes.StatusChange:
                        // Log("PowerChange");  // ignore this one due to flooding
                        break;
                    case PowerModes.Suspend:
                        Log("Suspend");
                        break;
                    default:
                        Log("Unknown PowerModeChange event");
                        break;
                }
            }
        }

        //------------------------------------------------------------
        public void SystemEvents_SessionSwitch(Object sender, SessionSwitchEventArgs e)
        {
            if (_Log_enabled)
            {
                switch (e.Reason)
                {
                    case SessionSwitchReason.ConsoleConnect:
                        Log("ConsoleConnect");
                        break;
                    case SessionSwitchReason.ConsoleDisconnect:
                        Log("Console Disconnect");
                        break;
                    case SessionSwitchReason.RemoteConnect:
                        Log("RemoteConnect");
                        break;
                    case SessionSwitchReason.RemoteDisconnect:
                        Log("RemoteDisconnect");
                        break;
                    case SessionSwitchReason.SessionLock:
                        Log("SessionLock");
                        break;
                    case SessionSwitchReason.SessionLogoff:
                        Log("SessionLogoff");
                        break;
                    case SessionSwitchReason.SessionLogon:
                        Log("SessionLogon");
                        break;
                    case SessionSwitchReason.SessionRemoteControl:
                        Log("SessionRemoteControl");
                        break;
                    case SessionSwitchReason.SessionUnlock:
                        Log("SessionUnlock");
                        break;
                    default:
                        Log("Unknown Session Switch event");
                        break;
                }
            }
        }

        //------------------------------------------------------------
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.ImageIndex == 0)
            {
                if (dlgFileSave.ShowDialog() == DialogResult.OK)
                {
                    File_Open(dlgFileSave.FileName);
                    btnRun.ImageIndex = 1;
                    Log("Started");
                    _Log_enabled = true;
                }
            }
            else
            {
                btnRun.ImageIndex = 0;
                SystemTray.Text = "Time protocol: Stopped!";
                Log("Stopped");
                File_Close();
                _Log_enabled = false;
            }
        }

        //------------------------------------------------------------
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            File_Close();
        }

        //------------------------------------------------------------
        private void tmrClock_Tick(object sender, EventArgs e)
        {
            DateTime log = DateTime.Now;

            ssdHours.Value = log.Hour;
            ssdMinutes.Value = log.Minute;
            ssdSeconds.Value = log.Second;

            if (_Log_enabled)
            {
                TimeSpan Span = log - _Log_First;
                ssdLog_Hours.Value = Span.Hours;
                ssdLog_Minutes.Value = Span.Minutes;
                ssdLog_Seconds.Value = Span.Seconds;

                SystemTray.Text = String.Format("Time Protocol: {0:D2}:{1:D2}",Span.Hours,Span.Minutes);
            };
        }

        //------------------------------------------------------------
        private void btnHide_Click(object sender, EventArgs e)
        {
            if (this.Height == Height_Hide)
            {
                LogShow();
            }
            else
            {
                LogHide();
            }
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
