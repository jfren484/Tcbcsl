using System;
using Tcbcsl.Data.Entities;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    public abstract class AdminControllerBase : Presentation.Controllers.ControllerBase
    {
        protected void UpdateCreatedFields(EntityModifiable entity)
        {
            entity.Created = DateTime.Now;
            entity.CreatedBy = User.Identity.Name;
        }

        protected void UpdateModifiedFields(EntityModifiable entity)
        {
            entity.Modified = DateTime.Now;
            entity.ModifiedBy = User.Identity.Name;
        }
    }
}