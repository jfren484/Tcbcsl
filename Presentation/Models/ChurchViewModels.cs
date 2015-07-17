using System.Collections.Generic;

namespace Tcbcsl.Presentation.Models
{
    public class ChurchListModel
    {
        public int Year { get; set; }
        public List<ChurchesListChurchModel> Churches { get; set; }

        public class ChurchesListChurchModel
        {
            public string Name { get; set; }
            public string Website { get; set; }
            public string Information { get; set; }
            public ContactInfoModel ContactInfo { get; set; }
        }
    }
}