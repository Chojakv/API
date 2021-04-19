using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Domain
{
    public class Category
    
        {
            [Key] 
            public Guid Id { get; set; }
            
            public string Name { get; set; }
            
            public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();
        }
}