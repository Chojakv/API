using System;

namespace Application.Models.Messages
{
    public class SendMessageModel
    {
        public string Username { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public DateTime SendDate = DateTime.UtcNow;
    
    }

}