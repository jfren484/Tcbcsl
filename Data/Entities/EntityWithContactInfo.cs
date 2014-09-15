using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcbcsl.Data.Entities
{
    public abstract class EntityWithContactInfo : EntityModifiable
    {
        [MaxLength(50)]
        public string Street1 { get; set; }

        [MaxLength(50)]
        public string Street2 { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        public int? StateId { get; set; }

        [MaxLength(10)]
        public string Zip { get; set; }

        [MaxLength(10)]
        public string Phone1 { get; set; }

        public int? Phone1TypeId { get; set; }

        [MaxLength(10)]
        public string Phone2 { get; set; }

        public int? Phone2TypeId { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public virtual State State { get; set; }

        [ForeignKey("Phone1TypeId")]
        public virtual ContactInfoPieceType Phone1Type { get; set; }

        [ForeignKey("Phone2TypeId")]
        public virtual ContactInfoPieceType Phone2Type { get; set; }
    }
}
