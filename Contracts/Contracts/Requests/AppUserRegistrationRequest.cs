using System.ComponentModel.DataAnnotations;

namespace Contracts.Contracts.Requests
{
    public class AppUserRegistrationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}