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

namespace YourDollarR2.Pages.ExpensesR2
{
    public class CreateModel : PageModel
    {
        private readonly YourDollarR2.DataAccess.YourDollarContext _context;

        public CreateModel(YourDollarR2.DataAccess.YourDollarContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ExpenseDto Expense { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var categoryFromRepo =
                await _context.Categories.FirstOrDefaultAsync(c => c.Id == Expense.ReturnedCategoryId);

            var mappedExpense = Mapper.Map<Expense>(Expense);

            mappedExpense.BudgetCategory = categoryFromRepo;

            _context.Expenses.Add(mappedExpense);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}