using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcbcsl.Data.Entities
{
    public class TeamYear : EntityModifiable
    {
        public int TeamYearId { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public int Year { get; set; }

        [MaxLength(50)]
        public string TeamName { get; set; }

        [Required]
        public int DivisionYearId { get; set; }

        [Required]
        public int ChurchId { get; set; }

        [Required]
        public int HeadCoachId { get; set; }

        [Required]
        public bool KeepsStats { get; set; }

        [Required]
        public bool HasPaid { get; set; }

        [MaxLength(5)]
        public string Clinch { get; set; }

        public virtual Team Team { get; set; }
        public virtual DivisionYear DivisionYear { get; set; }
        public virtual Church Church { get; set; }

        [ForeignKey("HeadCoachId")]
        public virtual Coach HeadCoach { get; set; }

        public virtual ICollection<GameParticipant> GameParticipants { get; set; }
    }
}
