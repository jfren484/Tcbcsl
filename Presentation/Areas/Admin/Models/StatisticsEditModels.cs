using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class StatisticsEditModel : EditModelBase
    {
        public string TeamName { get; set; }
        public DateTime GameDate { get; set; }

        public List<StatisticsEditStatLineModel> StatLines { get; set; }

        public string UrlForNewRow { get; set; }
    }

    public class StatisticsEditStatLineModel : EditModelBaseWithAudit
    {
        public int StatLineId { get; set; }

        public StatisticsEditPlayerModel Player { get; set; }

        [Display(Name = "Batting Order (Order)")]
        [UIHint("Number")]
        [Min(1)]
        public int BattingOrderPosition { get; set; }

        [Display(Name = "Singles (1B)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatSingles { get; set; }

        [Display(Name = "Doubles (2B)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatDoubles { get; set; }

        [Display(Name = "Triples (3B)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatTriples { get; set; }

        [Display(Name = "Home Runs (HR)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatHomeRuns { get; set; }

        [Display(Name = "Walks (BB)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatWalks { get; set; }

        [Display(Name = "Sacrifice Flies (Sac)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatSacrificeFlies { get; set; }

        [Display(Name = "Outs (Outs)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatOuts { get; set; }

        [Display(Name = "Reached on Fielder's Choice (FC)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatFieldersChoices { get; set; }

        [Display(Name = "Reached on Error (E)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatReachedByErrors { get; set; }

        [Display(Name = "Strikeouts (K)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatStrikeouts { get; set; }

        [Display(Name = "Runs (R)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatRuns { get; set; }

        [Display(Name = "Runs Batted In (RBI)")]
        [UIHint("Number")]
        [Min(0)]
        public int StatRunsBattedIn { get; set; }
    }

    public class StatisticsEditPlayerModel
    {
        [Display(Name = "Player")]
        public int PlayerId { get; set; }

        public string FullName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }
}