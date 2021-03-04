using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain
{
    public class Ad
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        
        public string BookName { get; set; }
        public string Content { get; set; }
        public float Price { get; set; }

        public string PictureAttached { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastEditedDate { get; set; }
        
        [ForeignKey("Category")] 
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        [ForeignKey("User")] 
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}