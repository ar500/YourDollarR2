using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Budgets
{
    public class DeleteModel : PageModel
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetDto Budget { get; set; }

        public DeleteModel(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public IActionResult OnGet(Guid budgetId)
        {
            var budgetFromRepo = _budgetRepository.GetBudgetById(budgetId);
            if(budgetFromRepo != null)
            {
                Budget = Mapper.Map<BudgetDto>(budgetFromRepo);
            }
            else
            {
                return RedirectToPage("./List");
            }

            return Page();
        }

        public IActionResult OnPost(Guid budgetId)
        {
            var budget = _budgetRepository.DeleteBudget(budgetId);

            if (budget == null)
            {
                return RedirectToPage("./List");
            }

            if (!_budgetRepository.SaveChanges())
            {
                return StatusCode(500, "The server was unable to handle your request.");
            }

            TempData["Message"] = $"{budget.ShortName} was deleted!";
            return RedirectToPage("./List");
        }
    }
}