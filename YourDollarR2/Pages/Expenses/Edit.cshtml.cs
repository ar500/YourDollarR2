using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2.Pages.Expenses
{
    public class EditModel : PageModel
    {
        private readonly YourDollarContext _context;

        public EditModel(YourDollarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExpenseForCreateDto Expense { get; set; }

        public async Task<IActionResult> OnGetLoadEditPartialAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            if (!ExpenseExists(id.Value))
            {
                return NotFound();
            }

            var expenseFromRepo = await _context.Expenses
                .Include(c => c.BudgetCategory)
                .FirstOrDefaultAsync(m => m.Id == id.Value);

            if (expenseFromRepo == null)
            {
                return BadRequest();
            }

            Expense = Mapper.Map<ExpenseForCreateDto>(expenseFromRepo);

            await PopulateCategories();

            if (Expense == null)
            {
                return BadRequest();
            }

            //return Partial("_EditPartial");

            return new PartialViewResult
            {
                ViewName = "_EditPartial",
                ViewData = new ViewDataDictionary<ExpenseForCreateDto>(ViewData, Expense)
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }

            var chosenCategory = await _context.Categories.FindAsync(Expense.ReturnedCategoryId);
            if (chosenCategory == null)
            {
                return BadRequest();
            }

            var expenseFromUser = Mapper.Map<Expense>(Expense);

            expenseFromUser.BudgetCategory = chosenCategory;

            _context.Attach(expenseFromUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(Expense.Id))
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

        private bool ExpenseExists(Guid id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }

        private async Task PopulateCategories()
        {
            Expense.Categories = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = Expense.BudgetCategory.Id.ToString(),
                    Text = Expense.BudgetCategory.ShortName,
                    Selected = true
                }
            };

            var categoriesFromRepo = await _context.Categories
                .Where(c => c.Id != Expense.BudgetCategory.Id)
                .ToListAsync();

            foreach (var category in categoriesFromRepo)
            {
                Expense.Categories.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.ShortName
                });
            }
        }
    }
}
