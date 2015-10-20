using System;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Helpers;
using Tcbcsl.Presentation.Services;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    [RouteArea("Admin")]
    [RoutePrefix("Schedule")]
    public class ScheduleController : AdminControllerBase
    {
        #region Constructor and Private Fields

        private readonly ScheduleService _scheduleService;

        public ScheduleController()
        {
            _scheduleService = new ScheduleService(DbContext);
        }

        #endregion

        [Route("{date:datetime?}")]
        public ActionResult Schedule(DateTime? date)
        {
            var model = _scheduleService.GetScheduleModelForDate(date);

            if (model == null)
            {
                return new HttpNotFoundResult();
            }

            return View(model);
        }
    }
}