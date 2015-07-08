using System;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class ScheduleController : ControllerBase
    {
        [Route("Schedule/{date:datetime?}")]
        public ActionResult Schedule(DateTime? date)
        {
            var model = new ScheduleModel
            {
                Date = date ?? GetClosestGameDate()
            };
                    
            return View(model);
        }

        private DateTime GetClosestGameDate()
        {
            var dateQuery = from g in DbContext.Games
                            orderby Math.Abs(SqlFunctions.DateDiff("day", g.GameDate, DateTime.Now).Value)
                            select g.GameDate;

            return dateQuery.First().Date;
        }
    }
}