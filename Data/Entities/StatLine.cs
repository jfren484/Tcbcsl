using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcbcsl.Data.Entities
{
    public class StatLine : EntityModifiable
    {
        public int StatLineId { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        public int GameParticipantId { get; set; }

        [Required]
        public int BattingOrderPosition { get; set; }

        [Required]
        public int StatSingles { get; set; }

        [Required]
        public int StatDoubles { get; set; }

        [Required]
        public int StatTriples { get; set; }

        [Required]
        public int StatHomeRuns { get; set; }

        [Required]
        public int StatWalks { get; set; }

        [Required]
        public int StatSacrificeFlies { get; set; }

        [Required]
        public int StatOuts { get; set; }

        [Required]
        public int StatFieldersChoices { get; set; }

        [Required]
        public int StatReachedByErrors { get; set; }

        [Required]
        public int StatStrikeouts { get; set; }

        [Required]
        public int StatRuns { get; set; }

        [Required]
        public int StatRunsBattedIn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int StatHits { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int StatTotalBases { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int StatAtBats { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int StatPlateAppearances { get; private set; }

        public virtual Player Player { get; set; }
        public virtual GameParticipant GameParticipant { get; set; }
    }
}