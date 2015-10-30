using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class GameResultsEditModel
    {
        public GameResultsEditTeamModel Team { get; set; }

        [Display(Name = "Id")]
        public int GameParticipantId { get; set; }

        public DateTime GameDate { get; set; }

        public string Opponent { get; set; }

        public string Outcome { get; set; }

        public bool KeepsStats { get; set; }

        [Display(Name = "")]
        public Dictionary<string, string> UrlsForActions { get; set; }
    }
}

public class GameResultsEditTeamModel
{
    public int TeamId { get; set; }
    public int Year { get; set; }
    public string FullName { get; set; }

    public List<TeamBasicInfoModel> Teams { get; set; }
}
