namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class PageContentEditModel : EditModelBase
    {
            public int PageContentId { get; set; }
            public string PageTag { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
    }
}