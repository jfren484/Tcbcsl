using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class StatisticsEditScheduleModel
    {
        public StatisticsEditTeamModel Team { get; set; }

        [Display(Name = "Id")]
        public int GameParticipantId { get; set; }

        public DateTime GameDate { get; set; }

        public string Opponent { get; set; }

        public string Outcome { get; set; }

        [Display(Name = "")]
        public string EnterStatsUrl { get; set; }
    }
}

public class StatisticsEditTeamModel
{
    public int TeamId { get; set; }
    public int Year { get; set; }
    public string FullName { get; set; }

    public List<TeamBasicInfoModel> Teams { get; set; }
}
