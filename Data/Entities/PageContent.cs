using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class PageContent : EntityModifiable
    {
        public int PageContentId { get; set; }

        [MaxLength(30), Required]
        public string PageTag { get; set; }

        [Required]
        public string Content { get; set; }
    }
}