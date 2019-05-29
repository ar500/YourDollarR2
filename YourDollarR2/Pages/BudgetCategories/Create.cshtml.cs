using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
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
        public BudgetCategoryDto BudgetCategory { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var mappedCategory = Mapper.Map<BudgetCategory>(BudgetCategory);


            _context.Categories.Add(mappedCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}