using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Domain
{
    public class Ad
    {
        [Key]
        public Guid Id { get; set; }
        public Condition Condition { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string BookName { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditedDate { get; set; }
        
        [ForeignKey("Category")] 
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        [ForeignKey("User")] 
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public virtual ICollection<AdImage> Images { get; set; } = new List<AdImage>();
    }
    
    public enum Condition
    {
        New, Used
    }
}