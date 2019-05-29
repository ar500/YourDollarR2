using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class DeleteModel : PageModel
    {
        private readonly YourDollarContext _context;

        public DeleteModel(YourDollarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BudgetCategoryDto BudgetCategory { get; set; }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var categoryToDelete = await _context.Categories.FindAsync(id);

                if (categoryToDelete != null)
                {
                    _context.Categories.Remove(categoryToDelete);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetDeletePartialAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var categoryFromRepo = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id.Value);
                BudgetCategory = Mapper.Map<BudgetCategoryDto>(categoryFromRepo);

                if (BudgetCategory != null)
                {
                    return new PartialViewResult
                    {
                        ViewName = "_DeletePartial",
                        ViewData = new ViewDataDictionary<BudgetCategoryDto>(ViewData, BudgetCategory)
                    };
                }
            }

            return BadRequest();
        }
    }
}
