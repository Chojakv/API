using System;
using System.Collections.Generic;
using Application.Models.AppUser;
using Application.Models.Category;
using Domain.Domain;

namespace Application.Models.Ad
{
    public class AdDetailsModel
    {
        public Guid Id { get; set; }

        public Condition Condition { get; set; }

        public string Title { get; set; }
        
        public string Author { get; set; }
        
        public string BookName { get; set; }

        public string Content { get; set; }
        
        public decimal Price { get; set; }
        
        public ICollection<AdPhotoDetailsModel> Images { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditedDate { get; set; }
        
        public string CategoryName { get; set; }
        public string CreatedBy { get; set; }

        public AppUserDetailsModel User { get; set; }

        public CategoryDetailsModel Category { get; set; }
        
        
    }
}