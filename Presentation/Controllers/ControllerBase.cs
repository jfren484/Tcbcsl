using System.Text;
using System.Web.Mvc;
using Tcbcsl.Data;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly TcbcslDbContext DbContext = new TcbcslDbContext();

        protected new JsonResult Json(object data)
        {
            return Json(data, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        protected new JsonResult Json(object data, JsonRequestBehavior behavior)
        {
            return Json(data, "application/json", Encoding.UTF8, behavior);
        }

        protected new JsonResult Json(object data, string contentType)
        {
            return Json(data, contentType, Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        protected new JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }

        protected new JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonDotNetResult
                   {
                       Data = data,
                       ContentType = contentType,
                       ContentEncoding = contentEncoding,
                       JsonRequestBehavior = behavior
                   };
        }
    }
}