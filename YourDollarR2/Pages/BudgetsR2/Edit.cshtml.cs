using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using TrackableEntities.Common.Core;
using TrackableEntities.EF.Core;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.BudgetsR2
{
    public class EditModel : PageModel
    {
        private readonly YourDollarContext _context;

        public EditModel(YourDollarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BudgetForCreateOrEditDto Budget { get; set; }
        
        public IActionResult OnGetAsync()
        {
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBudget = await _context.Budgets
                .Where(b => b.Id == Budget.Id)
                .Include(p => p.Expenses)
                .ThenInclude(e => e.BudgetCategory)
                .FirstOrDefaultAsync();

            Mapper.Map(Budget, existingBudget);

            existingBudget.Expenses = await _context.Expenses
                .Include(e => e.BudgetCategory)
                .Where(e => Budget.ReturnedExpenseIds.Contains(e.Id))
                .ToListAsync();


            _context.Entry(existingBudget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(Budget.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetLoadEditPartialAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return RedirectToPage("./Index");
            }

            if (!BudgetExists(id.Value))
            {
                return BadRequest();
            }

            var budgetFromRepo = await _context.Budgets
                .Include(e => e.Expenses)
                .ThenInclude(b => b.BudgetCategory)
                .FirstOrDefaultAsync(b => b.Id == id);

            Budget = Mapper.Map<BudgetForCreateOrEditDto>(budgetFromRepo);

            await PopulateExpenses();

            return new PartialViewResult
            {
                ViewName = "_EditPartial",
                ViewData = new ViewDataDictionary<BudgetForCreateOrEditDto>(ViewData, Budget)
            };
        }

        private bool BudgetExists(Guid id)
        {
            return _context.Budgets.Any(e => e.Id == id);
        }

        private async Task PopulateExpenses()
        {
            var expensesFromRepo = await _context.Expenses.ToListAsync();
            var mappedExpenses = Mapper.Map<IEnumerable<ExpenseDto>>(expensesFromRepo);

            var selectedIds = new List<Guid>();

            foreach(var attachedExpense in Budget.Expenses)
            {
                selectedIds.Add(attachedExpense.Id);
            }

            Budget.ExpenseMultiSelectList = new MultiSelectList(mappedExpenses, "Id", "ShortName");

            Budget.ReturnedExpenseIds = selectedIds;
        }
    }
}
