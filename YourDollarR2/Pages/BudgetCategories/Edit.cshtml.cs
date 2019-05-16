using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YourDollarR2.Core;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class EditModel : PageModel
    {
        private readonly IBudgetCategoryRepository _categoryRepository;

        [BindProperty]
        public BudgetCategoryDto BudgetCategory { get; set; }

        public EditModel(IBudgetCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult OnGet(Guid? categoryId)
        {
            if (categoryId.HasValue)
            {
                var category = _categoryRepository.GetCategoryById(categoryId.Value);
                BudgetCategory = Mapper.Map<BudgetCategoryDto>(category);
            }
            else
            {
                BudgetCategory = new BudgetCategoryDto();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var categoryFromUser = Mapper.Map<BudgetCategory>(BudgetCategory);

            if (BudgetCategory.Id == Guid.Empty)
            {
                _categoryRepository.AddCategory(categoryFromUser);
            }
            else
            {
                _categoryRepository.UpdateCategory(categoryFromUser);
            }

            if (!_categoryRepository.SaveChanges())
            {
                return RedirectToPage("../Error");
            }

            TempData["Message"] = "The category was saved.";

            return RedirectToPage("./Detail", new { categoryId = BudgetCategory.Id });
        }
    }
}