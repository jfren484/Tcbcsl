using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class TeamController : ControllerBase
    {
        [Route("Teams/{year?}")]
        public ActionResult Teams(int year = 2013)
        {
            var divisions = DbContext
                .DivisionYears
                .Where(dy => dy.Year == year && dy.IsInLeague)
                .OrderBy(dy => dy.ConferenceYear.Sort)
                .ThenBy(dy => dy.Sort)
                .Select(dy => new DivisionTeamsModel
                {
                    DivisionName = dy.Name,
                    Teams = dy.TeamYears
                        .Select(ty => new TeamsListTeamModel
                        {
                            TeamId = ty.TeamId,
                            TeamName = string.IsNullOrEmpty(ty.TeamName)
                                ? ty.Church.DisplayName
                                : ty.Church.DisplayName + " " + ty.TeamName
                        })
                        .ToList()
                })
                .ToList();

            return View(new TeamsListModel
            {
                Year = year,
                Divisions = divisions
            });
        }
    }
}