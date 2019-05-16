using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.DataAccess.Repositories;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class DeleteModel : PageModel
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseDto Expense { get; set; }

        [TempData] public string Message { get; set; }

        public DeleteModel(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public IActionResult OnGet(Guid expenseId)
        {
            var expenseFromRepo = _expenseRepository.GetExpenseById(expenseId);
            if (expenseFromRepo == null)
            {
                return RedirectToPage("./List");
            }

            Expense = Mapper.Map<ExpenseDto>(expenseFromRepo);

            return Page();
        }

        public IActionResult OnPost(Guid expenseId)
        {
            var expense = _expenseRepository.DeleteExpense(expenseId);
            if (expense == null)
            {
                return RedirectToPage("./List");
            }

            if (!_expenseRepository.SaveChanges())
            {
                return StatusCode(500, "The server was unable to handle your request.");
            }

            TempData["Message"] = $"{expense.ShortName} was deleted!";
            return RedirectToPage("./List");
        }
    }
}