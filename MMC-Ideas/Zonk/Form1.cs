using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zonk
{
    public partial class frmMain : Form
    {
        //------------------------------------------------------------
        // the private datas
        private int FCount;
        private int FZonk;
        private int FAuto;
        private int FRadio;
        private Random FRand;
        private int[,] FSituation = { { 0, 1, 2 }, { 0, 2, 1 }, { 1, 0, 2 }, { 1, 2, 0 }, { 2, 0, 1 }, { 2, 1, 0 } };

        //------------------------------------------------------------
        // constructor
        public frmMain()
        {
            InitializeComponent();
            edSeed.Text = "42";

            ResetNumbers();
            UpdateNumbers();
        }

        //------------------------------------------------------------
        // set all to zero
        public void ResetNumbers()
        {
            FRand = new Random(Convert.ToInt32(edSeed.Text));
            FCount = 0;
            FZonk = 0;
            FAuto = 0;
            FRadio = 0;
        }

        //------------------------------------------------------------
        // update the lables in the form
        public void UpdateNumbers()
        {
            lblCount.Text = FCount.ToString();
            lblZonk.Text = FZonk.ToString();
            lblAuto.Text = FAuto.ToString();
            lblRadio.Text = FRadio.ToString();
        }

        //------------------------------------------------------------
        // play one round
        public void NextNumbers()
        {
            FCount++;
            int Situation = FRand.Next(FSituation.GetLength(0));
            int Tor = FRand.Next(3);
            int Value = FSituation[Situation, Tor];
            switch (Value)
            {
                case 0: 
                case 1: FZonk++; break;
                case 2: FAuto++; break;

                default:
                    break;
            }
        }

        private void ToggleTimer()
        {
            if (clkBeat.Enabled)
            {
                clkBeat.Enabled = false;
                btnBeat.Text = "Start";
            }
            else
            {
                ResetNumbers();
                btnBeat.Text = "Stop";
                clkBeat.Enabled = true;
            }
        }

        private void btnBeat_Click(object sender, EventArgs e)
        {
            ToggleTimer();
        }

        private void clkBeat_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                NextNumbers();
            }
            UpdateNumbers();

            if (FCount == 1000)
            {
                ToggleTimer();
            }
        }
    }
}
