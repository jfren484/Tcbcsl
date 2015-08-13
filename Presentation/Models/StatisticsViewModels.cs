using System;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Tcbcsl.Presentation.Models
{
    public class StatisticsFilterModel
    {
        public int? GameId { get; set; }
        public int? TeamId { get; set; }
        public int? PlayerId { get; set; }
        public int? Year { get; set; }
        public string[] ColumnHeaders { get; set; }
    }

    #region Models for primary Actions

    public class GameStatisticsModel
    {
        public DateTime GameDate { get; set; }
        public GameStatisticsTeamModel RoadTeam { get; set; }
        public GameStatisticsTeamModel HomeTeam { get; set; }
    }

    public class GameStatisticsTeamModel
    {
        public int GameParticipantId { get; set; }
        public string HostLabel { get; set; }
        public int TeamId { get; set; }
        public int Year { get; set; }
        public string TeamName { get; set; }
    }

    public class TeamStatisticsModel
    {
        public int TeamYearId { get; set; }
        public int Year { get; set; }
        public string TeamName { get; set; }
        public string SortColumn { get; set; }
    }

    #endregion

    #region Models for Data methods

    public class CommonStatisticsModel
    {
        public int PlateAppearances { get; set; }
        public int AtBats { get; set; }
        public int Hits { get; set; }
        public int TotalBases { get; set; }
        public int Runs { get; set; }
        public int RunsBattedIn { get; set; }
        public int Singles { get; set; }
        public int Doubles { get; set; }
        public int Triples { get; set; }
        public int HomeRuns { get; set; }
        public int Walks { get; set; }
        public int SacrificeFlies { get; set; }
        public int Outs { get; set; }
        public int FieldersChoices { get; set; }
        public int ReachedByErrors { get; set; }
        public int Strikeouts { get; set; }

        // Computed columns
        private double _battingAverage = -1;
        public double BattingAverage
        {
            get
            {
                if (_battingAverage < 0)
                {
                    _battingAverage = Hits / (double)AtBats;
                }
                return _battingAverage;
            }
        }

        private double _onBasePercentage = -1;
        public double OnBasePercentage
        {
            get
            {
                if (_onBasePercentage < 0)
                {
                    _onBasePercentage = (Hits + Walks) / (double)(AtBats + Walks + SacrificeFlies);
                }
                return _onBasePercentage;
            }
        }

        private double _sluggingPercentage = -1;
        public double SluggingPercentage
        {
            get
            {
                if (_sluggingPercentage < 0)
                {
                    _sluggingPercentage = TotalBases / (double)AtBats;
                }
                return _sluggingPercentage;
            }
        }

        private double _onBasePlusSlugging = -1;
        public double OnBasePlusSlugging
        {
            get
            {
                if (_onBasePlusSlugging < 0)
                {
                    _onBasePlusSlugging = OnBasePercentage + SluggingPercentage;
                }
                return _onBasePlusSlugging;
            }
        }
    }

    public class GamePlayerStatisticsModel : CommonStatisticsModel
    {
        public string PlayerName { get; set; }
    }

    public class TeamPlayerStatisticsModel : CommonStatisticsModel
    {
        public string PlayerName { get; set; }
        public int Games { get; set; }
    }

    #endregion
}