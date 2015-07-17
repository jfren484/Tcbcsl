using System.Collections.Generic;

namespace Tcbcsl.Presentation.Models
{
    public class ContactInfoModel
    {
        public AddressInfoModel Address { get; set; }
        public List<PhoneInfoModel> PhoneNumbers { get; set; }
        public List<EmailInfoModel> EmailAddresses { get; set; }
    }

    public class AddressInfoModel
    {
        public string AddressStreet1 { get; set; }
        public string AddressStreet2 { get; set; }
        public string AddressCityStateZip { get; set; }
    }

    public class PhoneInfoModel
    {
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
    }

    public class EmailInfoModel
    {
        public string EmailAddress { get; set; }
        public string EmailType { get; set; }
    }
}