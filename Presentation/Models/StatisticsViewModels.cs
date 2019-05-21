using System;

namespace Tcbcsl.Presentation.Models
{
    #region Models for primary Actions

    public class GameStatisticsPageModel
    {
        public DateTime GameDate { get; set; }
        public GameStatisticsPageTeamModel RoadTeam { get; set; }
        public GameStatisticsPageTeamModel HomeTeam { get; set; }
    }

    public class GameStatisticsPageTeamModel
    {
        public int GameParticipantId { get; set; }
        public string HostLabel { get; set; }
        public int Year { get; set; }
        public StatisticsTeamInfoModel TeamInfo { get; set; }
    }

    public class LeagueStatisticsPageModel
    {
        public YearEnum Year { get; set; }
    }

    public class PlayerStatisticsPageModel
    {
        public YearEnum Year { get; set; }
        public StatisticsPlayerInfoModel Player { get; set; }
    }

    public class TeamStatisticsPageModel
    {
        public YearEnum Year { get; set; }
        public StatisticsTeamInfoModel Team { get; set; }
        public string SortColumn { get; set; }
    }

    #endregion

    #region Models for Data methods

    #region Base Models

    public class BaseStatisticsRowModel
    {
        public YearEnum Year { get; set; }
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

        // To-date columns - only used on Player screens
        public double ToDateAverage { get; set; }
        public double ToDateOnBasePercentage { get; set; }
        public double ToDateSluggingPercentage { get; set; }
        public double ToDateOnBasePlusSlugging { get; set; }

        // Computed columns
        private double _battingAverage = -1;
        public double BattingAverage
        {
            get
            {
                if (_battingAverage < 0)
                {
                    _battingAverage = AtBats == 0 
                        ? 0
                        : Hits / (double)AtBats;
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
                    _onBasePercentage = (AtBats + Walks + SacrificeFlies) == 0
                        ? 0
                        : (Hits + Walks) / (double)(AtBats + Walks + SacrificeFlies);
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
                    _sluggingPercentage = AtBats == 0
                        ? 0
                        : TotalBases / (double)AtBats;
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

    public class CommonStatisticsRowModel : BaseStatisticsRowModel
    {
        public int Games { get; set; }
    }

    public class StatisticsPlayerInfoModel
    {
        public int PlayerId { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }

        public string DisplayName => $"{PlayerFirstName} {PlayerLastName}";
        public string SortName => $"{PlayerLastName}, {PlayerFirstName}";
    }

    public class StatisticsTeamInfoModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }

    #endregion

    public class GamePlayerStatisticsRowModel : BaseStatisticsRowModel
    {
        public StatisticsPlayerInfoModel Player { get; set; }
    }

    public class LeagueIndividualStatisticsRowModel : LeagueTeamStatisticsRowModel
    {
        public StatisticsPlayerInfoModel Player { get; set; }
    }

    public class LeagueTeamStatisticsRowModel : CommonStatisticsRowModel
    {
        public StatisticsTeamInfoModel Team { get; set; }
    }

    public class PlayerCareerStatisticsRowModel : CommonStatisticsRowModel
    {
        public StatisticsPlayerInfoModel Player { get; set; }
        public StatisticsTeamInfoModel Team { get; set; }
    }

    public class PlayerSeasonStatisticsRowModel : BaseStatisticsRowModel
    {
        public int GameId { get; set; }
        public string GameDate { get; set; }
        public StatisticsTeamInfoModel Opponent { get; set; }
    }

    public class TeamPlayerStatisticsRowModel : CommonStatisticsRowModel
    {
        public StatisticsPlayerInfoModel Player { get; set; }
    }

    #endregion
}