using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();

        //POST
        [HttpPost]
        public async Task<IHttpActionResult> PostProduct(Product model)
        {
            if(ModelState.IsValid)
            {
                _ctx.Products.Add(model);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        //GET
        [HttpGet]
        public async Task<IHttpActionResult> GetProducts()
        {
            List<Product> products = await _ctx.Products.ToListAsync();
            return Ok(products);
        }
        //Get by ID
        [HttpGet]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            Product product = await _ctx.Products.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        //PUT
        [HttpPut]
        public async Task<IHttpActionResult> UpdateProduct([FromUri] int id, [FromBody] Product model)
        {
            if(ModelState.IsValid)
            {
                Product product = await _ctx.Products.FindAsync(id);
                if(id != model.ID)
                {
                    return BadRequest("Product ID mismatch");
                }
                _ctx.Entry(model).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        //DELETE
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await _ctx.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _ctx.Products.Remove(product);
            if(await _ctx.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
