using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class DetailModel : PageModel
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseDto Expense { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailModel(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public IActionResult OnGet(Guid expenseId)
        {
            var expenseFromRepo = _expenseRepository.GetExpenseById(expenseId);
            if (expenseFromRepo == null)
            {
                return RedirectToPage("../NotFound");
            }

            Expense = Mapper.Map<ExpenseDto>(expenseFromRepo);

            if (Expense == null)
            {
                return RedirectToPage("../Error");
            }

            return Page();
        }
    }
}