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

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>(); // Holds all the rating objects of the restaurant it is tied to.

        public double Rating // Average rating of the restaurant from ALL the ratings. 
        {
            get
            {
                double totalAverageRating = 0;

                foreach (Rating rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }
                return totalAverageRating / Ratings.Count;
            }
        }

        public bool IsRecommended => Rating > 8.5; // same as opening up the get and returning this condition. 
    }
}