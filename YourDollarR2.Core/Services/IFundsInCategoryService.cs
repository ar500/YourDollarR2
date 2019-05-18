using System.Collections.Generic;

namespace YourDollarR2.Core.Services
{
    public interface IFundsInCategoryService
    {
        IEnumerable<FundsInCategory> GroupExpensesByCat(IEnumerable<Expense> expenses);
    }
}