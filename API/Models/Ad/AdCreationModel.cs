using System;
using API.Domain;
using API.Extensions;
using Microsoft.AspNetCore.Http;

namespace API.Models.Ad
{
    public class AdCreationModel
    {
        public Condition Condition { get; set; }
        
        public string Title { get; set; }
        
        public string Author { get; set; }
        
        public string BookName { get; set; }

        public string Content { get; set; }
        
        public float Price { get; set; }

        public IFormFile PictureAttached { get; set; }
        
        public Guid CategoryId { get; set; }
        
        public DateTime CreationDate = DateTime.UtcNow;
        
    }
}