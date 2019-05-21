using Microsoft.AspNetCore.Html;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tcbcsl.Presentation.Models
{
    public class ContactInfoModel
    {
        public HtmlString EmailAddress { get; set; }
        public AddressInfoModel Address { get; set; }
        public List<string> PhoneNumbers { get; set; }
    }

    public class AddressInfoModel
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string AddressLinkParameter
        {
            get
            {
                var addressPieces = new[] { Street1, Street2, City, State, Zip };
                var addressForLink = string.Join(" ", addressPieces.Where(s => !string.IsNullOrWhiteSpace(s)));

                return HttpUtility.UrlEncode(addressForLink);
            }
        }
    }

    public class PhoneInfoModel
    {
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
    }

    public class YearModel
    {
        public int Year { get; set; } = Consts.CurrentYear;
        public int[] Years { get; set; } = Enumerable.Range(Consts.FirstYear, Consts.CurrentYear - Consts.FirstYear + 1).Reverse().ToArray();
    }
}