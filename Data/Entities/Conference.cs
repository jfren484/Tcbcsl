using System.Collections.Generic;

namespace Tcbcsl.Data.Entities
{
    public class Conference : EntityModifiable
    {
        public int ConferenceId { get; set; }

        public virtual ICollection<ConferenceYear> ConferenceYears { get; set; }
    }
}
