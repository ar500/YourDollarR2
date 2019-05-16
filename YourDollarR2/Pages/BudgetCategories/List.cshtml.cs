using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class ListModel : PageModel
    {
        private readonly IBudgetCategoryRepository _categoryRepository;

        public IEnumerable<BudgetCategoryDto> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [TempData]
        public string Message { get; set; }

        public ListModel(IBudgetCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult OnGet(string shortName = null)
        {
            var categoriesFromStore = _categoryRepository.GetCategoriesByName(shortName);

            Categories = Mapper.Map<IEnumerable<BudgetCategoryDto>>(categoriesFromStore);

            return Page();
        }

    }
}