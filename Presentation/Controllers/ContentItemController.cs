using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
                       ? (ActionResult)NotFound()
                       : View(new PageContentModel
                              {
                                  Tag = content.PageTag,
                                  Title = content.Title,
                                  Content = new HtmlString(content.Content)
                              });
        }
    }
}
