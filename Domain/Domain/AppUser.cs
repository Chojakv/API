using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Domain
{
    public class AppUser : IdentityUser
    {
        [MaxLength(30, ErrorMessage = "Name is too long. Max 30 chars.")]
        public string Name { get; set; }
        [MaxLength(30, ErrorMessage = "LastName is too long. Max 30 chars.")]
        public string Lastname { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ProfileImage { get; set; }

        public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        public virtual ICollection<Mailbox> Mailboxes { get; set; } = new List<Mailbox>();

    }
}