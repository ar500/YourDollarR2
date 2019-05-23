using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.Core;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Budgets
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public BudgetDto Budget { get; set; }

        [BindProperty]
        public List<ExpenseDto> Expenses { get; set; }

        [BindProperty]
        public List<Guid> ReturnedExpenseIds { get; set; }

        public EditModel()
        {
        }

        public IActionResult OnGet(Guid? budgetId)
        {
            TempData["ReturnUrl"] = HttpContext.Request.Path.ToString();



            if (budgetId.HasValue)
            {
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

            if (ReturnedExpenseIds.Count != 0)
            {
                foreach (var expenseId in ReturnedExpenseIds)
                {
                }
            }

            var budgetFromUser = Mapper.Map<Budget>(Budget);

            if (Budget.Id == Guid.Empty)
            {
            }
            else
            {
            }

            
            TempData["Message"] = "The Budget was saved.";

            return RedirectToPage("./Detail", new {budgetId = Budget.Id});
        }
    }
}