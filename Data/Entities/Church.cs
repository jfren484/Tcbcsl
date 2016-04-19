using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class Church : EntityWithContactInfo
    {
        public int ChurchId { get; set; }

        [MaxLength(100), Required]
        public string FullName { get; set; }

        [MaxLength(100), Required]
        public string DisplayName { get; set; }

        [MaxLength(1000)]
        public string Website { get; set; }

        public string Information { get; set; }

        public virtual ICollection<TeamYear> TeamYears { get; set; }
    }
}
