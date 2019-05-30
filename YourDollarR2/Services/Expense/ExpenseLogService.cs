using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YourDollarR2.DataAccess;

namespace YourDollarR2.Core.Services.Expense
{
    public class ExpenseLogService : IExpenseLogService
    {
        private readonly YourDollarContext _context;
        private readonly ILogger<ExpenseLogService> _logger;

        public ExpenseLogService(YourDollarContext context, ILogger<ExpenseLogService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ExpenseLogEntry>> GetEntriesForBudget(Guid budgetId, bool includeCategories = false)
        {
            if (includeCategories)
            {
                _logger.LogDebug($"Expense logs requested with categories for budget id: {budgetId}");
                return await _context.ExpenseEntries
                    .Include(c => c.BudgetCategory)
                    .Where( e => e.BudgetId == budgetId)
                    .ToListAsync();
            }
            else
            {
                _logger.LogDebug($"Expense logs requested without categories for budget id: {budgetId}");
                return await _context.ExpenseEntries
                    .Where(e => e.BudgetId == budgetId)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<ExpenseLogEntry>> GetEntriesForCategory(Guid categoryId)
        {
            _logger.LogDebug($"Expense logs requested for category id: {categoryId}");
            return await _context.ExpenseEntries
                .Include(e => e.BudgetCategory)
                .Where(e => e.BudgetCategory.Id == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ExpenseLogEntry>> GetEntriesForTimeSpan(DateTime startDate, DateTime endDate,
            bool includeCategories)
        {
            if (includeCategories)
            {
                _logger.LogDebug($"Expense logs requested with categories for the date range of {startDate}-{endDate}");
                return await _context.ExpenseEntries
                    .Include(e => e.BudgetCategory)
                    .Where(e => e.DatePaid >= startDate && e.DatePaid <= endDate)
                    .ToListAsync();
            }
            else
            {
                _logger.LogDebug($"Expense logs requested without categories for the date range of {startDate}-{endDate}");
                return await _context.ExpenseEntries
                    .Where(e => e.DatePaid >= startDate && e.DatePaid <= endDate)
                    .ToListAsync();
            }
        }

        public async Task<ExpenseLogEntry> GetEntryById(Guid expenseId, bool includeCategory = false)
        {
            if (includeCategory)
            {
                _logger.LogDebug($"Expense logs requested with categories expense id: {expenseId}");
                return await _context.ExpenseEntries
                    .Include(b => b.BudgetCategory)
                    .FirstOrDefaultAsync(e => e.Id == expenseId);
            }
            else
            {
                _logger.LogDebug($"Expense logs requested without categories expense id: {expenseId}");
                return await _context.ExpenseEntries.FirstOrDefaultAsync(e => e.Id == expenseId);
            }
        }

        public async Task<ExpenseLogEntry> LogExpense(ExpenseLogEntry expense)
        {
            await _context.ExpenseEntries.AddAsync(expense);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"a new expense was logged");
                return expense;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Concurrency exception.");
                throw;
            }
        }

        public async Task RemoveEntry(Guid expenseLogId)
        {
            if (EntryExists(expenseLogId))
            {
                var entry = await GetEntryById(expenseLogId);
                _context.Remove(entry);

                try
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"An expense entry was deleted from the database.");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError("Concurrency exception.");
                    throw;
                }

            }
        }

        public async Task<ExpenseLogEntry> UpdateEntry(ExpenseLogEntry expense)
        {
            if (EntryExists(expense.Id))
            {
                _context.Entry(expense).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"An expense entry was updated.");
                    return expense;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError("Concurrency exception.");
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        private bool EntryExists(Guid expenseId)
        {
            return _context.ExpenseEntries
                .Any(e => e.Id == expenseId);
        }
    }
}
