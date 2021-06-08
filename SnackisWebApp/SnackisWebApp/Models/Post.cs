using System;

namespace SnackisWebApp.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SubCategoryId { get; set; }
    }
}
