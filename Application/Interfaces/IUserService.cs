using System.Threading.Tasks;
using Application.Models.AppUser;
using Domain.Domain;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> GetUserByNameAsync(string username);
        Task<PayloadResult<AppUser>> UpdateUserAsync(AppUser user, AppUserUpdateModel model);
        Task<string> UploadAvatar(string username, AppUserAvatarModel image);

    }
}