using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain
{
    public class Mailbox
    {
        [Key]
        public Guid Id { get; set; }

        public MailboxType Type{ get; set; }
    
        [ForeignKey("User")] 
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    }

    public enum MailboxType
    {
        Sent, Received
    }

}