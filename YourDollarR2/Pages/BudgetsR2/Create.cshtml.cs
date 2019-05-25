using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetsR2
{
    public class CreateModel : PageModel
    {
        private readonly YourDollarContext _context;

        public CreateModel(YourDollarContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BudgetForCreateOrEditDto Budget { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expensesFromUser = _context.Expenses
                .Where(e => Budget.ReturnedExpenseIds.Contains(e.Id))
                .ToList();

            var budgetToCreate = Mapper.Map<Budget>(Budget);
            budgetToCreate.Expenses = expensesFromUser;

            _context.Budgets.Add(budgetToCreate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}