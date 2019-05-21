using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class GameEditModel : EditModelBaseWithAudit
    {
        public int GameId { get; set; }

        [Required, Display(Name = "Date")]
        public DateTimeOffset GameDate { get; set; }

        [Display(Name = "Game Type")]
        public GameEditTypeModel GameType { get; set; }

        [Display(Name = "Game Status")]
        public GameEditStatusModel GameStatus { get; set; }

        [Required]
        public bool IsFinalized { get; set; }

        [Display(Name = "Road Team")]
        public GameParticipantEditModel RoadParticipant { get; set; }

        [Display(Name = "Home Team")]
        public GameParticipantEditModel HomeParticipant { get; set; }
    }

    public class GameParticipantEditModel : EditModelBaseWithAudit
    {
        public GameEditTeamModel TeamYear { get; set; }

        [Display(Name = "Runs")]
        [Required, Range(0, int.MaxValue, ErrorMessage = "Runs must be greater than or equal to 0.")]
        public int RunsScored { get; set; }
    }

    public class GameEditStatusModel
    {
        public int? GameStatusId { get; set; }
        public string Description { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class GameEditTypeModel
    {
        public int? GameTypeId { get; set; }
        public string Description { get; set; }

        public SelectList ItemSelectList { get; set; }
    }

    public class GameEditTeamModel
    {
        public int TeamYearId { get; set; }
        public string FullName { get; set; }

        public SelectList ItemSelectList { get; set; }
    }
}
