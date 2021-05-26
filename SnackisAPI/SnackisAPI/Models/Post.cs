using System;
using System.Collections.Generic;

namespace SnackisAPI.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
