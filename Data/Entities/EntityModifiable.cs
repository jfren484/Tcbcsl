using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tcbcsl.Data.Identity;

namespace Tcbcsl.Data.Entities
{
    public abstract class EntityModifiable : EntityCreatable
    {
        [MaxLength(128)]
        public string ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }

        [ForeignKey("ModifiedBy")]
        public virtual TcbcslUser ModifiedByUser { get; set; }
    }
}
