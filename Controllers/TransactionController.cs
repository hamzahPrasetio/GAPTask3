using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task3.Data;
using Task3.Models;
using Task3.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.Include(t => t.Customer).Include(t => t.Food).ToListAsync();
        }

        // GET: api/transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.Include(t => t.Customer).Include(t => t.Food).FirstOrDefaultAsync(t => t.TransactionId == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // POST: api/transaction
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionDTO transactionDTO)
        {
            Transaction transaction = new Transaction{
                TransactionId = transactionDTO.TransactionId,
                CustomerId = transactionDTO.CustomerId,
                FoodId = transactionDTO.FoodId,
                Qty = transactionDTO.Qty,
                TotalPrice = transactionDTO.TotalPrice,
                TransactionDate = transactionDTO.TransactionDate,
            };

            var customer = await _context.Customers.FindAsync(transactionDTO.CustomerId);
            if (customer == null)
            {
                return Conflict("Customer with id is not found.");
            }
            var food = await _context.Foods.FindAsync(transactionDTO.FoodId);
            if (food == null)
            {
                return Conflict("Food with id is not found.");
            }
            transaction.Customer = customer;
            transaction.Food = food;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.TransactionId }, transaction);
        }

        // PUT: api/transaction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionDTO transactionDTO)
        {
            if (id != transactionDTO.TransactionId)
            {
                return BadRequest();
            }

            Transaction transaction = new Transaction{
                TransactionId = transactionDTO.TransactionId,
                CustomerId = transactionDTO.CustomerId,
                FoodId = transactionDTO.FoodId,
                Qty = transactionDTO.Qty,
                TotalPrice = transactionDTO.TotalPrice,
                TransactionDate = transactionDTO.TransactionDate,
            };

            var customer = await _context.Customers.FindAsync(transactionDTO.CustomerId);
            if (customer == null)
            {
                return Conflict("Customer with id is not found.");
            }
            var food = await _context.Foods.FindAsync(transactionDTO.FoodId);
            if (food == null)
            {
                return Conflict("Food with id is not found.");
            }
            transaction.Customer = customer;
            transaction.Food = food;

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
