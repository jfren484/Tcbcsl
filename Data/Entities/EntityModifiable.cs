using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public abstract class EntityModifiable : EntityBase
    {
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
