using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public abstract class EditModelBaseWithContactInfo : EditModelBaseWithAudit
    {
        [Display(Name = "Contact Information")]
        public ContactInfoEditModel ContactInfo { get; set; }
    }
}