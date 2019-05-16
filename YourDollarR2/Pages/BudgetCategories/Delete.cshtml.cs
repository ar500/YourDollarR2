using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.Core;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetCategories
{
    public class DeleteModel : PageModel
    {
        private readonly IBudgetCategoryRepository _categoryRepository;

        public BudgetCategoryDto BudgetCategory { get; set; }

        [TempData]
        public string Message { get; set; }

        public DeleteModel(IBudgetCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult OnGet(Guid categoryId)
        {
            var categoryFromRepo = _categoryRepository.GetCategoryById(categoryId);
            if (categoryFromRepo == null)
            {
                return RedirectToPage("./List");
            }

            BudgetCategory = Mapper.Map<BudgetCategoryDto>(categoryFromRepo);

            return Page();
        }

        public IActionResult OnPost(Guid categoryId)
        {
            var budgetCategory = _categoryRepository.DeleteCategory(categoryId);
            if (budgetCategory == null)
            {
                return RedirectToPage("./List");
            }

            if (!_categoryRepository.SaveChanges())
            {
                return StatusCode(500, "The server was unable to handle your request.");
            }

            TempData["Message"] = $"{budgetCategory.ShortName} was deleted!";
            return RedirectToPage("./List");
        }
    }
}