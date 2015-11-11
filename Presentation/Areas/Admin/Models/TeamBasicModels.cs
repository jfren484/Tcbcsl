using System.Collections.Generic;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class TeamPickerModel
    {
        public int TeamId { get; set; }
        public int Year { get; set; }
        public string FullName { get; set; }

        public List<TeamBasicInfoModel> Teams { get; set; }
    }

    public class TeamBasicInfoModel
    {
        public int? TeamId { get; set; }
        public string FullName { get; set; }
    }
}