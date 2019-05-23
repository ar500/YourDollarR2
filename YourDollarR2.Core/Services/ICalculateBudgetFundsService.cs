using System.Collections.Generic;

namespace YourDollarR2.Core.Services
{
    public interface ICalculateBudgetFundsService
    {
        decimal CalculateAllotedFunds(IEnumerable<FundsInCategory> categoryGroups);
        decimal CalculateUnallocateFunds(decimal allocatedFunds, decimal allotedFunds);
    }
}