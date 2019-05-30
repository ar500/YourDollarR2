using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Threading.Tasks;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Bills
{
    public class DeleteModel : PageModel
    {
        private readonly YourDollarContext _context;

        public DeleteModel(YourDollarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BillDto Bill { get; set; }

        public IActionResult OnGet()
        {
            return NotFound();
        }

        public async Task<IActionResult> OnGetDeletePartialAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var BillFromRepo = await _context.Bills.FindAsync(id);
                Bill = Mapper.Map<BillDto>(BillFromRepo);

                if (Bill != null)
                {
                    return new PartialViewResult
                    {
                        ViewName = "_DeletePartial",
                        ViewData = new ViewDataDictionary<BillDto>(ViewData, Bill)
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

            var BillToRemove = await _context.Bills.FindAsync(id);

            if (BillToRemove != null)
            {
                _context.Bills.Remove(BillToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}