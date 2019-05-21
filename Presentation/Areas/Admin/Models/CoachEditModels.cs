using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Other Info")]
        [UIHint("HtmlEditor")]
        public string Comments { get; set; }

        [Display(Name = "Has Coached")]
        public List<string> HasCoachedFor { get; set; }
    }
}