using Microsoft.AspNetCore.Mvc;

namespace Tcbcsl.Presentation.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly TcbcslDbContext DbContext = new TcbcslDbContext();
    }
}