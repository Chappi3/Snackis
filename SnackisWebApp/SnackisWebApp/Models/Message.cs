using System;

namespace SnackisWebApp.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string ToUser { get; set; }
        public string FromUser { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
