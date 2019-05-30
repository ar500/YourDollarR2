using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Bills
{
    public class IndexModel : PageModel
    {
        private readonly YourDollarContext _context;

        public IList<BillDto> Bills { get; set; }

        public IList<BudgetCategoryDto> Categories { get; set; }

        public BillForCreateDto Bill { get; set; } = new BillForCreateDto();

        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        public IndexModel(YourDollarContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var billsFromRepo = await _context.Bills
                .Include(c => c.BudgetCategory)
                .ToListAsync();
            Bills = Mapper.Map<IList<BillDto>>(billsFromRepo);
        }

        public async Task<IActionResult> OnGetCreateNewPartial()
        {
            var categoriesFromRepo = await _context.Categories.ToListAsync();
            var categoriesSelectList = new List<SelectListItem>();

            foreach (var category in categoriesFromRepo)
            {
                categoriesSelectList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.ShortName
                });
            }

            Bill.Categories = categoriesSelectList;

            return new PartialViewResult
            {
                ViewName = "_CreatePartial",
                ViewData = new ViewDataDictionary<BillForCreateDto>(ViewData, Bill)
            };
        }
    }
}
