﻿using System.ComponentModel.DataAnnotations;

namespace Rellish.Models.DTO
{
    public class MenuItemCreateDTO
    {
       
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpecialTag { get; set; }
        public string Category { get; set; }
        [Range(1, int.MaxValue)]
        public double Price { get; set; }
        public string Image { get; set; }
     
    }
}
