using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Entities
{
    public class NewsItem : EntityModifiable
    {
        public int NewsItemId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public int? TeamId { get; set; }

        [MaxLength(255)]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual Team Team { get; set; }
    }
}