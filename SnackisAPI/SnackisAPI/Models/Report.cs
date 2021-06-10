using System;

namespace SnackisAPI.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public string ByUser { get; set; }
        public string PostId { get; set; }
        public string CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
