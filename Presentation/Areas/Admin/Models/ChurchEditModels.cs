using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class ChurchEditModel : EditModelBaseWithContactInfo
    {
        [Display(Name = "Id")]
        public int ChurchId { get; set; }

        [Required, MaxLength(100)]
        [UIHint("TextSingleLine")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required, MaxLength(100)]
        [UIHint("TextSingleLine")]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [UIHint("TextSingleLine")]
        public string Website { get; set; }

        [UIHint("TextMultiLine")]
        [AllowHtml]
        public string Information { get; set; }
    }
}