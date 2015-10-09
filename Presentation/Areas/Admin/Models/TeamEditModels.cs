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
        public TeamEditMetaModel Conference { get; set; }

        [Display(Name = "Division")]
        public TeamEditMetaModel Division { get; set; }

        [Display(Name = "Church Name")]
        public string ChurchName { get; set; }

        [Display(Name = "Team Name")]
        [MaxLength(50)]
        public string TeamName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public bool KeepsStats { get; set; }

        public bool HasPaid { get; set; }

        [MaxLength(5)]
        public string Clinch { get; set; }

        public TeamEditCoachModel HeadCoach { get; set; }

        [Display(Name = "Field Information")]
        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string FieldInformation { get; set; }

        [Display(Name = "Other Info")]
        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string Comments { get; set; }
    }

    public class TeamEditMetaModel
    {
        public bool IsInLeague { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }
    }

    public class TeamEditCoachModel
    {
        public int CoachId { get; set; }
        public string FullName { get; set; }
        public string SortableName { get; set; }
    }
}