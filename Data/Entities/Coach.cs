using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcbcsl.Data.Entities
{
    public class Coach : EntityWithContactInfo
    {
        public int CoachId { get; set; }

        [MaxLength(30), Required]
        public string LastName { get; set; }

        [MaxLength(20), Required]
        public string FirstName { get; set; }

        public string Comments { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; private set; }

        public virtual ICollection<TeamYear> TeamYears { get; set; }
    }
}
