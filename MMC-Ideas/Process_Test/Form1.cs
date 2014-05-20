using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Process_Test
{
    public partial class Form1 : Form
    {
        private delegate void ProcessEndDelegate();
        
        private Process _Process;
        private Thread _Thread_Std;
        private Thread _Thread_Err;
        // private AutoResetEvent _Event;

        private StreamWriter _Stream_Output;
        private ProcessEndDelegate _ProcessEnd;
        private String _Output;

        public Form1()
        {
            InitializeComponent();
            _ProcessEnd = new ProcessEndDelegate(this.Process_Ended);
            _Process = null;
            _Thread_Std = null;
            _Thread_Err = null;
            _Stream_Output = null;
            btnEnter.Enabled = false;

            // _Event = new AutoResetEvent(false);
            _Output = "";
        }

        private void ProcessOutputHandler(object Stream)
        {   // this is the OutPut thread
            StreamReader _Stream = (StreamReader)Stream;
            while (true)
            {
                int c = _Stream.Read();
                if (c < 0) break;
                if (c>=32)
                {
                    _Output += (char) c;
                }
                else
                {
                    _Output += (char)c;
                }
            }
            _Output += "ENDE!";
        }

        void Process_Exited(object sender, EventArgs e)
        {   // this will run in a thread other than the original one
            Invoke(_ProcessEnd);
        }

        private void Process_Start(String Command)
        {
            _Process = new Process();
            _Process.StartInfo.FileName = Command;
            _Process.StartInfo.UseShellExecute = false;
            _Process.StartInfo.RedirectStandardInput = true;
            _Process.StartInfo.RedirectStandardOutput = true;
            _Process.StartInfo.RedirectStandardError = true;
            _Process.StartInfo.CreateNoWindow = true;
            _Process.EnableRaisingEvents = true;

            _Process.Exited += new EventHandler(Process_Exited);

            _Thread_Std = new Thread(this.ProcessOutputHandler);
            _Thread_Err = new Thread(this.ProcessOutputHandler);

            _Process.Start();
            _Thread_Std.Start(_Process.StandardOutput);
            _Thread_Err.Start(_Process.StandardError);
            _Stream_Output = _Process.StandardInput;
            //_Event.Set();

            tmrOutput.Enabled = true;
            btnProcess.Text = "Stop";
            btnEnter.Enabled = true;
        }

        private void Process_Ended()
        {
            _Process.WaitForExit();
            _Thread_Std.Abort();
            _Thread_Err.Abort();
            _Process.Close();

            _Process = null;
            _Thread_Std = null;
            _Thread_Err = null;
            _Stream_Output = null;

            tmrOutput.Enabled = false;
            btnProcess.Text = "Start";
            btnEnter.Enabled = false;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (btnProcess.Text =="Start")
            {
                //Process_Start("cmd.exe");
                Process_Start("C:\\Programms\\cygwin\\Cygwin.bat");
            }
            else
            {
                _Stream_Output.Close(); 
                // Process_End();
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (_Stream_Output != null)
            {
                _Stream_Output.WriteLine(txtInput.Text);
                txtInput.Clear();
            }
        }

        private void txtOutput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_Stream_Output != null)
            {
                _Stream_Output.Write("Hello World!");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_Thread_Std.Abort();
            if (_Stream_Output != null) _Stream_Output.Close();
        }

        private void tmrOutput_Tick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_Output))
            {
                txtOutput.AppendText(_Output);
                _Output = "";
                txtOutput.ScrollToCaret();
            }
        }
    }
}
