using System;
using System.IO;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.AppUser;
using Domain.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext dataContext, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }
        
        public async Task<PayloadResult<AppUser>> UpdateUserAsync(AppUser user, AppUserUpdateModel model)
        {
            var userToUpdate = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            if (userToUpdate == null)
                return new PayloadResult<AppUser>
                {
                    Errors = new[] {$"User with username '{user.UserName}' does not exists."}
                };

            if (model.Name != null)
            {
                user.Name = model.Name;
            }
            if (model.Lastname != null)
            {
                user.Lastname = model.Lastname;
            }
            if (model.Email != null)
            {
                user.Email = model.Email;
            }
            if (model.PhoneNumber != null)
            {
                user.PhoneNumber = model.PhoneNumber;
            }

            await _userManager.UpdateAsync(user);

            var update = await _dataContext.SaveChangesAsync() > 0;
            if (!update)
                return new PayloadResult<AppUser>
                {
                    Errors = new[] {$"Could not save changes."},
                    Success = false
                };

            return new PayloadResult<AppUser>
            {
                Payload = user
            };
        }

        public async Task<string> UploadAvatar(string username, AppUserAvatarModel image)
        { 
            var fileName = image.ProfileImage.FileName;
            var extension = Path.GetExtension(fileName);

            var newFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath,"wwwroot", "Avatars", newFileName);

            await using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.ProfileImage.CopyToAsync(fileStream);
            }

            var avatarUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/wwwroot/Avatars/{newFileName}";

            var user = await GetUserByNameAsync(username);
   
            user.ProfileImage = avatarUrl;
            
            await _dataContext.SaveChangesAsync();

            return avatarUrl;
        }
    }
}