using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class DetailsModel : PageModel
    {
        private readonly YourDollarR2.DataAccess.YourDollarContext _context;

        public DetailsModel(YourDollarR2.DataAccess.YourDollarContext context)
        {
            _context = context;
        }

        public BudgetCategoryDto BudgetCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryFromRepo = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            BudgetCategory = Mapper.Map<BudgetCategoryDto>(categoryFromRepo);

            if (BudgetCategory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
