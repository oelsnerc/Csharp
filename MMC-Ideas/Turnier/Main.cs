//********************************************************************
// Main - <type description here>
// (c) Mai 2010 MMC
//********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

//********************************************************************
namespace Turnier
{
    //****************************************************************
    public partial class frmMain : Form
    {
        private string[] Teams;
        private Game[]   Games;
        private Game[,]  Courts;

        public frmMain()
        {
            InitializeComponent();
            Teams = null;
            Games = null;
            Courts = null;
        }

        //------------------------------------------------------------
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                int nTeams = Convert.ToInt32(edCount.Text);     // Get Number of Teams
                int nCourts = Convert.ToInt32(edCourts.Text);   // Get Number of Courts

                NewTournament(nTeams, nCourts);
                WriteToFile(dlgSave.FileName);
            }
        }

        //------------------------------------------------------------
        // Calc the new Tournament
        public void NewTournament(int nTeams, int nCourts)
        {
            int nGames = nTeams * (nTeams - 1) / 2;         // Calc the number of Games to be played
            int nGpC = (nGames + nCourts - 1) / nCourts;    // Calc Number of Games per Court

            Teams = new string[nTeams];
            Games = new Game[nGames];
            Courts = new Game[nCourts, nGpC];

            GetTeams();
            CalcGames();
            SpreadGames();
        }

        //------------------------------------------------------------
        // fill the list with team names
        private void GetTeams()
        {
            for (int i = 0; i < Teams.Length; i++)
            {
                Teams[i] = ("Team" + (i+1).ToString("D2"));
            }
        }

        //------------------------------------------------------------
        // calc all matchups for the teams
        private void CalcGames()
        {
            int cnt = 0;

            for (int i = 0; i < Teams.Length; i++)
            {
                for (int k = i+1; k < Teams.Length; k++)
                {
                    Games[cnt] = new Game(cnt+1, Teams[i], Teams[k]);
                    cnt++;
                }
            }
        }

        //------------------------------------------------------------
        // spread the games over the courts
        private void SpreadGames()
        {
            int nCourts = Courts.GetLength(0);  // Get Number of Courts
            int nGpC = Courts.GetLength(1);     // Get Number of Games per Court

            // first spread the games just in order onto the courts
            for (int i = 0; i < Games.Length; i++)
            {
                Courts[i / nGpC, i % nGpC] = Games[i];
            }
        }

        //------------------------------------------------------------
        public void WriteToFile(string FileName)
        {
            if (File.Exists(FileName)) File.Delete(FileName);

            using (StreamWriter fs = File.CreateText(FileName))
            {
                fs.WriteLine("----------------------------------------");
                fs.WriteLine("Teams:");
                int i = 1;
                foreach (string T in Teams)
                {
                    fs.WriteLine(i.ToString("D2") + '\t' + T);
                    i++;
                }

                fs.WriteLine("----------------------------------------");
                fs.WriteLine("Games:");
                foreach (Game G in Games)
                {
                    fs.WriteLine(G.Name);
                }

                for (int c = 0; c < Courts.GetLength(0); c++)
                {
                    WriteCourt(c, fs);
                }

                fs.Close();
            };
        }

        //------------------------------------------------------------
        private void WriteCourt(int Court, StreamWriter fs)
        {
            fs.WriteLine("----------------------------------------");
            fs.WriteLine("Court: #"+Court.ToString("D2"));
            int c = 1;
            foreach (Game G in Courts[Court])
            {
                if (G != null) fs.WriteLine(c.ToString("D3") + '\t' + G.Name);
                c++;
            }

            //for (int c = 0; c < Courts.GetLength(1); c++)
            //{
            //    Game G = Courts[Court,c];
            //    if (G!=null) fs.WriteLine((c+1).ToString("D3") + '\t' + G.Name);
            //}
        }
    }
}

//********************************************************************
// END OF FILE Main
//********************************************************************
