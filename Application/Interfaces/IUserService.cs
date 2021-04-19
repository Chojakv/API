using System.Threading.Tasks;
using Application.Models.AppUser;
using Domain.Domain;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<AppUser> GetUserByNameAsync(string username);
        public Task<AppUser> GetUserByIdAsync(string id);
        public Task<PayloadResult<AppUser>> UpdateUserAsync(AppUser user, AppUserUpdateModel model);

    }
}