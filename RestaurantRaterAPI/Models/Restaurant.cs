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

                return (Ratings.Count > 0) ? totalAvg / Ratings.Count : 0;
            }
        }

        //AvgEnvironmentScore
        public double AverageEnviromentScore
        {
            get
            {
                IEnumerable<double> scores = Ratings.Select(rating => rating.EnvironmentScore);

                double totalEnScore = scores.Sum();

                return (Ratings.Count > 0) ? totalEnScore / Ratings.Count() : 0;
            }
        }

        //AvgCleanlinessScore
        public double AverageCleanlinesstScore
        {
            get
            {
                var totalScore = Ratings.Select(r => r.CleanlinessScore).Sum();
                return (Ratings.Count > 0) ? totalScore / Ratings.Count() : 0;
            }
        }

        public bool IsRecommended => AverageOverallRating > 8.5; // same as opening up the get and returning this condition. 
    }
}