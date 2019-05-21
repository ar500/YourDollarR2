using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;

namespace YourDollarR2.Pages.ExpensesR2
{
    public class DetailsModel : PageModel
    {
        private readonly YourDollarR2.DataAccess.YourDollarContext _context;

        public DetailsModel(YourDollarR2.DataAccess.YourDollarContext context)
        {
            _context = context;
        }

        public Expense Expense { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? expenseId)
        {
            if (expenseId == null)
            {
                return NotFound();
            }

            Expense = await _context.Expenses.FirstOrDefaultAsync(m => m.Id == expenseId);

            if (Expense == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
