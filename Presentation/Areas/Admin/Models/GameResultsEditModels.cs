using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class GameResultsListModel
    {
        public TeamPickerModel Team { get; set; }

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

    public class GameResultsEditModel
    {
        public DateTime GameDate { get; set; }

        [Display(Name = "Road Team")]
        public TeamBasicInfoModel RoadTeam { get; set; }

        [Display(Name = "Home Team")]
        public TeamBasicInfoModel HomeTeam { get; set; }

        public bool IsFinalized { get; set; }

        public List<GameResultsEditReportModel> ResultReports { get; set; }

        public GameResultsEditCreateReportModel NewReport { get; set; }
    }

    public enum ReportSubmitter
    {
        HomeTeam,
        RoadTeam,
        League
    }

    public class GameResultsEditReportModel
    {
        public string UserName { get; set; }

        public ReportSubmitter SubmittedFrom { get; set; }

        public bool IsConfirmation { get; set; }

        public string GameStatus { get; set; }

        public int? RoadTeamScore { get; set; }

        public int? HomeTeamScore { get; set; }

        public string Note { get; set; }
    }

    public class GameResultsEditCreateReportModel
    {
        public bool IsConfirmable { get; set; }

        public string CurrentResult { get; set; }

        [Display(Name = "Report On Behalf Of")]
        public GameResultsTeamModel Team { get; set; }

        [Display(Name = "Confirm Reported Score")]
        public bool IsConfirmation { get; set; }

        [Display(Name = "Result")]
        public GameEditStatusModel GameStatus { get; set; }

        public GameParticipantEditModel RoadParticipant { get; set; }

        public GameParticipantEditModel HomeParticipant { get; set; }

        public string Note { get; set; }

        public string UrlForReturn { get; set; }
    }

    public class GameResultsTeamModel
    {
        public int? TeamId { get; set; }
        public string FullName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }
}
