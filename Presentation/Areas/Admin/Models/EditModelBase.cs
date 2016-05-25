using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public abstract class EditModelBase
    {
        [Display(Name = "")]
        public string UrlForEdit { get; set; }

        [Display(Name = "")]
        public string UrlForReturn { get; set; }

        protected EditModelBase()
        {
            UrlForReturn = HttpContext.Current?.Request.UrlReferrer?.PathAndQuery;
        }
    }
}