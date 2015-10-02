using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class Church : EntityModifiable
    {
        public int ChurchId { get; set; }

        public int? AddressId { get; set; }

        [MaxLength(100), Required]
        public string FullName { get; set; }

        [MaxLength(100), Required]
        public string DisplayName { get; set; }

        public string Website { get; set; }

        public string Information { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<ContactPhoneNumber> PhoneNumbers { get; set; }

        public virtual ICollection<ContactEmailAddress> EmailAddresses { get; set; }

        public virtual ICollection<TeamYear> TeamYears { get; set; }
    }
}
