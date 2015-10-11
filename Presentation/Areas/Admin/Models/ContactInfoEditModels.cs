using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class AddressEditModel : EditModelBaseWithAudit
    {
        public int AddressId { get; set; }

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

    public class StateEditModel
    {
        public int? StateId { get; set; }
        public string StateName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class PhoneTypeModel
    {
        public int PhoneNumberTypeId { get; set; }
        public string Description { get; set; }
    }

    public class PhoneEditModel : EditModelBaseWithAudit
    {
        public int ContactPhoneNumberId { get; set; }

        [Display(Name = "Type")]
        public int PhoneNumberTypeId { get; set; }

        [Display(Name = "Number")]
        public string PhoneNumber { get; set; }

        public string PhoneTypeName { get; set; }

        public List<PhoneTypeModel> PhoneTypes { get; set; }
    }

    public class PhoneEditModelList : IEnumerable<PhoneEditModel>
    {
        public List<PhoneEditModel> Models { get; }

        public PhoneEditModelList() : this(null) {}
        public PhoneEditModelList(List<PhoneEditModel> models)
        {
            Models = models ?? new List<PhoneEditModel>();
        }

        public void Add(PhoneEditModel model)
        {
            Models.Add(model);
        }

        public IEnumerator<PhoneEditModel> GetEnumerator()
        {
            return Models.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Models.GetEnumerator();
        }
    }
}