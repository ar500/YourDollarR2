using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly YourDollarContext _context;

        public ExpensesController(YourDollarContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var expensesFromRepo = await _context.Expenses
                .Include(e => e.BudgetCategory)
                .ToListAsync();

            var mappedExpenses = Mapper.Map<IEnumerable<ExpenseDto>>(expensesFromRepo);

            return Ok(mappedExpenses);
        }

        // GET: api/Expenses/5
        [HttpGet("{id}", Name = "GetExpense")]
        public async Task<IActionResult> GetExpense(Guid id)
        {
            var expenseFromRepo = await _context.Expenses.FindAsync(id);

            if (expenseFromRepo == null)
            {
                return NotFound();
            }

            var expenseToReturn = Mapper.Map<ExpenseDto>(expenseFromRepo);

            return Ok(expenseToReturn);
        }

        // PUT: api/Expenses/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchExpense(Guid id,
            [FromBody] JsonPatchDocument<ExpenseDto> patchDoc)
        {
            if (id == null || patchDoc == null)
            {
                return BadRequest();
            }

            if (!ExpenseExists(id))
            {
                return NotFound();
            }

            var expenseFromRepo = await _context.Expenses
                .Include(b => b.BudgetCategory)
                .FirstOrDefaultAsync(c => c.Id == id);

            var expenseToPatch = Mapper.Map<ExpenseDto>(expenseFromRepo);

            patchDoc.ApplyTo(expenseToPatch, ModelState);

            TryValidateModel(expenseToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(expenseToPatch, expenseFromRepo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "The server was unable to handle your request");
            }

            return NoContent();
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<IActionResult> PostExpense([FromBody] ExpenseDto expense)
        {
            if (expense == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expenseToAdd = Mapper.Map<Expense>(expense);
            var categoryToAttach =
                await _context.Categories.FirstOrDefaultAsync(c => c.Id == expense.ReturnedCategoryId);
            if (categoryToAttach == null)
            {
                return BadRequest();
            }

            expenseToAdd.BudgetCategory = categoryToAttach;

            _context.Expenses.Add(expenseToAdd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                return StatusCode(500, "The server was unable to handle your request");
            }

            return CreatedAtAction("GetExpense", new { id = expenseToAdd.Id }, expenseToAdd);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            var expenseToReturn = Mapper.Map<ExpenseDto>(expense);

            return Ok(expenseToReturn);
        }

        private bool ExpenseExists(Guid id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
