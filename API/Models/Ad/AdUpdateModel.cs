using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.Models.Ad
{
    public class AdUpdateModel
    {
        public Guid Id { get; set; }
        public string Author { get; set; }

        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        
        public float Price { get; set; }
        
    }
}