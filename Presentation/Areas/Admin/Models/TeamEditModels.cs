using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class TeamEditModel : EditModelBaseWithAudit
    {
        [Display(Name = "Id")]
        public int TeamId { get; set; }

        public int Year { get; set; }

        public int TeamYearId { get; set; }

        [Display(Name = "Conference")]
        public TeamEditDivisionModel Conference { get; set; }

        [Display(Name = "Division")]
        public TeamEditDivisionModel Division { get; set; }

        [Display(Name = "Church")]
        public TeamEditChurchModel Church { get; set; }

        [Display(Name = "Team Name")]
        [MaxLength(50)]
        public string TeamName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public TeamEditCoachModel HeadCoach { get; set; }

        [Display(Name = "Keeps Stats")]
        public bool KeepsStats { get; set; }

        [Display(Name = "Paid for Current Year")]
        public bool HasPaid { get; set; }

        [MaxLength(5)]
        public string Clinch { get; set; }

        [Display(Name = "Field Information")]
        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string FieldInformation { get; set; }

        [Display(Name = "Other Info")]
        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string Comments { get; set; }
    }

    public class TeamEditDivisionModel
    {
        public int DivisionId { get; set; }
        public bool IsInLeague { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class TeamEditChurchModel
    {
        public int ChurchId { get; set; }
        public string FullName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class TeamEditCoachModel
    {
        public int CoachId { get; set; }
        public string FullName { get; set; }
        public string SortableName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }
}