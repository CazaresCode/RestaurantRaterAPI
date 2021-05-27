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
    public class RestaurantController : ApiController
    {
        private RestaurantDbContext _context = new RestaurantDbContext();

        [HttpPost] // not needy but it is nice. 
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model) // POST in the naming convention because it is part of the API thing, remember!?
        {
            if (model == null) // But this condition in first because it could be null.
            {
                return BadRequest("Your request body cannot be empy.");
            }

            if (ModelState.IsValid) // Assuring all required properities are present or VALID in this case. //ModelState is part of the API Controller.
            {
                _context.Restaurants.Add(model); // Adding it to the database.
                await _context.SaveChangesAsync(); // MUST save the new entry AND add the keyword `await`.

                return Ok(); // This is the 200 status code, hence feedback to the user saying it was successful.
            }

            return BadRequest(ModelState);
        }

        // GetAll
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync(); // turned the whole restaurants into a list and returned it.
            return Ok(restaurants);
        }

        // GetByID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id); // Built-in method.

            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }

        // Update(PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant(int id, Restaurant updatedRestaurant)
        { // Could do another condition to see if updatedRestaurant is null.
            if (ModelState.IsValid)
            {
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);

                if (restaurant != null)
                {
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Address = updatedRestaurant.Address;

                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // Delete
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The restaurant was succesfully deleted.");
            }

            return InternalServerError();
        }

        // GetAllRecommendedRestaurants
        // HINT: [Route]
        // Remember that the method is empty and would need to be differentated on how to do it. 

        [HttpGet, Route("api/Restaurant/IsRecommended")]
        public async Task<IHttpActionResult> GetRestaurantsByIsRecommended()
        {
            //List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            //List<Restaurant> recommendedRestaurants = new List<Restaurant>();

            //// Doesn't work below because we removing restaurants... so had to new up a new list.
            ////foreach (Restaurant restaurant in restaurants)
            ////{
            ////    if (restaurant.IsRecommended == false)
            ////    {
            ////        restaurants.Remove(restaurant);
            ////    }
            ////}

            //foreach (Restaurant restaurant in restaurants)
            //{
            //    if (restaurant.IsRecommended)
            //    {
            //        recommendedRestaurants.Add(restaurant);
            //    }
            //}

            //if (recommendedRestaurants.Count < 1)
            //{
            //    return NotFound();
            //}

            //return Ok(recommendedRestaurants);


            //This is the same as ABOVE in LINQ                       Similar to Foreach Loop... for each restaurant (r) in our collection, => for each that is true from IsRecommended, to put them in one step.    
            // Had to put ToList after the Restaurants so we can find what restaurant is recommended. REMEMBER that IsRecommended is a READONLY prop. To work around that, you need to put it in a list before.
            List<Restaurant> restuarants =  _context.Restaurants.ToList().Where(r => r.IsRecommended).ToList();
            return Ok(restuarants);
        }
    }
}
