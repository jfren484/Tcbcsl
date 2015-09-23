using Tcbcsl.Data.Entities;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    public abstract class AdminControllerBase : Presentation.Controllers.ControllerBase
    {
        protected void UpdateCreatedFields(EntityModifiable entity)
        {
            entity.UpdateCreatedFields(User.Identity.Name);
        }

        protected void UpdateModifiedFields(EntityModifiable entity)
        {
            entity.UpdateModifiedFields(User.Identity.Name);
        }
    }
}