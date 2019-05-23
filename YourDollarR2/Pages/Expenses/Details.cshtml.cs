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
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class DetailsModel : PageModel
    {
        private readonly YourDollarContext _context;

        public DetailsModel(YourDollarContext context)
        {
            _context = context;
        }

        public ExpenseDto Expense { get; set; }

        public IActionResult OnGet()
        {
            return NotFound();
        }

        public async Task<IActionResult> OnGetLoadDetailsPartialAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return RedirectToPage("./Index");
            }

            var expenseFromRepo = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id.Value);
            if (expenseFromRepo == null)
            {
                return NotFound();
            }

            Expense = Mapper.Map<ExpenseDto>(expenseFromRepo);

            return new PartialViewResult
            {
                ViewName = "_DetailsPartial",
                ViewData = new ViewDataDictionary<ExpenseDto>(ViewData, Expense)
            };
        }
    }
}