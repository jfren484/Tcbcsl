using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tcbcsl.Data;

namespace Tcbcsl.Presentation.Controllers
{
    public abstract class ControllerBase : Controller
    {
        // TODO: Fix and/or move to new UserCache
        private const string SessionKey = "AssignedTeams";

        protected readonly TcbcslDbContext DbContext = new TcbcslDbContext();

        public Dictionary<int, string> AssignedTeams
        {
            get
            {
                return /*HttpContext.Current.Session[SessionKey] as Dictionary<int, string> ??*/ SetAssignedTeams();
            }
            private set
            {
                //HttpContext.Current.Session[SessionKey] = value;
            }
        }

        public void ClearAssignedTeams()
        {
            // HttpContext.Current.Session.Remove(SessionKey);
        }

        private Dictionary<int, string> SetAssignedTeams()
        {
            return AssignedTeams = new Dictionary<int, string>();/* HttpContext.Current
                                              .GetOwinContext()
                                              .GetUserManager<ApplicationUserManager>()
                                              .FindByName(HttpContext.Current.User.Identity.Name)
                                              .AssignedTeams
                                              .Select(at => at.TeamYears.OrderByDescending(ty => ty.Year).First())
                                              .ToDictionary(ty => ty.TeamId, ty => ty.FullName); */
        }
    }
}