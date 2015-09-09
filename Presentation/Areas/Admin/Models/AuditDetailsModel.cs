using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

// ReSharper disable UnusedMxember.Global
// ReSharper disable UnusedAxutoPropertyAccessor.Global

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    [UsedImplicitly]
    public class AuditDetailsModel
    {
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created")]
        public DateTime Created { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified")]
        public DateTime Modified { get; set; }
    }
}