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
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> PostCustomer(Customer model)
        {
            if(ModelState.IsValid)
            {
                _context.Customers.Add(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Customer> listOfCust = await _context.Customers.ToListAsync();
            return Ok(listOfCust);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if(customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer([FromUri] int id, [FromBody] Customer model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.ID)
                {
                    return BadRequest("Customer ID mismatch");
                }
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            if( await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();

        }
    }
}
