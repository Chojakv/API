using Microsoft.AspNetCore.Http;

namespace Application.Models.AppUser
{
    public class AppUserUpdateModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        
    }
}