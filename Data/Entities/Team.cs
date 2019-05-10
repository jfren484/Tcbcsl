using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Tcbcsl.Data.Identity;

namespace Tcbcsl.Data.Entities
{
    public class Team : EntityModifiable
    {
        public int TeamId { get; set; }
        public string FieldInformation { get; set; }
        public string Comments { get; set; }

        public virtual ICollection<TeamYear> TeamYears { get; set; }
        public virtual ICollection<NewsItem> NewsItems { get; set; }

        [InverseProperty("CurrentTeam")]
        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<TcbcslUser> Managers { get; set; }

        public virtual ICollection<GameResultReport> GameResultReports { get; set; }
    }
}
