using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public abstract class EditModelBaseWithContactInfo : EditModelBaseWithAudit
    {
        public AddressEditModel Address { get; set; }

        [StringLength(100)]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Numbers")]
        public PhoneEditModelList PhoneNumbers { get; set; }
    }
}