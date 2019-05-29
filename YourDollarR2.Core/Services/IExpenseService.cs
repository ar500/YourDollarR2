using System.Collections.Generic;
using System.Text;

namespace YourDollarR2.Core.Services
{
    
    public interface IExpenseService
    {
        decimal AddPayment(Expense expense, decimal payment);
        decimal PayInFull(Expense expense);
    }
}
