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

namespace YourDollarR2.Pages.Bills
{
    public class EditModel : PageModel
    {
        private readonly YourDollarContext _context;

        public EditModel(YourDollarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BillForCreateDto Bill { get; set; }

        public async Task<IActionResult> OnGetLoadEditPartialAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            if (!BillExists(id.Value))
            {
                return NotFound();
            }

            var billFromRepo = await _context.Bills
                .Include(c => c.BudgetCategory)
                .FirstOrDefaultAsync(m => m.Id == id.Value);

            if (billFromRepo == null)
            {
                return BadRequest();
            }

            Bill = Mapper.Map<BillForCreateDto>(billFromRepo);

            await PopulateCategories();

            if (Bill == null)
            {
                return BadRequest();
            }

            return new PartialViewResult
            {
                ViewName = "_EditPartial",
                ViewData = new ViewDataDictionary<BillForCreateDto>(ViewData, Bill)
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }

            var chosenCategory = await _context.Categories.FindAsync(Bill.ReturnedCategoryId);
            if (chosenCategory == null)
            {
                return BadRequest();
            }

            var billFromUser = Mapper.Map<Bill>(Bill);

            billFromUser.BudgetCategory = chosenCategory;

            _context.Attach(billFromUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(Bill.Id))
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

        private bool BillExists(Guid id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }

        private async Task PopulateCategories()
        {
            Bill.Categories = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = Bill.BudgetCategory.Id.ToString(),
                    Text = Bill.BudgetCategory.ShortName,
                    Selected = true
                }
            };

            var categoriesFromRepo = await _context.Categories
                .Where(c => c.Id != Bill.BudgetCategory.Id)
                .ToListAsync();

            foreach (var category in categoriesFromRepo)
            {
                Bill.Categories.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.ShortName
                });
            }
        }
    }
}
