using System;

namespace API.Models.Ad
{
    public class AdDetailsModel
    {
        public Guid Id { get; set; }
        
        public string Author { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
        
        public float Price { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditedDate { get; set; }
        
        public Guid CategoryId { get; set; }
    }
}