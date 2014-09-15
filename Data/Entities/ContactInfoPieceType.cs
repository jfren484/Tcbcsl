using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public partial class ContactInfoPieceType : EntityBase
    {
        public int ContactInfoPieceTypeId { get; set; }

        [MaxLength(50), Required]
        public string Description { get; set; }
    }
}
