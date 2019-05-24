using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class State : EntityBase
    {
        public int StateId { get; set; }

        [MinLength(2), MaxLength(2), Required]
        public string Abbreviation { get; set; }

        [MaxLength(50), Required]
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
