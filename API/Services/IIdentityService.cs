using System.Threading.Tasks;
using API.Domain;

namespace API.Services
{
    public interface IIdentityService
    {
        Task<RegisterResult> RegisterAsync(string username, string email, string password);

        Task<LoginResult> LoginAsync(string email, string password);
        
    }
}