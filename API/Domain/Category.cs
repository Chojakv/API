using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace API.Domain
{
    public class Category
    
        {
            [Key] 
            public Guid Id { get; set; }
            
            public string Name { get; set; }
            
            public ICollection<Ad> Ads { get; set; } = new List<Ad>();
        }
}