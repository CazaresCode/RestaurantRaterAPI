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
    public class RestaurantController : ApiController
    {
        private RestaurantDbContext _context = new RestaurantDbContext();

        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
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
    }
}
