using System.ComponentModel.DataAnnotations;

namespace Application.Models.AppUser
{
    public class AppUserLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}