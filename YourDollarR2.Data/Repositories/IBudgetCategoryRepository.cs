using System;
using System.Collections.Generic;
using YourDollarR2.Core;

namespace YourDollarR2.DataAccess.Repositories
{
    public interface IBudgetCategoryRepository
    {
        IEnumerable<BudgetCategory> GetCategoriesByName(string shortName = null);
        BudgetCategory GetCategoryById(Guid categoryId);
        BudgetCategory UpdateCategory(BudgetCategory category);
        BudgetCategory AddCategory(BudgetCategory category);
        BudgetCategory DeleteCategory(Guid categoryId);
        IEnumerable<string> GetCategoryNames();
        bool SaveChanges();
    }
}
