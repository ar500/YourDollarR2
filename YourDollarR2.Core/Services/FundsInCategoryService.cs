using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace YourDollarR2.Core.Services
{
    public class FundsInCategoryService : IFundsInCategoryService
    {
        public IEnumerable<FundsInCategory> GroupExpensesByCat(IEnumerable<Expense> expenses)
        {
            if (expenses.Count() == 0)
            {
                return null;
            }

            return expenses
                .GroupBy(e => e.BudgetCategory)
                .Select(g => new FundsInCategory
                {
                    Category = g.FirstOrDefault().BudgetCategory.ShortName,
                    TotalFunds = g.Sum(e => e.PayoutAmount)
                }).ToList();
        }
    }
}
