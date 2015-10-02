using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Helpers;
using Tcbcsl.Presentation.Models;

// TODO: Use Automapper for all of these?

namespace Tcbcsl.Presentation.Services
{
    public static class ContactInfoService
    {
        public static ContactInfoModel GetContactInfoModel(EntityWithContactInfo entity)
        {
            var phoneNumbers = entity.PhoneNumbers
                                     .Select(ph => GetPhoneInfoModel(ph.PhoneNumber, ph.PhoneNumberType))
                                     .Where(p => p != null)
                                     .ToList();

            return new ContactInfoModel
                   {
                       EmailAddress = string.IsNullOrWhiteSpace(entity.EmailAddress)
                                          ? null
                                          : MvcHtmlString.Create(entity.EmailAddress.Replace("@", "@<span style=\"display: none;\">null</span>")),
                       Address = GetAddressInfoModel(entity),
                       PhoneNumbers = phoneNumbers
                           .Select(pi => pi.PhoneNumber + (phoneNumbers.Count == 1
                                                               ? ""
                                                               : pi.PhoneType))
                           .ToList()
                   };
        }

        private static AddressInfoModel GetAddressInfoModel(EntityWithContactInfo entity)
        {
            return new AddressInfoModel
            {
                Street1 = entity.Address.Street1,
                Street2 = entity.Address.Street2,
                City = entity.Address.City,
                State = entity.Address.State?.Abbreviation,
                Zip = entity.Address.Zip
            };
        }

        private static PhoneInfoModel GetPhoneInfoModel(string phoneNumber, PhoneNumberType phoneType)
        {
            return phoneNumber == null
                       ? null
                       : new PhoneInfoModel
                         {
                             PhoneNumber = phoneNumber.FormatPhoneNumber(),
                             PhoneType = phoneType == null ? null : $" ({phoneType.Description})"
                         };
        }
    }
}