using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class DeleteModel : PageModel
    {
        private readonly YourDollarContext _context;

        public DeleteModel(YourDollarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExpenseDto Expense { get; set; }

        public IActionResult OnGet()
        {
            return NotFound();
        }

        public async Task<IActionResult> OnGetDeletePartialAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var expenseFromRepo = await _context.Expenses.FindAsync(id);
                Expense = Mapper.Map<ExpenseDto>(expenseFromRepo);

                if (Expense != null)
                {
                    return new PartialViewResult
                    {
                        ViewName = "_DeletePartial",
                        ViewData = new ViewDataDictionary<ExpenseDto>(ViewData, Expense)
                    };
                }
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseToRemove = await _context.Expenses.FindAsync(id);

            if (expenseToRemove != null)
            {
                _context.Expenses.Remove(expenseToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}