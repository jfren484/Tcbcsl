using System.Collections.Generic;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Models
{
    public class ChurchListModel
    {
        public int Year { get; set; }
        public List<ChurchListChurchModel> Churches { get; set; }
    }

    public class ChurchListChurchModel
    {
        public string Name { get; set; }
        public MvcHtmlString Website { get; set; }
        public MvcHtmlString Information { get; set; }
        public ContactInfoModel ContactInfo { get; set; }
    }
}