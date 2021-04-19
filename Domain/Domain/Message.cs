using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        //public Guid ParentMessageId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsViewed { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime SendDate { get; set; }
    
        [ForeignKey("Sent")] 
        public Guid SentMailboxId { get; set; }
        public MailboxType Sent { get; set; }
    
        [ForeignKey("Received")] 
        public Guid ReceivedMailboxId { get; set; }
        public MailboxType Received { get; set; }
        
    
    }

}