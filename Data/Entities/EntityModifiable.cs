using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public abstract class EntityModifiable : EntityCreatable
    {
        [MaxLength(200)]
        public string ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }

        public void UpdateModifiedFields(string username)
        {
            Modified = DateTime.Now;
            ModifiedBy = username;
        }
    }
}
