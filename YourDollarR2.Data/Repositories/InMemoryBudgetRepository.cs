using System;
using System.Collections.Generic;
using System.Linq;
using YourDollarR2.Core;

namespace YourDollarR2.DataAccess.Repositories
{
    public class InMemoryBudgetRepository : IBudgetRepository
    {
        private List<Budget> _inMemoryBudgets;

        public InMemoryBudgetRepository()
        {
            _inMemoryBudgets = new List<Budget>
            {
                new Budget
                {
                    Id = Guid.Parse("80000002-0002-fd00-b63f-84710c7967bb"),
                    ShortName = "Jason's Personal Budget",
                    CycleStartDate = DateTime.Parse("05/15/2019"),
                    CycleEndDate = DateTime.Parse("05/25/2019"),
                    Description = "My personal budget.",
                    Funds = 2108m,
                    OwnerEmail = "bradshaw15r@gmail.com"
                },
                new Budget
                {
                    Id = Guid.Parse("8000003b-0006-fd00-b63f-84710c7967bb"),
                    ShortName = "Jason's Work Budget",
                    CycleStartDate = DateTime.Parse("05/15/2019"),
                    CycleEndDate = DateTime.Parse("05/25/2019"),
                    Description = "My work budget.",
                    Funds = 2108m,
                    OwnerEmail = "bradshaw15r@mywork.com"
                },
                new Budget
                {
                    Id = Guid.Parse("80000039-0005-fe00-b63f-84710c7967bb"),
                    ShortName = "Kanisha's Budget",
                    CycleStartDate = DateTime.Parse("05/15/2019"),
                    CycleEndDate = DateTime.Parse("05/25/2019"),
                    Description = "I am the queen!",
                    Funds = 2108m,
                    OwnerEmail = "Kanisha@mywork.com"
                }
            };
        }

        public Budget AddBudget(Budget budget)
        {
            _inMemoryBudgets.Add(budget);
            return budget;
        }

        public Budget DeleteBudget(Guid budgetId)
        {
            var budget = GetBudgetById(budgetId);
            if (budget != null)
            {
                _inMemoryBudgets.Remove(budget);
            }

            return budget;
        }

        public Budget GetBudgetById(Guid budgetId)
        {
            return _inMemoryBudgets.FirstOrDefault(b => b.Id == budgetId);
        }

        public IEnumerable<Budget> GetBudgetsByName(string shortName = null)
        {
            return from b in _inMemoryBudgets
                where string.IsNullOrWhiteSpace(shortName) || b.ShortName.Contains(shortName)
                orderby b.ShortName
                select b;
        }

        public bool SaveChanges()
        {
            return true;
        }

        public Budget UpdateBudget(Budget budget)
        {
            var budgetFromStore = GetBudgetById(budget.Id);
            if (budgetFromStore != null)
            {
                budgetFromStore.CycleStartDate = budget.CycleStartDate;
                budgetFromStore.CycleEndDate = budget.CycleEndDate;
                budgetFromStore.ShortName = budget.ShortName;
                budgetFromStore.Description = budget.Description;
                budgetFromStore.Funds = budget.Funds;
            }

            return budget;
        }
    }
}
