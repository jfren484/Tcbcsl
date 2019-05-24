using Tcbcsl.Data.Identity;

namespace Tcbcsl.Data.Entities
{
    public class TcbcslUserTeam
    {
        public string UserId { get; set; }

        public int TeamId { get; set; }

        public virtual TcbcslUser User { get; set; }
        public virtual Team Team { get; set; }
    }
}
