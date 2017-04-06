using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class GameTournamentDate : EntityCreatable
    {
        public int GameTournamentDateId { get; set; }

        [Required]
        public DateTime GameDate { get; set; }
    }
}
