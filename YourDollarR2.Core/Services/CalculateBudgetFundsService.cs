using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YourDollarR2.Core.Services
{
    public class CalculateBudgetFundsService : ICalculateBudgetFundsService
    {
        public decimal CalculateAllotedFunds(IEnumerable<FundsInCategory> categoryGroups)
        {
            if (!categoryGroups.Any() || categoryGroups == null)
            {
                return 0;
            }

            return categoryGroups
                   .Select(g => g.TotalFunds)
                   .Sum();
        }

        public decimal CalculateUnallocateFunds(decimal allocatedFunds, decimal allotedFunds)
        {
            
            if(allotedFunds >= allocatedFunds)
            {
                return 0;
            }

            return allotedFunds - allocatedFunds;
        }
    }
}
