using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetCategoriesController : ControllerBase
    {
        private readonly YourDollarContext _context;

        public BudgetCategoriesController(YourDollarContext context)
        {
            _context = context;
        }

        // GET: api/BudgetCategories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categoriesFromRepo = await _context.Categories.ToListAsync();

            var categoriesToReturn = Mapper.Map<IEnumerable<BudgetCategoryDto>>(categoriesFromRepo);

            return Ok(categoriesToReturn);
        }

        // GET: api/BudgetCategories/5
        [HttpGet("{id}", Name = "GetBudgetCategory")]
        public async Task<IActionResult> GetBudgetCategory(Guid id)
        {
            var budgetCategory = await _context.Categories.FindAsync(id);

            if (budgetCategory == null)
            {
                return NotFound();
            }

            var category = Mapper.Map<BudgetCategoryDto>(budgetCategory);

            return Ok(category);
        }

        // PUT: api/BudgetCategories/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBudgetCategory(Guid id,
            [FromBody] JsonPatchDocument<BudgetCategoryDto> patchDoc)
        {
            if (id == null || patchDoc == null)
            {
                return BadRequest();
            }

            if (!BudgetCategoryExists(id))
            {
                return NotFound();
            }

            var budgetCategoryFromRepo = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            var categoryToPatch = Mapper.Map<BudgetCategoryDto>(budgetCategoryFromRepo);

            patchDoc.ApplyTo(categoryToPatch, ModelState);

            TryValidateModel(categoryToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(categoryToPatch, budgetCategoryFromRepo);

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

        // POST: api/BudgetCategories
        [HttpPost]
        public async Task<IActionResult> PostBudgetCategory([FromBody] BudgetCategoryDto budgetCategory)
        {
            if (budgetCategory == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryToAdd = Mapper.Map<BudgetCategory>(budgetCategory);

            _context.Categories.Add(categoryToAdd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                return StatusCode(500, "The server was unable to handle your request");
            }

            return NoContent();
        }

        // DELETE: api/BudgetCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudgetCategory(Guid id)
        {
            var budgetCategory = await _context.Categories.FindAsync(id);
            if (budgetCategory == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(budgetCategory);
            await _context.SaveChangesAsync();

            var categoryToReturn = Mapper.Map<BudgetCategoryDto>(budgetCategory);

            return Ok(categoryToReturn);
        }

        private bool BudgetCategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
