using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Tcbcsl.Data;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Controllers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [Route("Admin/[controller]")]
    public abstract class AdminControllerBase : TcbcslControllerBase
    {
        public AdminControllerBase(TcbcslDbContext dbContext) : base(dbContext) { }

        #region Custom Return methods

        // TODO: Remove this
        protected ActionResult HttpOk()
        {
            return Ok();// new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region Helpers

        protected SelectList GetGameStatusesSelectListItems(int? gameStatusId, bool includeEmptyOption = false)
        {
            var stateSelectListItems = DbContext.GameStatuses
                                                .OrderBy(gs => gs.GameStatusId)
                                                .Select(gs => new SelectListItem { Value = gs.GameStatusId.ToString(), Text = gs.Description })
                                                .ToList();

            if (includeEmptyOption)
            {
                stateSelectListItems.Insert(0, new SelectListItem());
            }

            return new SelectList(stateSelectListItems, "Value", "Text", gameStatusId);
        }

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
