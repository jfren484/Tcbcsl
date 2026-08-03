using System.Collections.Generic;

namespace Tcbcsl.Presentation.Models
{
    public enum StandingsType
    {
        Division,
        League
    }

    public class StandingsModel : YearModel
    {
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
        public int DivisionYearId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string ClinchChar { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int TotalTies { get; set; }
        public double DivisionWinningPercentage { get; set; }
        public double DivisionGamesBack { get; set; }
        public int DivisionWins { get; set; }
        public int DivisionLosses { get; set; }
        public int DivisionTies { get; set; }
        public int DivisionRunsScored { get; set; }
        public int DivisionRunsAllowed { get; set; }
        public int TotalRunsScored { get; set; }
        public int TotalRunsAllowed { get; set; }
        public int StreakOutcome { get; set; }
        public int StreakCount { get; set; }
        public int Last5Wins { get; set; }
        public int Last5Losses { get; set; }
        public int Last5Ties { get; set; }
    }
}