using System;
using System.Threading.Tasks;
using API.Data;
using API.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }
        
    }
}