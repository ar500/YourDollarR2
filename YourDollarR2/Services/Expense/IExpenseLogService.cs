using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace YourDollarR2.Core.Services.Expense
{
    public interface IExpenseLogService
    {
        Task<ExpenseLogEntry> LogExpense(ExpenseLogEntry expense);

        Task RemoveEntry(Guid expenseLogId);

        Task<ExpenseLogEntry> UpdateEntry(ExpenseLogEntry expense);

        Task<ExpenseLogEntry> GetEntryById(Guid expenseId, bool includeCategory = false);

        Task<IEnumerable<ExpenseLogEntry>> GetEntriesForBudget(Guid budgetId, bool includeCategory = false);

        Task<IEnumerable<ExpenseLogEntry>> GetEntriesForCategory(Guid categoryId);

        Task<IEnumerable<ExpenseLogEntry>> GetEntriesForTimeSpan(DateTime startDate, DateTime endDate, bool includeCategory = false);
    }
}
