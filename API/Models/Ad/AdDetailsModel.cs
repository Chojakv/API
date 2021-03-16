using System;
using API.Domain;
using API.Models.AppUser;

namespace API.Models.Ad
{
    public class AdDetailsModel
    {
        public Guid Id { get; set; }

        public Condition Condition { get; set; }

        public string Title { get; set; }
        
        public string Author { get; set; }
        
        public string BookName { get; set; }

        public string Content { get; set; }
        
        public float Price { get; set; }

        public string PictureAttached { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditedDate { get; set; }
        
        public Guid CategoryId { get; set; }

        public string UserId { get; set; }
        
        public AppUserDetailsModel User { get; set; }


    }
}