using System.Collections.Generic;

namespace Tcbcsl.Presentation.Models
{
    public enum StandingsType
    {
        Division,
        League
    }

    public class StandingsModel
    {
        public int Year { get; set; }
        public StandingsType Type { get; set; }
        public bool ShowTies { get; set; }
        public List<StandingsGroupModel> Groups { get; set; }
    }

    public class StandingsGroupModel
    {
        public string Name { get; set; }
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
        public int StreakOutcome { get; set; }
        public int StreakCount { get; set; }
        public int Last5Wins { get; set; }
        public int Last5Losses { get; set; }
        public int Last5Ties { get; set; }
    }
}