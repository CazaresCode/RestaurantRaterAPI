using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        // Get All Ratings REDUNDANT
        // Get Rating By Restaurant REDUNDANT

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
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRating(int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The rating was successfully deleted.");
            }

            return InternalServerError();
        }
    }
}
