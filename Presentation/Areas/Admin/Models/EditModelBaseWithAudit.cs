using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public abstract class EditModelBaseWithAudit : EditModelBase
    {
        [Display(Name = "History")]
        public AuditDetailsModel AuditDetails { get; set; }
    }
}