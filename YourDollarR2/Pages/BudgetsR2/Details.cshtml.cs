using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetsR2
{
    public class DetailsModel : PageModel
    {
        private readonly YourDollarContext _context;

        public DetailsModel(YourDollarContext context)
        {
            _context = context;
        }

        public BudgetDto Budget { get; set; }

        public IActionResult OnGetAsync()
        {
            return NotFound();
        }

        public async Task<IActionResult> OnGetLoadDetailsPartialAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var budgetFromRepo = await _context.Budgets
                .Include(b => b.Expenses)
                .ThenInclude(e => e.BudgetCategory)
                .FirstOrDefaultAsync(b => b.Id == id.Value);

            if(budgetFromRepo == null)
            {
                return NotFound();
            }

            Budget = Mapper.Map<BudgetDto>(budgetFromRepo);

            return new PartialViewResult
            {
                ViewName = "_DetailsPartial",
                ViewData = new ViewDataDictionary<BudgetDto>(ViewData, Budget)
            };
        }
    }
}
