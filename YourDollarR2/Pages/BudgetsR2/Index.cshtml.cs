using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetsR2
{
    public class IndexModel : PageModel
    {
        private readonly YourDollarContext _context;

        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IList<BudgetDto> Budgets { get; set; }

        public BudgetForCreateOrEditDto Budget { get; set; } = new BudgetForCreateOrEditDto();

        public IndexModel(YourDollarContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var budgetsFromRepo = await _context.Budgets
                .Include(e => e.Expenses).ThenInclude(b => b.BudgetCategory)
                .ToListAsync();
            Budgets = Mapper.Map<IList<BudgetDto>>(budgetsFromRepo);
        }

        public async Task<IActionResult> OnGetCreateNewPartial()
        {

            var expensesFromRepo = await _context.Expenses
                .Where(e => e.BudgetId == Budget.Id || e.BudgetId == null)
                .ToListAsync();

            var mappedExpenses = Mapper.Map<IEnumerable<ExpenseDto>>(expensesFromRepo);

            Budget.ExpenseMultiSelectList = new MultiSelectList(mappedExpenses, "Id", "ShortName");

            return new PartialViewResult
            {
                ViewName = "_CreatePartial",
                ViewData = new ViewDataDictionary<BudgetForCreateOrEditDto>(ViewData, Budget)
            };
        }
    }
}
