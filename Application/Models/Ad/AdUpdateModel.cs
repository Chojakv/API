using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.Models.Ad
{
    public class AdUpdateModel
    {
        public string Title { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        [Required]
        public decimal Price { get; set; }
        public IFormFile PictureAttached { get; set; }
    }
}