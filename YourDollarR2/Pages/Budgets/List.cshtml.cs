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
    public class ListModel : PageModel
    {
        private readonly IBudgetRepository _budgetRepository;

        public IEnumerable<BudgetDto> Budgets { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void OnGet()
        {
            var budgetsFromRepo = _budgetRepository.GetBudgetsByName(SearchTerm);

            Budgets = Mapper.Map<IEnumerable<BudgetDto>>(budgetsFromRepo);
        }
    }
}