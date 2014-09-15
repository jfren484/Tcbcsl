using System.Collections.Generic;

namespace Tcbcsl.Data.Entities
{
    public class Division : EntityModifiable
    {
        public int DivisionId { get; set; }

        public virtual ICollection<DivisionYear> DivisionYears { get; set; }
    }
}
