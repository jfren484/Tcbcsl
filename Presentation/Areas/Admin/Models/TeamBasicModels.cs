using System.Collections.Generic;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class TeamPickerModel : YearModel
    {
        public int? TeamId { get; set; }
        public string FullName { get; set; }

        public List<TeamBasicInfoModel> Teams { get; set; }
    }

    public class TeamBasicInfoModel
    {
        public int? TeamId { get; set; }
        public string FullName { get; set; }
    }
}