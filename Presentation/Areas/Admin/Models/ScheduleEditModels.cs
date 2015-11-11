using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class ScheduleEditModel
    {
        public string Label { get; set; }
        public DateTime Date { get; set; }
        public List<ScheduleBucketEditModel> Buckets { get; set; }
    }

    public class ScheduleBucketEditModel
    {
        public GameBucket Bucket { get; set; }
        public List<ScheduleGameEditModel> Games { get; set; }
    }

    public class ScheduleGameEditModel
    {
        public int GameId { get; set; }
        public bool Entered { get; set; }
        public string Outcome { get; set; }
        public DateTime GameDate { get; set; }

        public ScheduleGameParticipantEditModel RoadParticipant { get; set; }
        public ScheduleGameParticipantEditModel HomeParticipant { get; set; }
    }

    public class ScheduleGameParticipantEditModel
    {
        public int GameParticipantId { get; set; }

        public string TeamName { get; set; }

        [Required, Range(0, 1000)]
        public int RunsScored { get; set; }
    }
}