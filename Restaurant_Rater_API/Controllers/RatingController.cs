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
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //POST
        [HttpPost]
        public async Task<IHttpActionResult> PostRating(Rating model)
        {
            if(ModelState.IsValid)
            {
                _context.Ratings.Add(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        //GET
        [HttpGet]
        public async Task<IHttpActionResult> GetRatings()
        {
            List<Rating> listOfRatings = await
                _context.Ratings.ToListAsync();
            return Ok(listOfRatings);
        }
    }
}
