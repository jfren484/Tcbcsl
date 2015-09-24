using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class NewsEditModel : EditModelBaseWithAudit
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
        public NewsEditTeamModel TeamModel { get; set; }

        [MaxLength(255)]
        public string Subject { get; set; }

        [Required]
        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string Content { get; set; }
    }

    public class NewsEditTeamListModel
    {
        public int? TeamId { get; set; }
        public string TeamName { get; set; }
    }

    public class NewsEditTeamModel : NewsEditTeamListModel
    {
        public bool IsReadonly { get; set; }
        public List<NewsEditTeamListModel> Teams { get; set; }
    }
}