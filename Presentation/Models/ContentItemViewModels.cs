﻿using System;
using System.Web.Mvc;

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
        public MvcHtmlString Content { get; set; }
    }
}