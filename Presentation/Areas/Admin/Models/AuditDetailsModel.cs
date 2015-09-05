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
        public string Created { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified")]
        public string Modified { get; set; }
    }
}