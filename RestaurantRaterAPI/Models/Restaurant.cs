using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant // known as Entity Object, which is the same as the POCO
    {
        [Key] // ctrl + . to get the using system DataAnnotations
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double Rating { get; set; }
        
        public bool IsRecommended => Rating > 3.5; // same as opening up the get and returning this condition. 
    }
}