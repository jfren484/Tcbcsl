using Microsoft.AspNetCore.Html;
using System.Collections.Generic;

namespace Tcbcsl.Presentation.Models
{
    public class ChurchListModel : YearModel
    {
        public List<ChurchListChurchModel> Churches { get; set; }
    }

    public class ChurchListChurchModel
    {
        public string Name { get; set; }
        public HtmlString Website { get; set; }
        public HtmlString Information { get; set; }
        public ContactInfoModel ContactInfo { get; set; }
    }
}