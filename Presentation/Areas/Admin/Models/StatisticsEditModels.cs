using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class StatisticsEditModel : EditModelBase
    {
        public string TeamName { get; set; }
        public DateTimeOffset GameDate { get; set; }

        public List<StatisticsEditStatLineModel> StatLines { get; set; }

        public string UrlForNewRow { get; set; }
    }

    public class StatisticsEditStatLineModel : EditModelBaseWithAudit
    {
        public int StatLineId { get; set; }

        public StatisticsEditPlayerModel Player { get; set; }

        [Display(Name = "Batting Order (Order)")]
        [UIHint("Number")]
        [Range(1, int.MaxValue, ErrorMessage = "Batting Order (Order) must be greater than or equal to 1.")]
        public int BattingOrderPosition { get; set; }

        [Display(Name = "Singles (1B)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Singles (1B) must be greater than or equal to 0.")]
        public int StatSingles { get; set; }

        [Display(Name = "Doubles (2B)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Doubles (2B) must be greater than or equal to 0.")]
        public int StatDoubles { get; set; }

        [Display(Name = "Triples (3B)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Triples (3B) must be greater than or equal to 0.")]
        public int StatTriples { get; set; }

        [Display(Name = "Home Runs (HR)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Home Runs (HR) must be greater than or equal to 0.")]
        public int StatHomeRuns { get; set; }

        [Display(Name = "Walks (BB)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Walks (BB) must be greater than or equal to 0.")]
        public int StatWalks { get; set; }

        [Display(Name = "Sacrifice Flies (Sac)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Sacrifice Flies (Sac) must be greater than or equal to 0.")]
        public int StatSacrificeFlies { get; set; }

        [Display(Name = "Outs (Outs)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Outs (Outs) must be greater than or equal to 0.")]
        public int StatOuts { get; set; }

        [Display(Name = "Reached on Fielder's Choice (FC)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Reached on Fielder's Choice (FC) must be greater than or equal to 0.")]
        public int StatFieldersChoices { get; set; }

        [Display(Name = "Reached on Error (E)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Reached on Error (E) must be greater than or equal to 0.")]
        public int StatReachedByErrors { get; set; }

        [Display(Name = "Strikeouts (K)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Strikeouts (K) must be greater than or equal to 0.")]
        public int StatStrikeouts { get; set; }

        [Display(Name = "Runs (R)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Runs (R) must be greater than or equal to 0.")]
        public int StatRuns { get; set; }

        [Display(Name = "Runs Batted In (RBI)")]
        [UIHint("Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Runs Batted In (RBI) must be greater than or equal to 0.")]
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