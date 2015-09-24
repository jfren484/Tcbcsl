using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Tcbcsl.Presentation.Helpers;
using InputType = Tcbcsl.Presentation.Helpers.InputType;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class ChurchEditModel : EditModelBaseWithContactInfo
    {
        [Display(Name = "Id")]
        public int ChurchId { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [InputType(InputType.Url)]
        public string Website { get; set; }

        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string Information { get; set; }
    }
}