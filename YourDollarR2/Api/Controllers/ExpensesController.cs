using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YourDollarR2.Core;
using YourDollarR2.Core.Services;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;


namespace YourDollarR2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly YourDollarContext _context;
        private readonly IExpenseService _expenseService;
        private readonly ILogger<ExpensesController> _logger;

        public ExpensesController(YourDollarContext context, IExpenseService expenseService, ILogger<ExpensesController> logger)
        {
            _context = context;
            _expenseService = expenseService;
            _logger = logger;
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
            _logger.LogInformation($"Patch request recieved for id: {id}");
            if (id == null || patchDoc == null)
            {
                _logger.LogWarning($"The request was incomplete.");
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

            if (patchDoc.Operations.Select(p => p.path.ToLower() == "/amountspent").FirstOrDefault())
            {
                var usersSpent = expenseToPatch.AmountSpent;

                if (patchDoc.Operations.Select(p => p.op == "add").FirstOrDefault())
                {
                    _logger.LogDebug($"Adding a payment of {usersSpent.ToString("C")} to the expense with id: {id}");
                    expenseToPatch.AmountSpent = _expenseService.AddPayment(expenseFromRepo, usersSpent);
                }
                else
                {
                    _logger.LogDebug($"Pay in full requested for id: {id}");
                    expenseToPatch.AmountSpent = _expenseService.PayInFull(expenseFromRepo);
                }
            }

            Mapper.Map(expenseToPatch, expenseFromRepo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical("A DbUpdateConcurrencyException was trown.");
                _logger.LogInformation($"The Error was: {ex.Message}");

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

            return NoContent(); //CreatedAtAction("GetExpense", new { id = expenseToAdd.Id }, expenseToAdd);
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
            if (!_context.Expenses.Any(e => e.Id == id))
            {
                _logger.LogDebug($"Expense with id: {id} was not found on the server.");
                return false;
            }

            return true;
        }
    }
}
