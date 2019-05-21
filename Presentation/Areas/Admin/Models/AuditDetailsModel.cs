using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class AuditDetailsModel
    {
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset Created { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset? Modified { get; set; }
    }
}