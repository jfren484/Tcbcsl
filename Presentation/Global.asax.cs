using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tcbcsl.Data;

namespace Tcbcsl.Presentation
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();

            var db = new TcbcslDbContext();
            Consts.PlayerPoolTeamName = db.TeamYears
                                          .Single(ty => ty.TeamId == Consts.PlayerPoolTeamId && ty.Year == Consts.CurrentYear)
                                          .FullName;
        }
    }
}
