using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.Core;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Budgets
{
    public class EditModel : PageModel
    {
        private readonly IBudgetRepository _budgetRepository;

        [BindProperty]
        public BudgetDto Budget { get; set; }

        public EditModel(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public IActionResult OnGet(Guid? budgetId)
        {
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