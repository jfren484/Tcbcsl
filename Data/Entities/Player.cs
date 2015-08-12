using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcbcsl.Data.Entities
{
    public class Player : EntityModifiable
    {
        public int PlayerId { get; set; }

        [MaxLength(30), Required]
        public string NameLast { get; set; }

        [MaxLength(20), Required]
        public string NameFirst { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CurrentTeamId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; private set; }

        public virtual Team CurrentTeam { get; set; }

        public virtual ICollection<StatLine> StatLines { get; set; }
    }
}