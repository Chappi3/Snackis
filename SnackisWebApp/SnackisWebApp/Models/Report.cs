using System;

namespace SnackisWebApp.Models
{
    public class Report
    {
        public string Id { get; set; }
        public string ByUser { get; set; }
        public string PostId { get; set; }
        public string CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
