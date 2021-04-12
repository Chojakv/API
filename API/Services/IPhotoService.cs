using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public interface IPhotoService
    {
        Task<string> UploadImage(string key, string folder, IFormFile file);
    }
}