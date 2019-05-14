using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using YourDollarR2.Core;
using YourDollarR2.DataAccess.Repositories;

namespace YourDollarR2.Pages.Budgets
{
    public class DetailModel : PageModel
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ILogger<DetailModel> _logger;

        public Budget Budget { get; set; }

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
            Budget = _budgetRepository.GetBudgetById(budgetId);
            if (Budget == null)
            {
               return RedirectToPage("../Error");
            }

            return Page();
        }
    }
}