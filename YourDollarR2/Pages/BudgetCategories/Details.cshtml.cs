using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class DetailsModel : PageModel
    {
        private readonly YourDollarContext _context;
        private readonly ILogger _logger;

        public DetailsModel(YourDollarContext context, ILogger<DetailsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public BudgetCategoryDto BudgetCategory { get; set; } = new BudgetCategoryDto();
        
        public async Task<IActionResult> OnGetLoadDetailsPartial(Guid? id)
        {
            Debug.WriteLine(id.Value);
            if (!id.HasValue)
            {
                return null;
            }

            var categoryFromRepo = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id.Value);
            if (categoryFromRepo != null)
            {
                BudgetCategory = Mapper.Map<BudgetCategoryDto>(categoryFromRepo);

                return new PartialViewResult
                {
                    ViewName = "_DetailsPartial",
                    ViewData = new ViewDataDictionary<BudgetCategoryDto>(ViewData, BudgetCategory)
                };
            }

            return null;
        }
    }
}
