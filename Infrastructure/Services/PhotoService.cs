using System;
using System.IO;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class PhotoService: IPhotoService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotoService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> UploadImage(string key, string folder, IFormFile file)
        {
            var url = $"{_configuration.GetValue<string>($"{key}")}";
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, $"{folder}");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return url + uniqueFileName;
        }
        
    }
}