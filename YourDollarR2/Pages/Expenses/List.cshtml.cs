using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class ListModel : PageModel
    {
        private readonly IExpenseRepository _expenseRepository;

        public IEnumerable<ExpenseDto> Expenses { get; set; }

        [BindProperty]
        public string SearchTerm { get; set; }

        [TempData]
        public string Message { get; set; }

        public ListModel(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public void OnGet()
        {
            var expensesFromRepo = _expenseRepository.GetExpensesByName(SearchTerm);

            Expenses = Mapper.Map<IEnumerable<ExpenseDto>>(expensesFromRepo);
        }
    }
}