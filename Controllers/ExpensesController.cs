using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pop_Andreea_Georgiana_Lab6.Models;

namespace Pop_Andreea_Georgiana_Lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ExpensesController(ExpenseContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> GetExpense()
        {
          if (_context.Expense == null)
          {
              return NotFound();
          }
            return await _context.ExpenseDTO.ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDTO>> GetExpenses(int id)
        {
          if (_context.Expense == null)
          {
              return NotFound();
          }
            var expenses = await _context.ExpenseDTO.FindAsync(id);

            if (expenses == null)
            {
                return NotFound();
            }

            return expenses;
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenses(int id, Expense expenses)
        {
            if (id != expenses.Id)
            {
                return BadRequest();
            }

            _context.Entry(expenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpensesExists(id))
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

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpenses(Expense expenses)
        {
          if (_context.Expense == null)
          {
              return Problem("Entity set 'ExpenseContext.Expense'  is null.");
          }
            _context.Expense.Add(expenses);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expenses.Id }, expenses);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenses(int id)
        {
            if (_context.Expense == null)
            {
                return NotFound();
            }
            var expenses = await _context.Expense.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }

            _context.Expense.Remove(expenses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpensesExists(int id)
        {
            return (_context.Expense?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
