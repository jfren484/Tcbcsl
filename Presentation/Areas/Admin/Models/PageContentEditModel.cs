using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class PageContentEditModel : EditModelBaseWithAudit
    {
        [Display(Name = "Id")]
        public int PageContentId { get; set; }

        [StringLength(30), Required]
        [RegularExpression("^\\S+$", ErrorMessage = "Tag may not contain whitespace")]
        [Display(Name = "Tag")]
        public string PageTag { get; set; }

        [StringLength(50), Required]
        public string Title { get; set; }

        [Required]
        [UIHint("HtmlEditor")]
        public string Content { get; set; }
    }
}