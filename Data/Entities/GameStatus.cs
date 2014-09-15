using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public partial class GameStatus : EntityBase
    {
        public int GameStatusId { get; set; }

        [MaxLength(50), Required]
        public string Description { get; set; }

        [Required]
        public bool DisplayScores { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}