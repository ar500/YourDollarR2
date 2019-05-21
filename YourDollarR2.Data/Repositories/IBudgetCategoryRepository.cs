using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourDollarR2.Core;

namespace YourDollarR2.DataAccess.Repositories
{
    public interface IBudgetCategoryRepository
    {
        IEnumerable<BudgetCategory> GetCategoriesByName(string shortName = null);
        Task<BudgetCategory> GetCategoryById(Guid categoryId);
        BudgetCategory UpdateCategory(BudgetCategory category);
        Task<BudgetCategory> AddCategory(BudgetCategory category);
        Task<BudgetCategory> DeleteCategory(Guid categoryId);
        Task<bool> SaveChanges();
    }
}
