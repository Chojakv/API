using System;

namespace API.Models.Ad
{
    public class AdCreationModel
    {
        public string Author { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
        
        public float Price { get; set; }
        
        public DateTime CreationDate = DateTime.UtcNow;
        
    }
}