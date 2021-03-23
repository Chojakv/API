using System;
using System.IO;
using System.Threading.Tasks;
using API.Data;
using API.Domain;
using API.Models.AppUser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(DataContext dataContext, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
                var filePath = UploadProfileImage(model);
                user.ProfileImage = await filePath;
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

        private async Task<string> UploadProfileImage(AppUserUpdateModel model)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Avatars");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ProfileImage.CopyToAsync(fileStream);
            }
            return uniqueFileName;
        }
        
        // public async Task<bool> UpdateUserAsync(AppUser user, AppUserUpdateModel model)
        // {
        //     
        //     if (model.Name != null)
        //     {
        //         user.Name = model.Name;
        //     }
        //     if (model.Lastname != null)
        //     {
        //         user.Lastname = model.Lastname;
        //     }
        //     if (model.Email != null)
        //     {
        //         user.Email = model.Email;
        //     }
        //
        //     await _userManager.UpdateAsync(user);
        //
        //     return await _dataContext.SaveChangesAsync() > 0;
        // }

    }
}