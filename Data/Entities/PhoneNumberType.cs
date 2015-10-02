using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public partial class PhoneNumberType : EntityBase
    {
        public int PhoneNumberTypeId { get; set; }

        [MaxLength(50), Required]
        public string Description { get; set; }
    }
}
