using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.Core;
using YourDollarR2.Core.Services;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Budgets
{
    public class EditModel : PageModel
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IExpenseRepository _expenseRepository;

        [BindProperty]
        public BudgetDto Budget { get; set; }

        [BindProperty]
        public List<ExpenseDto> Expenses { get; set; }

        [BindProperty]
        public List<Guid> ReturnedExpenseIds { get; set; }

        public EditModel(IBudgetRepository budgetRepository, IExpenseRepository expenseRepository)
        {
            _budgetRepository = budgetRepository;
            _expenseRepository = expenseRepository;
        }

        public IActionResult OnGet(Guid? budgetId)
        {
            TempData["ReturnUrl"] = HttpContext.Request.Path.ToString();

            var expensesFromRepo = _expenseRepository.GetExpensesByName();

            Expenses = Mapper.Map<List<ExpenseDto>>(expensesFromRepo);

            if (budgetId.HasValue)
            {
                var budgetFromRepo = _budgetRepository.GetBudgetById(budgetId.Value);
                Budget = Mapper.Map<BudgetDto>(budgetFromRepo);
            }
            else
            {
                Budget = new BudgetDto();
            }

            if (Budget == null)
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

            if (ReturnedExpenseIds.Count != 0)
            {
                foreach (var expenseId in ReturnedExpenseIds)
                {
                    _budgetRepository.AttachExpense(Budget.Id, expenseId);
                }
            }

            var budgetFromUser = Mapper.Map<Budget>(Budget);

            if (Budget.Id == Guid.Empty)
            {
                _budgetRepository.AddBudget(budgetFromUser);
            }
            else
            {
                _budgetRepository.UpdateBudget(budgetFromUser);
            }

            if (!_budgetRepository.SaveChanges())
            {
                return RedirectToPage("../Error");
            }

            TempData["Message"] = "The Budget was saved.";

            return RedirectToPage("./Detail", new {budgetId = Budget.Id});
        }
    }
}