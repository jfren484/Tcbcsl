using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public abstract class EntityCreatable : EntityBase
    {
        [Required]
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
