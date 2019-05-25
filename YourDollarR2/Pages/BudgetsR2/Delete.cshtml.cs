using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetsR2
{
    public class DeleteModel : PageModel
    {
        private readonly YourDollarR2.DataAccess.YourDollarContext _context;

        public DeleteModel(YourDollarR2.DataAccess.YourDollarContext context)
        {
            _context = context;
        }

        [BindProperty] public BudgetDto Budget { get; set; }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetToDelete = await _context.Budgets.FindAsync(id);

            if (budgetToDelete != null)
            {
                _context.Budgets.Remove(budgetToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetDeletePartialAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var budgetFromRepo = await _context.Budgets.FindAsync(id);
                Budget = Mapper.Map<BudgetDto>(budgetFromRepo);

                if (Budget != null)
                {
                    return new PartialViewResult
                    {
                        ViewName = "_DeletePartial",
                        ViewData = new ViewDataDictionary<BudgetDto>(ViewData, Budget)
                    };
                }

            }

            return RedirectToPage("./Index");
        }
    }
}
