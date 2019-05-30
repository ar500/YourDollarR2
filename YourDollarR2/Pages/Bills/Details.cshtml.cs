using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Bills
{
    public class DetailsModel : PageModel
    {
        private readonly YourDollarContext _context;

        public DetailsModel(YourDollarContext context)
        {
            _context = context;
        }

        public BillDto Bill { get; set; }

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

            var BillFromRepo = await _context.Bills.FirstOrDefaultAsync(e => e.Id == id.Value);
            if (BillFromRepo == null)
            {
                return NotFound();
            }

            Bill = Mapper.Map<BillDto>(BillFromRepo);

            return new PartialViewResult
            {
                ViewName = "_DetailsPartial",
                ViewData = new ViewDataDictionary<BillDto>(ViewData, Bill)
            };
        }
    }
}