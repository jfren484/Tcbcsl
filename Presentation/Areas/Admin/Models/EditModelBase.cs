using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class EditModelBase
    {
        public bool IsCreate { get; set; }

        [Display(Name = "")]
        public string EditUrl { get; set; }
    }
}