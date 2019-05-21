using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class IndexModel : PageModel
    {
        private readonly YourDollarContext _context;

        public IList<BudgetCategoryDto> BudgetCategories { get; set; }

        public BudgetCategoryDto BudgetCategory { get; set; } = new BudgetCategoryDto();

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

            var categoriesFromRepo = await _context.Categories.ToListAsync();

            var filteredCategories = from c in categoriesFromRepo
                where string.IsNullOrWhiteSpace(SearchTerm) || c.ShortName.Contains(SearchTerm)
                select c;

            BudgetCategories = Mapper.Map<IList<BudgetCategoryDto>>(filteredCategories);
        }
    }
}
