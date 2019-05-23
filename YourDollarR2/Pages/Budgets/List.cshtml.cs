using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Budgets
{
    public class ListModel : PageModel
    {

        public IEnumerable<BudgetDto> Budgets { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [TempData]
        public string Message { get; set; }

        public ListModel()
        {
        }

        public void OnGet()
        {

        }
    }
}