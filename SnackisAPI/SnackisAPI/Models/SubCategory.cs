using System;
using System.Collections.Generic;

namespace SnackisAPI.Models
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}
