using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcbcsl.Data.Entities
{
    public class Player : EntityModifiable
    {
        public int PlayerId { get; set; }

        [MaxLength(30), Required]
        public string LastName { get; set; }

        [MaxLength(20), Required]
        public string FirstName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CurrentTeamId { get; set; }

        [ForeignKey("CurrentTeamId")]
        public virtual Team CurrentTeam { get; set; }

        public virtual ICollection<StatLine> StatLines { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}