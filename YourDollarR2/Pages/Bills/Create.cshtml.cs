using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YourDollarR2.Core;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Bills
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
        public BillDto Bill { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var categoryFromRepo =
                await _context.Categories.FirstOrDefaultAsync(c => c.Id == Bill.ReturnedCategoryId);

            var mappedBill = Mapper.Map<Bill>(Bill);

            mappedBill.BudgetCategory = categoryFromRepo;

            _context.Bills.Add(mappedBill);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}