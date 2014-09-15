using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class DivisionYear : EntityModifiable
    {
        public int DivisionYearId { get; set; }

        [Required]
        public int DivisionId { get; set; }

        [Required]
        public int ConferenceYearId { get; set; }

        [Required]
        public int Year { get; set; }

        [MaxLength(20), Required]
        public string Name { get; set; }

        [Required]
        public bool IsInLeague { get; set; }

        [Required]
        public int Sort { get; set; }

        public virtual Division Division { get; set; }
        public virtual ConferenceYear ConferenceYear { get; set; }
        public virtual ICollection<TeamYear> TeamYears { get; set; }
    }
}
