using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class Game : EntityModifiable
    {
        public int GameId { get; set; }

        [Required]
        public DateTime GameDate { get; set; }

        [Required]
        public int GameTypeId { get; set; }

        [Required]
        public int GameStatusId { get; set; }

        [Required]
        public bool IsFinalized { get; set; }

        public virtual GameType GameType { get; set; }

        public virtual GameStatus GameStatus { get; set; }

        public virtual ICollection<GameParticipant> GameParticipants { get; set; }

        public virtual ICollection<GameResultReport> GameResultReports { get; set; }
    }
}