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
    public class ListModel : PageModel
    {
        private readonly IBudgetRepository _budgetRepository;

        public IEnumerable<Budget> Budgets { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void OnGet()
        {
            Budgets = _budgetRepository.GetBudgetsByName(SearchTerm);
        }
    }
}