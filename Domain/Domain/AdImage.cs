using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Domain
{
    public class AdImage
    {
        public Guid Id { get; set; }
        
        [ForeignKey("Ad")] 
        public Guid AdId { get; set; }
        
        public Ad Ad { get; set; }
        public string ImageUrl { get; set; }
    }
}