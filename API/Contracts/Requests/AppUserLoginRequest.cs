using System.ComponentModel.DataAnnotations;

namespace API.Contracts.Requests
{
    public class AppUserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(1)]
        public string Password { get; set; }
    }
}