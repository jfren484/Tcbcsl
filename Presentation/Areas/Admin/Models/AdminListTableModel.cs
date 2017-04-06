using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class AdminListTableModel
    {
        public string ListItemName { get; set; }
        public string CreateItemUrl { get; set; }
        public MvcHtmlString CustomActionHeader { get; set; }
    }
}