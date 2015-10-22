using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class ScheduleEditModel
    {
        public DateTime Date { get; set; }
        public List<ScheduleBucketEditModel> ConferenceModels { get; set; }
    }

    public class ScheduleBucketEditModel
    {
        public string Label { get; set; }
        public List<ScheduleGameEditModel> Games { get; set; }
    }

    public class ScheduleGameEditModel
    {
        public int GameId { get; set; }
        public DateTime GameDate { get; set; }

        public ScheduleGameParticipantEditModel RoadTeam { get; set; }
        public ScheduleGameParticipantEditModel HomeTeam { get; set; }
    }

    public class ScheduleGameParticipantEditModel
    {
        public int GameParticipantId { get; set; }

        public int TeamYearId { get; set; }

        public string TeamName { get; set; }

        public bool IsHost { get; set; }

        [Required, Range(0, 1000)]
        [UIHint("Runs")]
        public int RunsScored { get; set; }
    }
}