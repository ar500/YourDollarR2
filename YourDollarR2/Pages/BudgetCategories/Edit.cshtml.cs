using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class EditModel : PageModel
    {
        private readonly YourDollarR2.DataAccess.YourDollarContext _context;

        public EditModel(YourDollarR2.DataAccess.YourDollarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BudgetCategoryDto BudgetCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryFromRepo = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            var mappedCategory = Mapper.Map<BudgetCategoryDto>(categoryFromRepo);

            BudgetCategory = mappedCategory;

            if (BudgetCategory == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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
                    return NotFound();
                }
                else
                {
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
