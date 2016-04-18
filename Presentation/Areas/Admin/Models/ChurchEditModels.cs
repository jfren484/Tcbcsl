using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class ChurchEditModel : EditModelBaseWithContactInfo
    {
        [Display(Name = "Id")]
        public int ChurchId { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL (including the protocol: http:// or https://).")]
        [UIHint("String")]
        public string Website { get; set; }

        [Display(Name = "Other Info")]
        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string Information { get; set; }

        public string UrlForReturn { get; set; }
    }
}