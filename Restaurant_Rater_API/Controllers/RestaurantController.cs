using Restaurant_Rater_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Restaurant_Rater_API.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //POST
        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //GET
        [HttpGet]
        public async Task<IHttpActionResult> GetRestaurant()
        {
            List<Restaurant> listOfRestaurants = await
                _context.Restaurants.ToListAsync();
            return Ok(listOfRestaurants);
        }

        //GET BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant oneRest = await _context.Restaurants.FindAsync(id);
            if (oneRest != null)
            {
                return Ok(oneRest);
            }
            return NotFound();
        }
        //PUT
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant model)
        {
            if (ModelState.IsValid)
            {
                var foundRest = await _context.Restaurants.FindAsync(id);
                if (foundRest != null)
                {
                    foundRest.Name = model.Name;
                    foundRest.Address = model.Address;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        //DELETE
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant(int id)
        {
            var foundRest = await _context.Restaurants.FindAsync(id);
            if(foundRest == null)
            {
                return NotFound();
            }
            _context.Restaurants.Remove(foundRest);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
