using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Domain
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public DateTime RegistrationDate { get; set; }
        
        public string ProfileImage { get; set; }

        public ICollection<Ad> Ads { get; set; }
    }
}