using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class NewsEditModel : EditModelBaseWithAudit
    {
        [Display(Name = "Id")]
        public int NewsItemId { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTimeOffset StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTimeOffset EndDate { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Team")]
        public NewsEditTeamModel Team { get; set; }

        [StringLength(255)]
        public string Subject { get; set; }

        [Required]
        [UIHint("HtmlEditor")]
        public string Content { get; set; }

        [Display(Name = "")]
        public Dictionary<string, string> UrlsForActions { get; set; }
    }

    public class NewsEditTeamModel : TeamBasicInfoModel
    {
        public bool IsReadonly { get; set; }
        public List<TeamBasicInfoModel> Teams { get; set; }
    }
}