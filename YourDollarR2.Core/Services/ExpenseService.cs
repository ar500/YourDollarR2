using System;
using Microsoft.Extensions.Logging;

namespace YourDollarR2.Core.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ILogger<ExpenseService> _logger;

        public ExpenseService(ILogger<ExpenseService> logger)
        {
            _logger = logger;
        }

        public decimal AddPayment(Expense expense, decimal payment)
        {
            if (Math.Sign(payment) == -1)
            {
                return expense.AmountSpent;
            }

            try
            {
                return expense.AmountSpent += payment;
            }
            catch (OverflowException)
            {
                _logger.LogError("Overflow caught in AddPayment. There may be something fishy going on.");
                return expense.AmountSpent;
            }
        }

        public decimal PayInFull(Expense expense)
        {
            return expense.AmountPlanned;
        }
    }
}