using System.Threading.Tasks;
using API.Domain;
using API.Models.AppUser;

namespace API.Services
{
    public interface IUserService
    {
        public Task<AppUser> GetUserByNameAsync(string username);
        public Task<AppUser> GetUserByIdAsync(string id);
        public Task<PayloadResult<AppUser>> UpdateUserAsync(AppUser user, AppUserUpdateModel model);

    }
}