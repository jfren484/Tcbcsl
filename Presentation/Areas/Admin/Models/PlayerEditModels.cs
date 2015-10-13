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
        public string NameLast { get; set; }

        [MaxLength(20), Required]
        [Display(Name = "First Name")]
        public string NameFirst { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Has Stats For")]
        public List<string> HasStatsFor { get; set; }
    }

    public class PlayerEditTeamModel
    {
        public int TeamId { get; set; }
        public string FullName { get; set; }

        public string TransferUrl { get; set; }

        public SelectList ItemSelectList { get; set; }
    }
}