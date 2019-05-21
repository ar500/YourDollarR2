using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.ExpensesR2
{
    public class IndexModel : PageModel
    {
        private readonly YourDollarR2.DataAccess.YourDollarContext _context;

        public IList<ExpenseDto> Expenses { get; set; }

        public IList<BudgetCategoryDto> Categories { get; set; }

        public ExpenseDtoForCreateDto Expense { get; set; } = new ExpenseDtoForCreateDto();

        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        public IndexModel(YourDollarR2.DataAccess.YourDollarContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var expensesFromRepo = await _context.Expenses
                .Include(c => c.BudgetCategory)
                .ToListAsync();
            Expenses = Mapper.Map<IList<ExpenseDto>>(expensesFromRepo);

            var categoriesFromRepo = await _context.Categories.ToListAsync();
            Expense.Categories = Mapper.Map<IList<BudgetCategoryDto>>(categoriesFromRepo);

        }
    }
}
