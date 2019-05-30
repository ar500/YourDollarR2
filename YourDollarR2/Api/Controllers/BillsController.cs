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
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;
using YourDollarR2.Core.Services.Expense;

namespace YourDollarR2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly YourDollarContext _context;
        private readonly IExpenseService _expenseService;
        private readonly ILogger<BillsController> _logger;

        public BillsController(YourDollarContext context, IExpenseService expenseService, ILogger<BillsController> logger)
        {
            _context = context;
            _expenseService = expenseService;
            _logger = logger;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<IActionResult> GetBills()
        {
            var billsFromRepo = await _context.Bills
                .Include(e => e.BudgetCategory)
                .ToListAsync();

            var mappedBills = Mapper.Map<IEnumerable<BillDto>>(billsFromRepo);

            return Ok(mappedBills);
        }

        // GET: api/Bills/5
        [HttpGet("{id}", Name = "GetBill")]
        public async Task<IActionResult> GetBill(Guid id)
        {
            var BillFromRepo = await _context.Bills.FindAsync(id);

            if (BillFromRepo == null)
            {
                return NotFound();
            }

            var BillToReturn = Mapper.Map<BillDto>(BillFromRepo);

            return Ok(BillToReturn);
        }

        // PUT: api/Bills/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBill(Guid id,
            [FromBody] JsonPatchDocument<BillDto> patchDoc)
        {
            _logger.LogInformation($"Patch request recieved for id: {id}");
            if (id == null || patchDoc == null)
            {
                _logger.LogWarning($"The request was incomplete.");
                return BadRequest();
            }

            if (!BillExists(id))
            {
                return NotFound();
            }

            var BillFromRepo = await _context.Bills
                .Include(b => b.BudgetCategory)
                .FirstOrDefaultAsync(c => c.Id == id);

            var BillToPatch = Mapper.Map<BillDto>(BillFromRepo);

            patchDoc.ApplyTo(BillToPatch, ModelState);

            TryValidateModel(BillToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (patchDoc.Operations.Select(p => p.path.ToLower() == "/amountspent").FirstOrDefault())
            {
                var usersSpent = BillToPatch.AmountSpent;
                _logger.LogDebug($"Adding a payment of {usersSpent.ToString("C")} to the Bill with id: {id}");
                BillToPatch.AmountSpent = _expenseService.AddPayment(BillFromRepo, usersSpent);
            }
            else
            { 
                _logger.LogDebug($"Pay in full requested for id: {id}");
                BillToPatch.AmountSpent = _expenseService.PayInFull(BillFromRepo);
            }

            Mapper.Map(BillToPatch, BillFromRepo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical("A DbUpdateConcurrencyException was thrown.");
                _logger.LogInformation($"The Error was: {ex.Message}");

                return StatusCode(500, "The server was unable to handle your request");
            }

            return NoContent();
        }

        // POST: api/Bills
        [HttpPost]
        public async Task<IActionResult> PostBill([FromBody] BillDto Bill)
        {
            if (Bill == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var BillToAdd = Mapper.Map<Bill>(Bill);
            var categoryToAttach =
                await _context.Categories.FirstOrDefaultAsync(c => c.Id == Bill.ReturnedCategoryId);
            if (categoryToAttach == null)
            {
                return BadRequest();
            }

            BillToAdd.BudgetCategory = categoryToAttach;

            _context.Bills.Add(BillToAdd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                return StatusCode(500, "The server was unable to handle your request");
            }

            return NoContent(); //CreatedAtAction("GetBill", new { id = BillToAdd.Id }, BillToAdd);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(Guid id)
        {
            var Bill = await _context.Bills.FindAsync(id);
            if (Bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(Bill);
            await _context.SaveChangesAsync();

            var BillToReturn = Mapper.Map<BillDto>(Bill);

            return Ok(BillToReturn);
        }

        private bool BillExists(Guid id)
        {
            if (!_context.Bills.Any(e => e.Id == id))
            {
                _logger.LogDebug($"Bill with id: {id} was not found on the server.");
                return false;
            }

            return true;
        }
    }
}
