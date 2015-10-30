using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Batting Order (Order)")]
        [UIHint("Number")]
        public int BattingOrderPosition { get; set; }

        [Display(Name = "Singles (1B)")]
        [UIHint("Number")]
        public int StatSingles { get; set; }

        [Display(Name = "Doubles (2B)")]
        [UIHint("Number")]
        public int StatDoubles { get; set; }

        [Display(Name = "Triples (3B)")]
        [UIHint("Number")]
        public int StatTriples { get; set; }

        [Display(Name = "Home Runs (HR)")]
        [UIHint("Number")]
        public int StatHomeRuns { get; set; }

        [Display(Name = "Walks (BB)")]
        [UIHint("Number")]
        public int StatWalks { get; set; }

        [Display(Name = "Sacrifice Flies (Sac)")]
        [UIHint("Number")]
        public int StatSacrificeFlies { get; set; }

        [Display(Name = "Outs (Outs)")]
        [UIHint("Number")]
        public int StatOuts { get; set; }

        [Display(Name = "Reached on Fielder's Choice (FC)")]
        [UIHint("Number")]
        public int StatFieldersChoices { get; set; }

        [Display(Name = "Reached on Error (E)")]
        [UIHint("Number")]
        public int StatReachedByErrors { get; set; }

        [Display(Name = "Strikeouts (K)")]
        [UIHint("Number")]
        public int StatStrikeouts { get; set; }

        [Display(Name = "Runs (R)")]
        [UIHint("Number")]
        public int StatRuns { get; set; }

        [Display(Name = "Runs Batted In (RBI)")]
        [UIHint("Number")]
        public int StatRunsBattedIn { get; set; }
    }

    public class StatisticsEditPlayerModel
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }
}