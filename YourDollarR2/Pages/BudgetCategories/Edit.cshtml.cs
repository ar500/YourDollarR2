using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class EditModel : PageModel
    {
        private readonly YourDollarContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(YourDollarContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public BudgetCategoryDto BudgetCategory { get; set; }

        public async Task<IActionResult> OnGetLoadEditPartialAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            if (!BudgetCategoryExists(id.Value))
            {
                return NotFound();
            }

            var categoryFromRepo = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id.Value);

            if (categoryFromRepo == null)
            {
                return BadRequest();
            }

            var mappedCategory = Mapper.Map<BudgetCategoryDto>(categoryFromRepo);

            BudgetCategory = mappedCategory;

            if (BudgetCategory == null)
            {
                return NotFound();
            }

            //return Partial("_EditPartial");

            return new PartialViewResult
            {
                ViewName = "_EditPartial",
                ViewData = new ViewDataDictionary<BudgetCategoryDto>(ViewData, BudgetCategory)
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("The model state was not valid on post.");
                return BadRequest(ModelState);
            }

            var categoryToUpdate = Mapper.Map<BudgetCategory>(BudgetCategory);

            _context.Attach(categoryToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetCategoryExists(BudgetCategory.Id))
                {
                    _logger.LogError($"The category with id: {BudgetCategory.Id} was not found.");
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Db Concurrency Exception");
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BudgetCategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
