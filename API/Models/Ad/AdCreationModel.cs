using System;

namespace API.Models.Ad
{
    public class AdCreationModel
    {
        public string Title { get; set; }
        
        public string Author { get; set; }
        
        public string BookName { get; set; }

        public string Content { get; set; }
        
        public float Price { get; set; }
        
        public Guid CategoryId { get; set; }
        
        public DateTime CreationDate = DateTime.UtcNow;
        
    }
}