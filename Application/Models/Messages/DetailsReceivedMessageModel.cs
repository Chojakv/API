using System;

namespace Application.Models.Messages
{
    public class DetailsReceivedMessageModel
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string SenderId { get; set; }
        
        public DateTime SendDate { get; set; }
        
        //public bool IsViewed { get; set; }
    }

}