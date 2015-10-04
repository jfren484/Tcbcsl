using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class ContactPhoneNumber : EntityModifiable
    {
        public int ContactPhoneNumberId { get; set; }

        public int? ChurchId { get; set; }

        public int? CoachId { get; set; }

        public int PhoneNumberTypeId { get; set; }

        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }

        public virtual Church Church { get; set; }

        public virtual Coach Coach { get; set; }

        public virtual PhoneNumberType PhoneNumberType { get; set; }
    }
}
