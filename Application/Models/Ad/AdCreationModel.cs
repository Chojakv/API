using System;
using System.Collections.Generic;
using Domain.Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Models.Ad
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
        
        public ICollection<IFormFile> AdPhotos { get; set; }
    }
}