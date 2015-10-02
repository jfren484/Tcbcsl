using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public abstract class EntityWithContactInfo : EntityModifiable
    {
        public int? AddressId { get; set; }

        [MaxLength(100)]
        public string EmailAddress { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<ContactPhoneNumber> PhoneNumbers { get; set; }
    }
}
