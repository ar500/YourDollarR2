using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetsR2
{
    public class DashboardModel : PageModel
    {
        private readonly YourDollarContext _context;

        public BudgetDto Budget { get; set; }

        public string SerializedCategories { get; set; }

        public DashboardModel(YourDollarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(Guid? budgetId)
        {
            if (!budgetId.HasValue)
            {
                return RedirectToPage("./Index");
            }

            if (!await BudgetExists(budgetId.Value))
            {
                return RedirectToPage("./Index");
            }

            var budgetFromRepo = await _context.Budgets
                .Include(b => b.Expenses)
                .ThenInclude(e => e.BudgetCategory)
                .FirstOrDefaultAsync(b => b.Id == budgetId.Value);

            if (budgetFromRepo != null)
            {
                Budget = Mapper.Map<BudgetDto>(budgetFromRepo);
            }

            SerializedCategories = JsonConvert.SerializeObject(Budget.CategoryGroups);

            return Page();
        }

        private async Task<bool> BudgetExists(Guid id)
        {
            return await _context.Budgets.AnyAsync(b => b.Id == id);
        }
    }
}