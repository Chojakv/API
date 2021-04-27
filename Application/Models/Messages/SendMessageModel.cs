using System;

namespace Application.Models.Messages
{
    public class SendMessageModel
    {
        public string Subject { get; set; }
        public string Content { get; set; }

        public DateTime SendDate = DateTime.UtcNow;
    
    }

}