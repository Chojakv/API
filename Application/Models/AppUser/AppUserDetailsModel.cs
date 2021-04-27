using System;

namespace Application.Models.AppUser
{
    public class AppUserDetailsModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }
        
        public string Email { get; set; }
        
        public string Username { get; set; }

        public string PhoneNumber { get; set; }
        
        public DateTime RegistrationDate { get; set; }
        
        public string ProfileImage { get; set; }
    }
}