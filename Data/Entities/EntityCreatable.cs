using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public abstract class EntityCreatable : EntityBase
    {
        [Required, MaxLength(200)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public void UpdateCreatedFields(string username)
        {
            Created = DateTime.Now;
            CreatedBy = username;
        }
    }
}
