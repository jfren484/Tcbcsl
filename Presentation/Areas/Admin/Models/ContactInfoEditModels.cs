using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class ContactInfoEditModel
    {
        public AddressEditModel Address { get; set; }

        [StringLength(100)]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Numbers")]
        public List<PhoneEditModel> PhoneNumbers { get; set; }
    }

    public class AddressEditModel
    {
        public int? AddressId { get; set; }

        [StringLength(50)]
        [Display(Name = "Street Address 1")]
        public string Street1 { get; set; }

        [StringLength(50)]
        [Display(Name = "Street Address 2")]
        public string Street2 { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        public StateEditModel State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }
    }

    public class StateModel
    {
        public int? StateId { get; set; }
        public string StateName { get; set; }
    }

    public class StateEditModel : StateModel
    {
        public List<StateModel> States { get; set; }
    }

    public class PhoneTypeModel
    {
        public int? PhoneNumberTypeId { get; set; }
        public string PhoneTypeName { get; set; }
    }

    public class PhoneEditModel : PhoneTypeModel
    {
        public List<PhoneTypeModel> PhoneTypes { get; set; }

        public string PhoneNumber { get; set; }
    }
}