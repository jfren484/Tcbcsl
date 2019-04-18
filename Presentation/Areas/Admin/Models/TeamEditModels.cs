using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class TeamEditModel : EditModelBaseWithAudit
    {
        [Display(Name = "Id")]
        public int TeamId { get; set; }

        public YearModel YearModel { get; set; }

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

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Head Coach")]
        public TeamEditCoachModel HeadCoach { get; set; }

        [Display(Name = "Keeps Stats")]
        public bool KeepsStats { get; set; }

        [Display(Name = "Paid for Current Year")]
        public bool HasPaid { get; set; }

        [Display(Name = "Clinch Char")]
        public TeamEditClinchModel Clinch { get; set; }

        [Display(Name = "Field Information")]
        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string FieldInformation { get; set; }

        [Display(Name = "Other Info")]
        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string Comments { get; set; }

        public TeamEditModel()
        {
            Conference = new TeamEditDivisionModel();
            Division = new TeamEditDivisionModel();
            Church = new TeamEditChurchModel();
            HeadCoach = new TeamEditCoachModel();
            Clinch = new TeamEditClinchModel();
        }
    }

    public class TeamListEditModel
    {
        public int TeamYearId { get; set; }
        public int DivisionYearId { get; set; }
        public bool HasPaid { get; set; }
        public bool KeepsStats { get; set; }
        public string Clinch { get; set; }
    }

    /// <summary>
    /// Model for holding the common data used to populate division and clinch dropdowns in the Teams List page
    /// </summary>
    public class TeamListCommonEditModel
    {
        [Display(Name = "Division")]
        public TeamEditDivisionModel Division { get; set; }

        [Display(Name = "Clinch Char")]
        public TeamEditClinchModel Clinch { get; set; }

        public TeamListCommonEditModel()
        {
            Division = new TeamEditDivisionModel();
            Clinch = new TeamEditClinchModel();
        }
    }

    public class TeamYearTransferModel
    {
        [Display(Name = "Id")]
        public int TeamId { get; set; }

        public YearModel YearModel { get; set; }

        public int TeamYearId { get; set; }

        [Display(Name = "Conference")]
        public TeamEditDivisionModel Conference { get; set; }

        [Display(Name = "Division")]
        public TeamEditDivisionModel Division { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Exists in Current Year")]
        public bool ExistsInCurrentYear { get; set; }

        public TeamYearTransferModel()
        {
            Conference = new TeamEditDivisionModel();
            Division = new TeamEditDivisionModel();
        }
    }

    public class TeamEditDivisionModel
    {
        [Display(Name = "Division")]
        public int DivisionYearId { get; set; }

        public bool IsInLeague { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class TeamEditChurchModel
    {
        [Display(Name = "Church")]
        public int ChurchId { get; set; }

        public string FullName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class TeamEditCoachModel
    {
        [Display(Name = "Head Coach")]
        public int CoachId { get; set; }

        public string FullName { get; set; }

        public string SortableName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class TeamEditClinchModel
    {
        public string ClinchChar { get; set; }

        public string Description { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class TeamManageModel
    {
        [Display(Name = "Team")]
        public TeamEditModel Team { get; set; }

        [Display(Name = "Church")]
        public ChurchEditModel Church { get; set; }

        [Display(Name = "Coach")]
        public CoachEditModel HeadCoach { get; set; }
    }
}