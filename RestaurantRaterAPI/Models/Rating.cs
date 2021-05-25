using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        [Key] // using statement: DataAnnotations;
        public int Id { get; set; }

        [ForeignKey(nameof(Restaurant))] // using statement: DataAnnotations.Schema;
        public int RestaurantId { get; set; } // This is tied to the Restaurant's ID that was set.
        public virtual Restaurant Restaurant { get; set; }

        [Required]
        [Range(0, 10)]
        public double FoodScore { get; set; }
        
        [Required]
        [Range(0, 10)]
        public double EnvironmentScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double CleanlinessScore { get; set; }
        
        public double AverageRating 
        {
            get
            {
                return (FoodScore + EnvironmentScore + CleanlinessScore) / 3; // Could do lamba =>
            }
        }
    }
}