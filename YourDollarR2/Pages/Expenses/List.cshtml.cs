using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class ListModel : PageModel
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBudgetCategoryRepository _categoryRepository;

        public IEnumerable<ExpenseDto> Expenses { get; set; }

        [BindProperty]
        public IEnumerable<BudgetCategoryDto> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [TempData]
        public string Message { get; set; }

        public ListModel(IExpenseRepository expenseRepository, IBudgetCategoryRepository categoryRepository)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
        }

        public void OnGet()
        {
            // TODO: Make List searches case insensitive.
            var expensesFromRepo = _expenseRepository.GetExpensesByName(SearchTerm);

            Expenses = Mapper.Map<IEnumerable<ExpenseDto>>(expensesFromRepo);

            var categoriesFromRepo = _categoryRepository.GetCategoriesByName();

            Categories = Mapper.Map<IEnumerable<BudgetCategoryDto>>(categoriesFromRepo);
        }
    }
}