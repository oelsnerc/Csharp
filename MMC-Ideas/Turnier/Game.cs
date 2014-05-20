//********************************************************************
// Game - data-class to describe the properties of one game
// (c) Mai 2010 MMC
//********************************************************************
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace Turnier
{
    class Game
    {
        public int FNumber;
        public string FTeamA;
        public string FTeamB;

        public Game(int Number, string TeamA, string TeamB)
        {
            FNumber = Number;
            FTeamA = TeamA;
            FTeamB = TeamB;
        }

        public string Name
        {
            get { return FNumber.ToString("D3") + '\t' + FTeamA + "\tvs\t" + FTeamB; }
        }

        public bool HasConflict(Game other)
        {
            if (other.FTeamA == FTeamA || other.FTeamA == FTeamB) return true;
            if (other.FTeamB == FTeamA || other.FTeamB == FTeamB) return true;
            return false;
        }
    }
}

//********************************************************************
// END OF FILE Game
//********************************************************************
