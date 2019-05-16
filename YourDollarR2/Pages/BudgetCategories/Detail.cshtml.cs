using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class DetailModel : PageModel
    {
        private readonly IBudgetCategoryRepository _categoryRepository;

        public BudgetCategoryDto BudgetCategory { get; set; }

        [TempData]
        public string Message { get; set; }
        
        public DetailModel(IBudgetCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult OnGet(Guid categoryId)
        {
            var categoryFromRepo = _categoryRepository.GetCategoryById(categoryId);
            
            BudgetCategory = Mapper.Map<BudgetCategoryDto>(categoryFromRepo);
            if (BudgetCategory == null)
            {
                return RedirectToPage("../Error");
            }

            return Page();
        }
    }
}