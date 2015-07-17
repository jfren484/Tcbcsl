using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Helpers;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Services
{
    public static class ContactInfoService
    {
        public static ContactInfoModel GetContactInfoModel(EntityWithContactInfo entity)
        {
            return new ContactInfoModel
                   {
                       EmailAddress = string.IsNullOrWhiteSpace(entity.Email) ? null : MvcHtmlString.Create(entity.Email.Replace("@", "@<span style=\"display: none;\">null</span>")),
                       Address = GetAddressInfoModel(entity),
                       PhoneNumbers = new List<PhoneInfoModel>
                                      {
                                          GetPhoneInfoModel(entity.Phone1, entity.Phone1Type),
                                          GetPhoneInfoModel(entity.Phone2, entity.Phone2Type)
                                      }
                           .Where(p => p != null)
                           .ToList()
                   };
        }

        private static AddressInfoModel GetAddressInfoModel(EntityWithContactInfo entity)
        {
            return new AddressInfoModel
            {
                Street1 = entity.Street1,
                Street2 = entity.Street2,
                City = entity.City,
                State = entity.State?.Abbreviation,
                Zip = entity.Zip
            };
        }

        private static PhoneInfoModel GetPhoneInfoModel(string phoneNumber, ContactInfoPieceType phoneType)
        {
            return phoneNumber == null
                       ? null
                       : new PhoneInfoModel
                         {
                             PhoneNumber = phoneNumber.FormatPhoneNumber(),
                             PhoneType = phoneType?.Description
                         };
        }
    }
}