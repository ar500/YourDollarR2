using System;
using System.Collections.Generic;
using YourDollarR2.Core;

namespace YourDollarR2.DataAccess.Repositories
{
    public interface IBudgetRepository
    {
        IEnumerable<Budget> GetBudgetsByName(string shortName = null);
        Budget GetBudgetById(Guid budgetId);
        Budget UpdateBudget(Budget budget);
        Budget AddBudget(Budget budget);
        Budget DeleteBudget(Guid budgetId);
        bool SaveChanges();
    }
}
