using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class StatisticsEditModel
    {
        public string TeamName { get; set; }
        public DateTime GameDate { get; set; }

        public List<StatisticsEditStatLineModel> StatLines { get; set; }

        public string UrlForReturn { get; set; }
    }

    public class StatisticsEditStatLineModel
    {
        public int StatLineId { get; set; }

        public StatisticsEditPlayerModel Player { get; set; }

        public int BattingOrderPosition { get; set; }
        public int StatSingles { get; set; }
        public int StatDoubles { get; set; }
        public int StatTriples { get; set; }
        public int StatHomeRuns { get; set; }
        public int StatWalks { get; set; }
        public int StatSacrificeFlies { get; set; }
        public int StatOuts { get; set; }
        public int StatFieldersChoices { get; set; }
        public int StatReachedByErrors { get; set; }
        public int StatStrikeouts { get; set; }
        public int StatRuns { get; set; }
        public int StatRunsBattedIn { get; set; }
    }

    public class StatisticsEditPlayerModel
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }
}