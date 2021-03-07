using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.Models.Ad
{
    public class AdUpdateModel
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        
        [Required]
        public string Content { get; set; }
        public float Price { get; set; }
        
    }
}