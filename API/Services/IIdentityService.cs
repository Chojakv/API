using System.Threading.Tasks;
using API.Domain;
using API.Models.AppUser;

namespace API.Services
{
    public interface IIdentityService
    {
        Task<RegisterResult> RegisterAsync(string username, string email, string password);

        Task<LoginResult> LoginAsync(string email, string password);

    }
}