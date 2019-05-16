using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Budgets
{
    public class DetailModel : PageModel
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ILogger<DetailModel> _logger;

        public BudgetDto Budget { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailModel(IBudgetRepository budgetRepository, ILogger<DetailModel> logger)
        {
            _budgetRepository = budgetRepository;
            _logger = logger;
        }

        public IActionResult OnGet(Guid budgetId)
        {
            _logger.LogError($"Getting detail information for Id: {budgetId}");

            var budgetFromRepo = _budgetRepository.GetBudgetById(budgetId);
            Budget = Mapper.Map<BudgetDto>(budgetFromRepo);

            if (Budget == null)
            {
               return RedirectToPage("../Error");
            }

            return Page();
        }
    }
}