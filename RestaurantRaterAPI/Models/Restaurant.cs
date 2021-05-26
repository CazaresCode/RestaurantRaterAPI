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

        public double AverageOverallRating // Average rating of the restaurant from ALL the ratings. 
        {
            get
            {
                double totalAverageRating = 0;

                foreach (Rating rating in Ratings)
                {
                    totalAverageRating += rating.TotatlAvgRating;
                }

                return totalAverageRating / Ratings.Count;
            }
        }

        //AvgFoodScore
        public double AverageFoodScore
        {
            get
            {
                double totalAvg = 0;

                foreach (Rating allRatings in Ratings)
                {
                    totalAvg += allRatings.FoodScore;
                }

                return totalAvg / Ratings.Count;
            }
        }

        //AvgEnvironmentScore
        public double AverageEnviromentScore
        {
            get
            {
                double totalAvg = 0;

                foreach (Rating allRatings in Ratings)
                {
                    totalAvg += allRatings.EnvironmentScore;
                }

                return totalAvg / Ratings.Count;
            }
        }

        //AvgCleanlinessScore
        public double AverageCleanlinesstScore
        {
            get
            {
                double totalAvg = 0;

                foreach (Rating allRatings in Ratings)
                {
                    totalAvg += allRatings.CleanlinessScore;
                }

                return totalAvg / Ratings.Count;
            }
        }

        public bool IsRecommended => AverageOverallRating > 8.5; // same as opening up the get and returning this condition. 
    }
}