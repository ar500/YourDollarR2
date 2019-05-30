using System.Collections.Generic;
using System.Text;

namespace YourDollarR2.Core.Services
{
    
    public interface IExpenseService
    {
        decimal AddPayment(ExpenseBase expense, decimal payment);
        decimal PayInFull(Bill bill);
    }
}
