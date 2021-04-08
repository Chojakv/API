using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain
{
    public class Photo
    {
        public Guid Id { get; set; }
            
        [ForeignKey("Ad")] 
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }
        
        public string PhotoUrl { get; set; }
    }
}