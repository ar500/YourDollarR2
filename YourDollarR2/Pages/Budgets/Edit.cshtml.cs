using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.Core;
using YourDollarR2.DataAccess.Repositories;

namespace YourDollarR2.Pages.Budgets
{
    public class EditModel : PageModel
    {
        private readonly IBudgetRepository _budgetRepository;

        [BindProperty]
        public Budget Budget { get; set; }

        public EditModel(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public IActionResult OnGet(Guid? budgetId)
        {
            if (budgetId.HasValue)
            {
                Budget = _budgetRepository.GetBudgetById(budgetId.Value);
            }
            else
            {
                Budget = new Budget();
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

            if (Budget.Id == Guid.Empty)
            {
                _budgetRepository.AddBudget(Budget);
            }
            else
            {
                _budgetRepository.UpdateBudget(Budget);
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