using Microsoft.AspNetCore.Html;
using System.Collections.Generic;

namespace Tcbcsl.Presentation.Models
{
    public class CoachListModel : YearModel
    {
        public List<CoachListDivisionModel> Divisions { get; set; }
    }

    public class CoachListDivisionModel
    {
        public string DivisionName { get; set; }
        public List<CoachListTeamModel> Teams { get; set; }
    }

    public class CoachListTeamModel
    {
        public int TeamId { get; set; }
        public int Year { get; set; }
        public string TeamName { get; set; }
        public CoachListCoachModel Coach { get; set; }
    }

    public class CoachListCoachModel
    {
        public string Name { get; set; }
        public HtmlString Comments { get; set; }
        public ContactInfoModel ContactInfo { get; set; }
    }
}