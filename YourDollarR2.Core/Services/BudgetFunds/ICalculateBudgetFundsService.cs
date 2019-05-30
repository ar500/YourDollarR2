using System.Collections.Generic;

namespace YourDollarR2.Core.Services.BudgetFunds
{
    public interface ICalculateBudgetFundsService
    {
        decimal CalculateAllocatedFunds(IEnumerable<FundsInCategory> categoryGroups);
        decimal CalculateUnallocateFunds(decimal allocatedFunds, decimal allotedFunds);
    }
}