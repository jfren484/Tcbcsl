using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Presentation.Helpers;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class ChurchController : ControllerBase
    {
        [Route("Churches/{year:year?}")]
        public ActionResult Churches(int? year)
        {
            var churches = DbContext
                .Churches
                .Where(ch => ch.TeamYears.Any(ty => ty.Year == (year ?? Consts.CurrentYear) && ty.DivisionYear.IsInLeague))
                .OrderBy(ch => ch.FullName)
                .ToList()
                .Select(ch => new ChurchListChurchModel
                              {
                                  Name = ch.FullName,
                                  Website = ch.Website.UrlToLink(),
                                  Information = string.IsNullOrWhiteSpace(ch.Information) ? null : MvcHtmlString.Create(ch.Information),
                                  ContactInfo = Mapper.Map<ContactInfoModel>(ch)
                })
                .ToList();

            return View(new ChurchListModel
                        {
                            Year = year ?? Consts.CurrentYear,
                            Churches = churches
                        });
        }
    }
}