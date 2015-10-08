using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class CoachEditModel : EditModelBaseWithContactInfo
    {
        [Display(Name = "Id")]
        public int CoachId { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string Comments { get; set; }
    }
}