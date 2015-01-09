using System.Web.Mvc;
using Tcbcsl.Data;

namespace Tcbcsl.Presentation.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly TcbcslDbContext DbContext = new TcbcslDbContext();
    }
}