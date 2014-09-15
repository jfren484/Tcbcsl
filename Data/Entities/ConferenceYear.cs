using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class ConferenceYear : EntityModifiable
    {
        public int ConferenceYearId { get; set; }

        [Required]
        public int ConferenceId { get; set; }

        [Required]
        public int Year { get; set; }

        [MaxLength(20), Required]
        public string Name { get; set; }

        [Required]
        public bool IsInLeague { get; set; }

        [Required]
        public int Sort { get; set; }

        public virtual Conference Conference { get; set; }
        public virtual ICollection<DivisionYear> DivisionYears { get; set; }
    }
}
