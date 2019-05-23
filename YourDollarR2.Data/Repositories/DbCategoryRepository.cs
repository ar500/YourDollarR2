using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;
using System.Linq;

namespace YourDollarR2.DataAccess.Repositories
{
    public class DbCategoryRepository : IBudgetCategoryRepository
    {
        private readonly YourDollarContext _context;

        public DbCategoryRepository(YourDollarContext context)
        {
            _context = context;
        }

        public async Task<BudgetCategory> AddCategory(BudgetCategory category)
        {
            await _context.Categories.AddAsync(category);
            return category;
        }

        public async Task<BudgetCategory> DeleteCategory(Guid categoryId)
        {
            var category = await GetCategoryById(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                return category;
            }
            return null;
        }

        public IEnumerable<BudgetCategory> GetCategoriesByName(string shortName = null)
        {
           return  from c in _context.Categories.AsNoTracking()
                where c.ShortName.Contains(shortName) || string.IsNullOrWhiteSpace(shortName)
                orderby c.ShortName
                select c;
        }

        public async Task<BudgetCategory> GetCategoryById(Guid categoryId)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch(DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public BudgetCategory UpdateCategory(BudgetCategory category)
        {
            var targetCategory = _context.Categories.Attach(category);
            targetCategory.State = EntityState.Modified;
            return category;
        }
    }
}
