using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;

namespace API.Models.AppUser
{
    public class AppUserUpdateModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        
        
        //public IFormFile ProfileImage { get; set; }
    }
}