using System.Collections.Generic;

namespace YourDollarR2.Core.Services.BudgetFunds
{
    public interface IFundsInCategoryService
    {
        IEnumerable<FundsInCategory> GroupExpensesByCat(IEnumerable<Bill> bills);
    }
}