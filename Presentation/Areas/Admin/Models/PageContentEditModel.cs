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
        [UIHint("TextSingleLine")]
        public string PageTag { get; set; }

        [MaxLength(50), Required]
        [UIHint("TextSingleLine")]
        public string Title { get; set; }

        [Required]
        [UIHint("TextMultiLine")]
        [AllowHtml]
        public string Content { get; set; }
    }
}