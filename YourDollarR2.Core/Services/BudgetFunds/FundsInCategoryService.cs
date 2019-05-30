using System.Collections.Generic;
using System.Linq;

namespace YourDollarR2.Core.Services.BudgetFunds
{
    public class FundsInCategoryService : IFundsInCategoryService
    {
        public IEnumerable<FundsInCategory> GroupExpensesByCat(IEnumerable<Bill> bills)
        {
            if (!bills.Any() || bills == null)
            {
                return null;
            }

            var groupedCategories = bills
                .GroupBy(e => e.BudgetCategory)
                .Where(g => g.Key != null)
                .Select(g => new FundsInCategory
                {
                    Category = g.FirstOrDefault().BudgetCategory.ShortName,
                    TotalFunds = g.Sum(e => e.AmountPlanned)
                }).ToList();

            return groupedCategories;
        }
    }
}
