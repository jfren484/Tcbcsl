using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class GameResultReport : EntityCreatable
    {
        public int GameResultReportId { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public int GameStatusId { get; set; }

        public int HomeTeamScore { get; set; }

        public int RoadTeamScore { get; set; }

        public string Note { get; set; }

        public virtual Game Game { get; set; }

        public virtual GameStatus GameStatus { get; set; }
    }
}
