using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Budgets
{
    public class DeleteModel : PageModel
    {

        public BudgetDto Budget { get; set; }

        public DeleteModel()
        {
        }

        public IActionResult OnGet(Guid budgetId)
        {
            return Page();
        }

        public IActionResult OnPost(Guid budgetId)
        {


            TempData["Message"] = $" was deleted!";
            return RedirectToPage("./List");
        }
    }
}