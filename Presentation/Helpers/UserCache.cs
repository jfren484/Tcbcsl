using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tcbcsl.Presentation.Helpers
{
    public static class UserCache
    {
        private const string SessionKey = "AssignedTeams";

        public static Dictionary<int, string> AssignedTeams
        {
            get
            {
                return HttpContext.Current.Session[SessionKey] as Dictionary<int, string> ?? SetAssignedTeams();
            }
            private set
            {
                HttpContext.Current.Session[SessionKey] = value;
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Remove(SessionKey);
        }

        private static Dictionary<int, string> SetAssignedTeams()
        {
            return AssignedTeams = HttpContext.Current
                                              .GetOwinContext()
                                              .GetUserManager<ApplicationUserManager>()
                                              .FindByName(HttpContext.Current.User.Identity.Name)
                                              .AssignedTeams
                                              .Select(at => at.TeamYears.OrderByDescending(ty => ty.Year).First())
                                              .ToDictionary(ty => ty.TeamId, ty => ty.FullName);
        }
    }
}