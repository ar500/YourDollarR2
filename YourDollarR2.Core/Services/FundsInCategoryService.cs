using System.Collections.Generic;
using System.Linq;

namespace YourDollarR2.Core.Services
{
    public class FundsInCategoryService : IFundsInCategoryService
    {
        public IEnumerable<FundsInCategory> GroupExpensesByCat(IEnumerable<Expense> expenses)
        {
            if (!expenses.Any() || expenses == null)
            {
                return null;
            }

            return expenses
                .GroupBy(e => e.BudgetCategory)
                .Where(g => g.Key != null)
                .Select(g => new FundsInCategory
                {
                    Category = g.FirstOrDefault().BudgetCategory.ShortName,
                    TotalFunds = g.Sum(e => e.PayoutAmount)
                }).ToList();
        }
    }
}
