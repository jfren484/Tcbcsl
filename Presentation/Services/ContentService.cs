using System;
using System.Collections.Generic;
using System.Linq;
using Tcbcsl.Data;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Services
{
    public class ContentService
    {
        private readonly TcbcslDbContext _db;

        public ContentService(TcbcslDbContext dbContext)
        {
            _db = dbContext;
        }

        public List<NewsItemModel> GetCurrentNews(int? teamId = null)
        {
            var now = DateTime.Now;

            var news = _db.NewsItems.Where(n => n.IsActive && n.StartDate < now && n.EndDate > now);

            // Needs to be done this way to handle SQL NULL values.
            news = teamId == null
                    ? news.Where(n => n.TeamId == null)
                    : news.Where(n => n.TeamId == teamId);

            return news
                .OrderByDescending(n => n.StartDate)
                .Select(n => new NewsItemModel
                {
                    StartDate = n.StartDate,
                    Subject = n.Subject,
                    Content = n.Content
                })
                .ToList();
        }
    }
}