using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Domain
{
    public class AppUser : IdentityUser
    {
        [MaxLength(30, ErrorMessage = "Name is too long. Max 30 chars.")]
        public string Name { get; set; }
        [MaxLength(30, ErrorMessage = "LastName is too long. Max 30 chars.")]
        public string Lastname { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        public string ProfileImage { get; set; }

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}