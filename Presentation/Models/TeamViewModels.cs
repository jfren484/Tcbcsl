﻿using System;
using System.Collections.Generic;

namespace Tcbcsl.Presentation.Models
{
    public class TeamsListModel : YearModel
    {
        public List<DivisionTeamsModel> Divisions { get; set; }
    }

    public class DivisionTeamsModel
    {
        public string DivisionName { get; set; }
        public List<TeamsListTeamModel> Teams { get; set; }
    }

    public class TeamsListTeamModel
    {
        public int Year { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }

    public class TeamViewModel : YearModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string DivisionName { get; set; }
        public int ChurchId { get; set; }
        public string ChurchName { get; set; }
        public TeamCoachModel Coach { get; set; }
        public string Field { get; set; }
        public string Comments { get; set; }
        public List<NewsItemModel> NewsItems { get; set; }
        public List<StatsLeaderModel> StatsLeaders { get; set; }
        public TeamScheduleModel Schedule { get; set; }
    }

    public class TeamScheduleModel
    {
        public int TeamId { get; set; }
        public int Year { get; set; }
        public List<TeamGameModel> Games { get; set; }
    }

    public class TeamScheduleDownloadModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int Year { get; set; }
        public List<TeamScheduleDownloadGameModel> Games { get; set; }
    }

    public class TeamCoachModel
    {
        public int Year { get; set; }
        public int CoachId { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public ContactInfoModel ContactInfo { get; set; }
    }

    public class StatsCategory
    {
        public string Name { get; set; }
        public string Field { get; set; }
        public bool IsPercentage { get; set; }
    }

    public class StatsLeaderModel
    {
        public int Year { get; set; }
        public int TeamId { get; set; }
        public StatsCategory Category { get; set; }
        public int? PlayerId { get; set; }
        public string Name { get; set; }
        public decimal? Value { get; set; }
    }

    public class TeamGameModel
    {
        public int GameId { get; set; }
        public DateTime Date { get; set; }
        public int OpponentId { get; set; }
        public string OpponentName { get; set; }
        public string Location { get; set; }
        public bool IsGameCompleted { get; set; }
        public bool DidWin { get; set; }
        public bool DidLose { get; set; }
        public bool IsHomeTeam { get; set; }
        public bool IsNeutralSite { get; set; }
        public bool IsPlaceholder { get; set; }
        public bool IsExhibition { get; set; }
        public string GameResultDescription { get; set; }
    }

    public class TeamScheduleDownloadGameModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string UID { get; set; }
        public string Location { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}