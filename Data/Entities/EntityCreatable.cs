using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tcbcsl.Data.Identity;

namespace Tcbcsl.Data.Entities
{
    public abstract class EntityCreatable : EntityBase
    {
        [Required, MaxLength(128)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual TcbcslUser CreatedByUser { get; set; }
    }
}
