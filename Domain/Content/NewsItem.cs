using System;

namespace Domain.Content
{
    public abstract class NewsItem
    {
        public int Id { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IsActive { get; private set; }
        public string Subject { get; private set; }
        public string Content { get; private set; }
    }
}