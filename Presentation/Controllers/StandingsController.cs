using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class StandingsController : ControllerBase
    {
        [Route("Standings/{type}/{year:year?}")]
        public ActionResult Standings(string type, int year = Config.CurrentYear)
        {
            var divisions = DbContext
                .DivisionYears
                .Where(dy => dy.Year == year && dy.IsInLeague)
                .OrderBy(dy => dy.ConferenceYear.Sort)
                .ThenBy(dy => dy.Sort)
                .Select(dy => new DivisionStandingsModel
                {
                    DivisionName = dy.Name,
                    Teams = dy.TeamYears
                        .Select(ty => new StandingsTeamModel
                        {
                            TeamId = ty.TeamId,
                            TeamName = string.IsNullOrEmpty(ty.TeamName)
                                ? ty.Church.DisplayName
                                : ty.Church.DisplayName + " " + ty.TeamName
                        })
                        .ToList()
                })
                .ToList();

            return View(new StandingsModel
            {
                Year = year,
                Divisions = divisions
            });
        }
    }
}