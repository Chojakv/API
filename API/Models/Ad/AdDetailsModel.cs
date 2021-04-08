using System;
using System.Collections.Generic;
using API.Domain;
using API.Models.AppUser;
using API.Models.Category;
using API.Models.Photo;


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
        
        public string CategoryName { get; set; }
        public string CreatedBy { get; set; }

        public AppUserDetailsModel User { get; set; }

        public CategoryDetailsModel Category { get; set; }
        
        public ICollection<PhotoDetailsModel> AdPhotos { get; set; }

    }
}