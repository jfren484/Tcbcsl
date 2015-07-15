using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Models
{
    public class ScheduleModel
    {
        public DateTime Date { get; set; }
        public List<ScheduleConferenceModel> ConferenceModels { get; set; }
    }

    public class ScheduleConferenceModel
    {
        public string Label { get; set; }
        public List<ScheduleGameModel> Games { get; set; }
    }

    public class ScheduleGameModel
    {
        public int GameId { get; set; }
        public DateTime GameDate { get; set; }
        public bool DisplayOutcome { get; set; }
        public bool IsComplete { get; set; }
        public string Outcome { get; set; }

        // This list should always contain 2 items, Away team first and Home team second
        public List<ScheduleGameTeamModel> Teams { get; set; }
    }

    public class ScheduleGameTeamModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int Year { get; set; }
        public bool IsWinner { get; set; }
        public string RecordInfo { get; set; }
        public int RunsScored { get; set; }
        public int? Hits { get; set; }
    }

    public class ScheduleGameRowModel
    {
        public string RowClasses { get; set; }
        public bool DisplayScores { get; set; }
        public MvcHtmlString LabelHtml { get; set; }
        public object Runs { get; set; }
        public object Hits { get; set; }
    }
}