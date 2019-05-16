using System;
using System.Collections.Generic;
using System.Linq;
using YourDollarR2.Core;

namespace YourDollarR2.DataAccess.Repositories
{
    public class InMemoryExpenseRepository : IExpenseRepository
    {
        private readonly IBudgetCategoryRepository _categoryRepository;
        private List<Expense> _expenses;

        public InMemoryExpenseRepository(IBudgetCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            _expenses = new List<Expense>
            {
                new Expense
                {
                    Id = Guid.Parse("de4dacb4-ffeb-4b9c-9a0d-f49b4355ebb6"),
                    ShortName = "Light Bill",
                    Description = "If I don't pay 'em, I won't be able to see...",
                    CompanyName = "ACBLight Co-op",
                    PayoutAccountNumber = "123444-44fg33",
                    PayoutAmount = 200m,
                    BudgetCategory = _categoryRepository.GetCategoriesByName("Utilities").FirstOrDefault()
                },
                new Expense
                {
                    Id = Guid.Parse("e5b0db9e-803b-4ff4-a357-e04731adef33"),
                    ShortName = "Groceries",
                    Description = "I Like food",
                    PayoutAmount = 250m
                },
                new Expense
                {
                    Id = Guid.Parse("3bd010e2-a2ed-4e81-ba20-a25a234f13fa"),
                    ShortName = "Bus Fee",
                    Description = "I gotta get there somehow",
                    CompanyName = "Greyhound",
                    PayoutAmount = 20m,
                    BudgetCategory = _categoryRepository.GetCategoriesByName("Transportation").FirstOrDefault()
                },
                new Expense
                {
                    Id = Guid.Parse("de6be2cc-7695-41b3-af7e-299b0a1e9009"),
                    ShortName = "New style swag",
                    Description = "All da bitches",
                    PayoutAmount = 100m,
                    BudgetCategory = _categoryRepository.GetCategoriesByName("Clothing").FirstOrDefault()
                }
            };
        }

        public Expense AddExpense(Expense expense)
        {
           _expenses.Add(expense);
           return expense;
        }

        public Expense AttachCategory(Guid expenseId, Guid categoryId)
        {
            var categoryToAttach = _categoryRepository.GetCategoryById(categoryId);
            if (categoryToAttach == null)
            {
                return null;
            }

            var expenseToUpdate = GetExpenseById(expenseId);
            if (expenseToUpdate == null)
            {
                return null;
            }

            expenseToUpdate.BudgetCategory = categoryToAttach;

            return expenseToUpdate;
        }

        public Expense DeleteBudget(Guid expenseId)
        {
            var expenseToDelete = GetExpenseById(expenseId);
            if (expenseToDelete != null)
            {
                _expenses.Remove(expenseToDelete);
                return expenseToDelete;
            }

            return null;
        }

        public Expense GetExpenseById(Guid expenseId)
        {
            return _expenses.FirstOrDefault(e => e.Id == expenseId);
        }

        public IEnumerable<Expense> GetExpensesByName(string shortName = null)
        {
            return from e in _expenses
                where string.IsNullOrWhiteSpace(shortName) || e.ShortName.Contains(shortName)
                orderby e.ShortName
                select e;
        }

        public Expense RemoveCategory(Guid expenseId)
        {
            var expenseToUpdate = GetExpenseById(expenseId);
            if (expenseToUpdate == null)
            {
                return null;
            }

            expenseToUpdate.BudgetCategory = null;

            return expenseToUpdate;
        }

        public bool SaveChanges()
        {
            return true;
        }

        public Expense UpdateExpense(Expense expense)
        {
            var expenseToUpdate = GetExpenseById(expense.Id);
            if (expenseToUpdate != null)
            {
                expenseToUpdate.ShortName = expense.ShortName;
                expenseToUpdate.Description = expense.Description;
                expenseToUpdate.CompanyName = expense.CompanyName;
                expenseToUpdate.PayoutAccountNumber = expense.PayoutAccountNumber;
                expenseToUpdate.PayoutAmount = expense.PayoutAmount;
                expenseToUpdate.BudgetCategory = expense.BudgetCategory;
            }

            return expenseToUpdate;
        }

        
    }
}
