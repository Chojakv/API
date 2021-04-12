using System;

namespace API.Models.Messages
{
    public class DetailsSentMessageModel
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string ReceiverId { get; set; }

        public DateTime SendDate { get; set; }
        
    }

}