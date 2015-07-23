using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class ContentItemController : ControllerBase
    {
        [Route("Content/{tag}")]
        public ActionResult PageContent(string tag)
        {
            var content = DbContext.PageContents
                                   .SingleOrDefault(pc => pc.PageTag == tag);

            return content == null
                       ? (ActionResult)HttpNotFound()
                       : View(new PageContentModel
                              {
                                  Tag = content.PageTag,
                                  Title = content.Title,
                                  Content = MvcHtmlString.Create(content.Content)
                              });
        }
    }
}
