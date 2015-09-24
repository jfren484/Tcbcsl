using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class ContactInfoEditModel
    {
        [MaxLength(50)]
        [Display(Name = "Street Address 1")]
        public string Street1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Street Address 2")]
        public string Street2 { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        public StateEditModel State { get; set; }

        [MaxLength(10)]
        public string Zip { get; set; }

        [Display(Name = "Pimary Phone")]
        public PhoneEditModel PrimaryPhone { get; set; }

        [Display(Name = "Secondary Phone")]
        public PhoneEditModel SecondaryPhone { get; set; }

        [MaxLength(100)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
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
        public int? PhoneTypeId { get; set; }
        public string PhoneTypeName { get; set; }
    }

    public class PhoneEditModel : PhoneTypeModel
    {
        public List<PhoneTypeModel> PhoneTypes { get; set; }

        [MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}