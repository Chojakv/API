using System;

namespace API.Models.AppUser
{
    public class AppUserDetailsModel
    {
        public string Id { get; set; }
        
        public string Email { get; set; }
        
        public string Username { get; set; }
        
        public DateTime RegistrationDate { get; set; }
        
        public string ProfileImage { get; set; }
        
    }
}