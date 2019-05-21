using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using YourDollarR2.Core;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class EditModel : PageModel
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBudgetCategoryRepository _budgetCategoryRepository;

        [BindProperty]
        public ExpenseDto Expense { get; set; }

        [BindProperty]
        public List<BudgetCategoryDto> Categories { get; set; }

        [BindProperty]
        public Guid ReturnedCategoryId { get; set; }

        public EditModel(IExpenseRepository expenseRepository, IBudgetCategoryRepository budgetCategoryRepository)
        {
            _expenseRepository = expenseRepository;
            _budgetCategoryRepository = budgetCategoryRepository;
        }

        public IActionResult OnGet(Guid? expenseId)
        {
            var categoriesFromRepo = _budgetCategoryRepository.GetCategoriesByName();

            Categories = Mapper.Map<List<BudgetCategoryDto>>(categoriesFromRepo);

            Categories.Insert(0, new BudgetCategoryDto {Id = Guid.NewGuid(), ShortName = "Select a Category"});

            if (expenseId.HasValue)
            {
                var expenseFromRepo = _expenseRepository.GetExpenseById(expenseId.Value);
                Expense = Mapper.Map<ExpenseDto>(expenseFromRepo);
            }
            else
            {
                Expense = new ExpenseDto();
            }

            if (Expense == null)
            {
                return RedirectToPage("../Error");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var expenseFromUser = Mapper.Map<Expense>(Expense);

            if (Expense.Id == Guid.Empty)
            {
                var category = _budgetCategoryRepository.GetCategoryById(ReturnedCategoryId).Result;
                if (category != null)
                {
                    expenseFromUser.BudgetCategory = category;
                }

                _expenseRepository.AddExpense(expenseFromUser);
            }
            else
            {
                var category = _budgetCategoryRepository.GetCategoryById(ReturnedCategoryId).Result;
                if (category != null)
                {
                    expenseFromUser.BudgetCategory = category;
                }
                _expenseRepository.UpdateExpense(expenseFromUser);
            }

            if (!_expenseRepository.SaveChanges())
            {
                return RedirectToPage("../Error");
            }

            TempData["Message"] = "The Expense was saved.";

            return RedirectToPage("./Detail", new { expenseId = Expense.Id });
        }
    }
}