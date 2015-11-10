using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class GameResultsListModel
    {
        public GameResultsListTeamModel Team { get; set; }

        [Display(Name = "Id")]
        public int GameParticipantId { get; set; }

        public DateTime GameDate { get; set; }

        public string Opponent { get; set; }

        public string Outcome { get; set; }

        public bool KeepsStats { get; set; }

        public bool IsWaitingForMyInput { get; set; }

        public bool NoStats { get; set; }

        [Display(Name = "")]
        public Dictionary<string, string> UrlsForActions { get; set; }
    }

    public class GameResultsListTeamModel
    {
        public int TeamId { get; set; }
        public int Year { get; set; }
        public string FullName { get; set; }

        public List<TeamBasicInfoModel> Teams { get; set; }
    }

    public class GameResultsEditModel
    {
        public DateTime GameDate { get; set; }

        public TeamBasicInfoModel RoadTeam { get; set; }

        public TeamBasicInfoModel HomeTeam { get; set; }

        public bool IsFinalized { get; set; }

        public List<GameResultsEditReportModel> ResultReports { get; set; }
    }

    public class GameResultsEditTeamModel
    {
        public int TeamId { get; set; }
        public int Year { get; set; }
        public string FullName { get; set; }

        public List<TeamBasicInfoModel> Teams { get; set; }
    }

    public class GameResultsEditReportModel
    {
        public string UserName { get; set; }

        public GameResultsEditTeamModel Team { get; set; }

        public bool IsConfirmation { get; set; }

        public GameEditStatusModel GameStatus { get; set; }

        public GameParticipantEditModel RoadTeam { get; set; }

        public GameParticipantEditModel HomeTeam { get; set; }

        public string Note { get; set; }
    }
}
