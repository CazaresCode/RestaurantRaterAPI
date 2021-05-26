using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostRating(Rating model)
        {
            if (!ModelState.IsValid) // Will only check to see if the model state is an int as it was required.
            {
                return BadRequest(ModelState);
            }

            Restaurant restaurant = await _context.Restaurants.FindAsync(model.RestaurantId); // Checking to see if restaurant is legit.
            if (restaurant == null)
            {
                return BadRequest($"The target restaurant with the Id of {model.RestaurantId} does not exist.");
            }

            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"You rated {restaurant.Name} successfully.");
            }

            return InternalServerError();
        }

        // Get All Ratings and Get Rating By ID (if you want to...)
        // Get Rating By Restaurant (if you want to...)

        // Update
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating(int id, Rating updateRating)
        {
            if (ModelState.IsValid)
            {
                Rating rating = await _context.Ratings.FindAsync(id);

                if (rating != null)
                {
                    rating.FoodScore = updateRating.FoodScore;
                    rating.EnvironmentScore = updateRating.EnvironmentScore;
                    rating.CleanlinessScore = updateRating.CleanlinessScore;

                    await _context.SaveChangesAsync();
                    return Ok($"You successfully updated your rating.");
                }
                
                return NotFound();
            }

            return BadRequest();
        }

        // Delete



    }
}
