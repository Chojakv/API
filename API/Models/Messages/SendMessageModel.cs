using System;

namespace API.Models.Messages
{
    public class SendMessageModel
    {
        //public Guid Id { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }

        //public string SenderId { get; set; }

        //public string ReceiverUsername { get; set; }

        public DateTime SendDate = DateTime.UtcNow;
    
    }

}