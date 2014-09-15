using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public partial class GameType : EntityBase
    {
        public int GameTypeId { get; set; }

        [MaxLength(50), Required]
        public string Description { get; set; }

        [Required]
        public bool RecordGame { get; set; }
    }
}