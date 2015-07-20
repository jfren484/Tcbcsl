using System;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Controllers
{
    public class CoachController : ControllerBase
    {
        [Route("Coaches/{year:year?}")]
        public ActionResult Coaches(int year = Consts.CurrentYear)
        {
            throw new NotImplementedException();
            return View();
        }
    }
}