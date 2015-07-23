using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class PageContent : EntityModifiable
    {
        public int PageContentId { get; set; }

        [MaxLength(30), Required]
        public string PageTag { get; set; }

        [MaxLength(50), Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}