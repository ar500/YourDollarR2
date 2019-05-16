using System;
using System.Collections.Generic;
using YourDollarR2.Core;

namespace YourDollarR2.DataAccess.Repositories
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> GetExpensesByName(string shortName = null);
        Expense GetExpenseById(Guid expenseId);
        Expense UpdateExpense(Expense expense);
        Expense AddExpense(Expense expense);
        Expense DeleteExpense(Guid expenseId);
        Expense AttachCategory(Guid expenseId, Guid categoryId);
        Expense RemoveCategory(Guid expenseId);
        bool SaveChanges();
    }
}
