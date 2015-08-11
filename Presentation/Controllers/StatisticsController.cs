using System.Web.Mvc;
using DataTables.Mvc;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    [RoutePrefix("Statistics")]
    public class StatisticsController : ControllerBase
    {
        [Route("")]
        public ActionResult Statistics(StatisticsFilterModel filterModel)
        {
            return View(filterModel);
        }

        [Route("Game")]
        public ActionResult StatisticsForGame(StatisticsFilterModel filterModel)
        {
            return View(filterModel);
        }

        //[HttpPost]
        [Route("GameData/{gameId}")]
        public JsonResult GameData(int gameId)
        {
            var data = new[]
            {
                new[] {"Ian McLeish", gameId.ToString(), "10", "4", "4", "3", "3", "0.750", "0.750", "0.750", "1.500", "2", "1", "3", "0", "0", "0", "0", "0", "1", "0", "0", "0"},
                new[] {"Les Leith", "Fourth TimberCats", "10", "4", "4", "3", "3", "0.750", "0.750", "0.750", "1.500", "3", "0", "3", "0", "0", "0", "0", "0", "1", "0", "0", "0"},
                new[] {"Ryan Lohse", "Fourth TimberCats", "10", "3", "3", "2", "2", "0.667", "0.667", "0.667", "1.333", "2", "1", "2", "0", "0", "0", "0", "0", "1", "0", "0", "0"},
                new[] {"Ryan Laco", "Fourth TimberCats", "10", "3", "3", "2", "2", "0.667", "0.667", "0.667", "1.333", "0", "1", "2", "0", "0", "0", "0", "0", "1", "0", "0", "0"},
                new[] {"Rory Martin", "Fourth TimberCats", "10", "3", "2", "2", "2", "1.000", "1.000", "1.000", "2.000", "3", "1", "2", "0", "0", "0", "1", "0", "0", "0", "0", "0"},
                new[] {"Dave Ruegemer", "Fourth TimberCats", "10", "3", "3", "2", "2", "0.667", "0.667", "0.667", "1.333", "2", "3", "2", "0", "0", "0", "0", "0", "0", "1", "0", "0"},
                new[] {"Matt Morrell", "Fourth TimberCats", "10", "3", "3", "3", "3", "1.000", "1.000", "1.000", "2.000", "1", "3", "3", "0", "0", "0", "0", "0", "0", "0", "0", "0"},
                new[] {"Jared Leith", "Fourth TimberCats", "10", "3", "2", "1", "1", "0.500", "0.333", "0.500", "0.833", "1", "1", "1", "0", "0", "0", "0", "1", "1", "0", "0", "0"},
                new[] {"Jay French", "Fourth TimberCats", "10", "3", "3", "3", "3", "1.000", "1.000", "1.000", "2.000", "0", "3", "3", "0", "0", "0", "0", "0", "0", "0", "0", "0"},
                new[] {"Will Nething", "Fourth TimberCats", "10", "3", "3", "2", "2", "0.667", "0.667", "0.667", "1.333", "1", "1", "2", "0", "0", "0", "0", "0", "0", "1", "0", "0"},
                new[] {"Jay Russell", "Fourth TimberCats", "10", "3", "3", "0", "0", "0.000", "0.000", "0.000", "0.000", "0", "0", "0", "0", "0", "0", "0", "0", "2", "1", "0", "1"},
                new[] {"Aaron Davis", "Fourth TimberCats", "10", "3", "3", "3", "5", "1.000", "1.000", "1.667", "2.667", "1", "1", "2", "0", "1", "0", "0", "0", "0", "0", "0", "0"},
                new[] {"Matthew Bruffey", "Fourth TimberCats", "10", "3", "3", "0", "0", "0.000", "0.000", "0.000", "0.000", "0", "0", "0", "0", "0", "0", "0", "0", "2", "1", "0", "0"}
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}