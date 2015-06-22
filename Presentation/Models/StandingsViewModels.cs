using System.Collections.Generic;

namespace Tcbcsl.Presentation.Models
{
    public class StandingsModel
    {
        public int Year { get; set; }
        public List<DivisionStandingsModel> Divisions { get; set; }
    }

    public class DivisionStandingsModel
    {
        public string DivisionName { get; set; }
        public List<StandingsTeamModel> Teams { get; set; }
    }

    public class StandingsTeamModel
    {
        public int Year { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public double WinningPercentage { get; set; }
        public double GamesBack { get; set; }
        public int DivisionWins { get; set; }
        public int DivisionLosses { get; set; }
        public int DivisionTies { get; set; }
        public int RunsScored { get; set; }
        public int RunsAllowed { get; set; }
        public int Streak { get; set; }
        public int Last5Wins { get; set; }
        public int Last5Losses { get; set; }
        public int Last5Ties { get; set; }
    }
}