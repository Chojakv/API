using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain
{
    public class Ad
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(60, ErrorMessage = "Title is too long. Max 60 chars.")]
        public string Title { get; set; }
        [MaxLength(50, ErrorMessage = "Author name is too long. Max 30 chars.")]
        public string Author { get; set; }
        [MaxLength(60, ErrorMessage = "BookName is too long. Max 30 chars.")]
        public string BookName { get; set; }
        
        [MaxLength(350, ErrorMessage = "Content is too long. Max 350 chars.")]
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