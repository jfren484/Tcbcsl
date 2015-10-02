using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class ContactEmailAddress : EntityModifiable
    {
        public int ContactEmailAddressId { get; set; }

        public int? ChurchId { get; set; }

        public int? CoachId { get; set; }

        public int? ContactInfoPieceTypeId { get; set; }

        [MaxLength(100)]
        public string EmailAddress { get; set; }

        public virtual Church Church { get; set; }

        public virtual Coach Coach { get; set; }

        public virtual ContactInfoPieceType EmailType { get; set; }
    }
}
