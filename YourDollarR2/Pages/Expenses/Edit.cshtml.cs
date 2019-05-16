using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using YourDollarR2.Core;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class EditModel : PageModel
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBudgetCategoryRepository _budgetCategoryRepository;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public ExpenseDto Expense { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> Categories { get; set; }

        public EditModel(IExpenseRepository expenseRepository, IBudgetCategoryRepository budgetCategoryRepository, 
            IHtmlHelper htmlHelper)
        {
            _expenseRepository = expenseRepository;
            _budgetCategoryRepository = budgetCategoryRepository;
            _htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(Guid? expenseId)
        {
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
                _expenseRepository.AddExpense(expenseFromUser);
            }
            else
            {
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