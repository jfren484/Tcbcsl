using System;

namespace Tcbcsl.Data.Entities
{
    public abstract class EntityModifiable : EntityCreatable
    {
        public string ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public void UpdateModifiedFields(string username)
        {
            Modified = DateTime.Now;
            ModifiedBy = username;
        }
    }
}
