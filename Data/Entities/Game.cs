using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Tcbcsl.Data.Entities
{
    public class Game : EntityModifiable
    {
        public int GameId { get; set; }

        [Required]
        public DateTime GameDate { get; set; }

        [Required]
        public int GameTypeId { get; set; }

        [Required]
        public int GameStatusId { get; set; }

        [Required]
        public bool IsFinalized { get; set; }

        public virtual GameType GameType { get; set; }

        public virtual GameStatus GameStatus { get; set; }

        public virtual ICollection<GameParticipant> GameParticipants { get; set; }

        public virtual ICollection<GameResultReport> GameResultReports { get; set; }

        public GameParticipant RoadParticipant
        {
            get
            {
                return GameParticipants.Single(gp => !gp.IsHost);
            }
        }

        public GameParticipant HomeParticipant
        {
            get
            {
                return GameParticipants.Single(gp => gp.IsHost);
            }
        }

        public void AddResultReportFromLeague()
        {
            GameResultReports.Add(new GameResultReport
            {
                GameStatusId = GameStatusId,
                RoadTeamScore = RoadParticipant.RunsScored,
                HomeTeamScore = HomeParticipant.RunsScored
            });
        }
    }
}