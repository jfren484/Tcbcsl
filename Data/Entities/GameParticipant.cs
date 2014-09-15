using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class GameParticipant : EntityModifiable
    {
        public int GameParticipantId { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public int TeamYearId { get; set; }

        [Required]
        public bool IsHost { get; set; }

        [Required]
        public int RunsScored { get; set; }

        public virtual Game Game { get; set; }
        public virtual TeamYear TeamYear { get; set; }

        public virtual ICollection<StatLine> StatLines { get; set; }
    }
}