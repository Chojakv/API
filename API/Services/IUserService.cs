using System.Threading.Tasks;
using API.Domain;

namespace API.Services
{
    public interface IUserService
    {
        public Task<AppUser> GetUserByNameAsync(string username);
    }
}