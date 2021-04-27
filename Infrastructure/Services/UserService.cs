using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.AppUser;
using Domain.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IPhotoService _photoService;

        public UserService(DataContext dataContext, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IPhotoService photoService)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _photoService = photoService;
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
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
            
            if (model.ProfileImage != null)
            {
                user.ProfileImage = await _photoService.UploadImage("AvatarUrl", "Avatars", model.ProfileImage);
            }
            else user.ProfileImage = user.ProfileImage;
            
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

    }
}