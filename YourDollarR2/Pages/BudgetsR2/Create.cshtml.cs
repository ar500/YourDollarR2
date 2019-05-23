﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.Core;

namespace YourDollarR2.Pages.BudgetsR2
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
        public Budget Budget { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Budgets.Add(Budget);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}