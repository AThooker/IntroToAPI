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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostTransaction(Transaction model)
        {
            if(ModelState.IsValid)
            {
                model.DateOfTransaction = DateTime.Now;
                _ctx.Transactions.Add(model);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTransactions()
        {
            List<Transaction> transactions = await _ctx.Transactions.ToListAsync();
            return Ok(transactions);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTransactionByID(int id)
        {
            Transaction transaction = await _ctx.Transactions.FindAsync(id);
            if(transaction != null)
            {
                return Ok(transaction);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateTransaction([FromUri] int id, [FromBody] Transaction model)
        {
            if(ModelState.IsValid)
            {
                Transaction transaction = await _ctx.Transactions.FindAsync(id);
                if(id != model.ID)
                {
                    return BadRequest("ID mismatch you bafoon");
                }
                transaction.CustomerID = model.CustomerID;
                transaction.ID = model.ID;
                transaction.ProductID = model.ProductID;
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTransaction(int id)
        {
            Transaction transaction = await _ctx.Transactions.FindAsync(id);
            if(transaction == null)
            {
                return NotFound();
            }
            _ctx.Transactions.Remove(transaction);
            if(await _ctx.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
