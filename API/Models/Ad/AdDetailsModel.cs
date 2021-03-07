using System;
using API.Models.AppUser;

namespace API.Models.Ad
{
    public class AdDetailsModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public string Author { get; set; }
        
        public string BookName { get; set; }

        public string Content { get; set; }
        
        public float Price { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditedDate { get; set; }
        
        public Guid CategoryId { get; set; }

        public string UserId { get; set; }
    }
}