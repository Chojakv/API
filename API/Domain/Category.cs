﻿using System;
using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class Category
    
        {
            [Key] 
            public Guid Id { get; set; }
            public string Name { get; set; }
        
            // public ICollection<Ad> Ads { get; set; }
        }
}