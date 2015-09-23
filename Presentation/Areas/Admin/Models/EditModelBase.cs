using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public abstract class EditModelBase
    {
        [Display(Name = "")]
        public string EditUrl { get; set; }
    }
}