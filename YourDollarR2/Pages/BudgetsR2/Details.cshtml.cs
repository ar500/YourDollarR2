using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;

namespace YourDollarR2.Pages.BudgetsR2
{
    public class DetailsModel : PageModel
    {
        private readonly YourDollarR2.DataAccess.YourDollarContext _context;

        public DetailsModel(YourDollarR2.DataAccess.YourDollarContext context)
        {
            _context = context;
        }

        public Budget Budget { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Budget = await _context.Budgets.FirstOrDefaultAsync(m => m.Id == id);

            if (Budget == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
