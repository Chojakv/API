using Microsoft.AspNetCore.Http;

namespace Application.Models.AppUser
{
    public class AppUserAvatarModel
    {
        public IFormFile ProfileImage { get; set; }
    }
}