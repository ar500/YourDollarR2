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

        public IActionResult OnGet()
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
                .Include(p => p.Bills)
                .ThenInclude(e => e.BudgetCategory)
                .FirstOrDefaultAsync();

            Mapper.Map(Budget, existingBudget);

            existingBudget.Bills = await _context.Bills
                .Include(e => e.BudgetCategory)
                .Where(e => Budget.ReturnedBillIds.Contains(e.Id))
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
                .Include(e => e.Bills)
                .ThenInclude(b => b.BudgetCategory)
                .FirstOrDefaultAsync(b => b.Id == id);



            Budget = Mapper.Map<BudgetForCreateOrEditDto>(budgetFromRepo);
            Budget.Bill = new BillForCreateDto();

            var categoriesFromRepo = await _context.Categories.ToListAsync();

            foreach (var budgetCategory in categoriesFromRepo)
            {
                Budget.Bill.Categories.Add(
                    new SelectListItem
                    {
                        Value = budgetCategory.Id.ToString(),
                        Text = budgetCategory.ShortName
                    }
                );
            }

            await PopulateBills();

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

        private async Task PopulateBills()
        {
            var BillsFromRepo = await _context.Bills
                .Where(e => e.BudgetId == null || e.BudgetId == Budget.Id)
                .ToListAsync();
            var mappedBills = Mapper.Map<IEnumerable<BillDto>>(BillsFromRepo);

            var selectedIds = new List<Guid>();

            foreach (var attachedBill in Budget.Bills)
            {
                selectedIds.Add(attachedBill.Id);
            }

            Budget.BillsMultiSelectList = new MultiSelectList(mappedBills, "Id", "ShortName");

            Budget.ReturnedBillIds = selectedIds;
        }
    }
}
