using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class StatisticsEditScheduleModel
    {
        public int TeamId { get; set; }

        public int Year { get; set; }

        [Display(Name = "Id")]
        public int GameParticipantId { get; set; }

        public DateTime GameDate { get; set; }

        public string Opponent { get; set; }

        public string Outcome { get; set; }

        [Display(Name = "")]
        public string EnterStatsUrl { get; set; }
    }
}