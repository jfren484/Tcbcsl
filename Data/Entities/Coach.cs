using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class Coach : EntityModifiable
    {
        public int CoachId { get; set; }

        public int? AddressId { get; set; }

        [MaxLength(30), Required]
        public string LastName { get; set; }

        [MaxLength(20), Required]
        public string FirstName { get; set; }

        public string Comments { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<ContactPhoneNumber> PhoneNumbers { get; set; }

        public virtual ICollection<ContactEmailAddress> EmailAddresses { get; set; }

        public virtual ICollection<TeamYear> TeamYears { get; set; }
    }
}
