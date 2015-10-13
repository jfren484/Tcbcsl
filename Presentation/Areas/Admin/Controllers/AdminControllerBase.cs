using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Tcbcsl.Presentation.Areas.Admin.Models;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    public abstract class AdminControllerBase : Presentation.Controllers.ControllerBase
    {
        #region Custom Return methods

        protected static ActionResult HttpOk()
        {
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region Helpers

        protected List<PhoneTypeModel> GetPhoneTypes()
        {
            return DbContext.PhoneNumberTypes
                            .Select(Mapper.Map<PhoneTypeModel>)
                            .OrderBy(t => t.PhoneNumberTypeId)
                            .ToList();
        }

        protected SelectList GetStatesSelectList(int? stateId)
        {
            var stateSelectListItems = DbContext.States
                                                .OrderBy(s => s.Name)
                                                .Select(s => new SelectListItem { Value = s.StateId.ToString(), Text = s.Name })
                                                .ToList();
            stateSelectListItems.Insert(0, new SelectListItem());

            return new SelectList(stateSelectListItems, "Value", "Text", stateId);
        }

        #endregion
    }
}
