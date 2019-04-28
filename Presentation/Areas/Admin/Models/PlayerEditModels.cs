using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class PlayerEditModel : EditModelBaseWithAudit
    {
        [Display(Name = "Id")]
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Current Team")]
        public PlayerEditTeamModel Team { get; set; }

        [MaxLength(30), Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(20), Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Has Stats For")]
        public List<string> HasStatsFor { get; set; }

        public bool UserManagesMultipleTeams { get; set; }
    }

    public class PlayerEditTeamModel
    {
        public int TeamId { get; set; }
        public string FullName { get; set; }

        public string UrlForTransfer { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class PlayerEditMergeModel
    {
        public IList<int> PlayerIds { get; set; }
    }
}