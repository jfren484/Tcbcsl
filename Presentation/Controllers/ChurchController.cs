using System.Linq;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Controllers
{
    public class ChurchController : ControllerBase
    {
        [Route("Churches/{year:year?}")]
        public ActionResult Churches(int year = Consts.CurrentYear)
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