using Microsoft.AspNetCore.Html;
using System;

namespace Tcbcsl.Presentation.Models
{
    public class NewsItemModel
    {
        public DateTime StartDate { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }

    public class PageContentModel
    {
        public string Tag { get; set; }
        public string Title { get; set; }
        public HtmlString Content { get; set; }
    }
}