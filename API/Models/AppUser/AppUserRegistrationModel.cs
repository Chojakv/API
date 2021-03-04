using System.ComponentModel.DataAnnotations;

namespace API.Models.AppUser
{
    public class AppUserRegistrationModel
    {
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}