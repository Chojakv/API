using System.Threading.Tasks;
using Domain.Domain;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<RegisterResult> RegisterAsync(string username, string email, string password);
        Task<LoginResult> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}