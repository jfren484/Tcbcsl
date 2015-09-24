using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class PageContentEditModel : EditModelBaseWithAudit
    {
        [Display(Name = "Id")]
        public int PageContentId { get; set; }

        [MaxLength(30), Required]
        [RegularExpression("^\\S+$", ErrorMessage = "Tag may not contain whitespace")]
        [Display(Name = "Tag")]
        public string PageTag { get; set; }

        [MaxLength(50), Required]
        public string Title { get; set; }

        [Required]
        [UIHint("HtmlEditor")]
        [AllowHtml]
        public string Content { get; set; }
    }
}