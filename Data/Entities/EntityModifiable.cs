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

        public void UpdateCreatedFields(string username)
        {
            Created = DateTime.Now;
            CreatedBy = username;
        }

        public void UpdateModifiedFields(string username)
        {
            Modified = DateTime.Now;
            ModifiedBy = username;
        }
    }
}
