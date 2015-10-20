using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Models
{
    #region Schedule Models

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
        [UIHint("ScheduleGameRowModel")]
        public ScheduleGameRowModel<IScheduleGameRowDataModel> HeaderRow { get; set; }

        [UIHint("ScheduleGameRowModel")]
        public ScheduleGameRowModel<IScheduleGameRowDataModel> RoadTeamRow { get; set; }

        [UIHint("ScheduleGameRowModel")]
        public ScheduleGameRowModel<IScheduleGameRowDataModel> HomeTeamRow { get; set; }
    }

    public interface IScheduleGameRowDataModel {}

    public class ScheduleGameRowModel<T> where T : IScheduleGameRowDataModel
    {
        public T LabelData { get; set; }
        public int GameId { get; set; }
        public bool IsWinner { get; set; }
        public bool DisplayScores { get; set; }

        [Range(0, 1000)]
        [UIHint("Runs")]
        public object Runs { get; set; }

        public object Hits { get; set; }
    }

    public class ScheduleGameHeaderModel : IScheduleGameRowDataModel
    {
        public DateTime GameDate { get; set; }
        public bool DisplayOutcome { get; set; }
        public string Outcome { get; set; }
    }

    public class ScheduleGameTeamModel : IScheduleGameRowDataModel
    {
        public int Year { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string RecordInfo { get; set; }
    }

    #endregion

    #region Calendar Dropdown Models

    public class YearCalendarModel
    {
        public int Year { get; set; }
        public DateTime ActiveDate { get; set; }
        public List<YearCalendarMonthModel> Months { get; set; }
    }

    public class YearCalendarMonthModel
    {
        public int Month { get; set; }
        public string MonthName { get; set; }
        public List<List<YearCalendarDayModel>> Weeks { get; set; }
    }

    public class YearCalendarDayModel
    {
        public int Day { get; set; }
        public DateTime Date { get; set; }
        public bool HasGames { get; set; }
    }

    #endregion
}