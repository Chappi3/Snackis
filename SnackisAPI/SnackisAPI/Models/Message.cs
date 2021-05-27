using System;

namespace SnackisAPI.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ToUser { get; set; }
        public Guid FromUser { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
