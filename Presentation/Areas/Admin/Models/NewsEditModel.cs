using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class NewsEditModel : EditModelBase
    {
        [Display(Name = "Id")]
        public int NewsItemId { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Team")]
        public int? TeamId { get; set; }

        [Display(Name = "Team")]
        public string TeamName { get; set; }

        [MaxLength(255)]
        [UIHint("TextSingleLine")]
        public string Subject { get; set; }

        [Required]
        [UIHint("TextMultiLine")]
        [AllowHtml]
        public string Content { get; set; }
    }
}