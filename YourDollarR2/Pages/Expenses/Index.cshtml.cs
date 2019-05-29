using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly YourDollarContext _context;

        public IList<ExpenseDto> Expenses { get; set; }

        public IList<BudgetCategoryDto> Categories { get; set; }

        public ExpenseForCreateDto Expense { get; set; } = new ExpenseForCreateDto();

        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        public IndexModel(YourDollarContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var expensesFromRepo = await _context.Expenses
                .Include(c => c.BudgetCategory)
                .ToListAsync();
            Expenses = Mapper.Map<IList<ExpenseDto>>(expensesFromRepo);
        }

        public async Task<IActionResult> OnGetCreateNewPartial()
        {
            var categoriesFromRepo = await _context.Categories.ToListAsync();
            var categoriesSelectList = new List<SelectListItem>();

            foreach (var category in categoriesFromRepo)
            {
                categoriesSelectList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.ShortName
                });
            }

            Expense.Categories = categoriesSelectList;

            return new PartialViewResult
            {
                ViewName = "_CreatePartial",
                ViewData = new ViewDataDictionary<ExpenseForCreateDto>(ViewData, Expense)
            };
        }
    }
}
