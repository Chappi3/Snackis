using System;

namespace SnackisAPI.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid SubCategoryId { get; set; }
    }
}
