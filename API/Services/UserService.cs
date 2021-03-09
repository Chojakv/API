using System;
using System.Net.Http;
using System.Threading.Tasks;
using API.Data;
using API.Domain;
using API.Extensions;
using API.Models.AppUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<AppUser> _userManager;

        public UserService(DataContext dataContext, UserManager<AppUser> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }
        
        public async Task<bool> UpdateUserAsync(AppUser user, AppUserUpdateModel model)
        {
            
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
            // user.Name = model.Name;
            // user.Lastname = model.Lastname;
            // user.Email = model.Email;
            
            await _userManager.UpdateAsync(user);
        
            return await _dataContext.SaveChangesAsync() > 0;
        }

    }
}