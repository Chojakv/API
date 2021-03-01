using System;
using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class Ad
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
        
        public float Price { get; set; }

        public string PictureAttached { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditedDate { get; set; }

        public DateTime ExpiryDate { get; set; }
        //
        // [ForeignKey("Category")] 
        // public string CategoryId { get; set; }
        // public Category Category { get; set; }
        //
        // [ForeignKey("User")] 
        // public string UserId { get; set; }
        // public AppUser User { get; set; }
    }
}