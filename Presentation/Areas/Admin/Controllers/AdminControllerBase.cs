using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    public abstract class AdminControllerBase : Presentation.Controllers.ControllerBase
    {
        #region Helpers

        protected List<PhoneTypeModel> GetPhoneTypes()
        {
            return DbContext.PhoneNumberTypes
                            .Select(Mapper.Map<PhoneTypeModel>)
                            .OrderBy(t => t.PhoneNumberTypeId)
                            .ToList();
        }

        protected List<StateModel> GetStates()
        {
            return DbContext.States
                            .Select(Mapper.Map<StateModel>)
                            .OrderBy(s => s.Name)
                            .ToList();
        }

        #endregion
    }
}
