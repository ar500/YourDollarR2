using System.Collections.Generic;
using System.Text;

namespace YourDollarR2.Core.Services.Expense
{

    public interface IExpenseService
    {
        decimal AddPayment(ExpenseBase expense, decimal payment);
        decimal PayInFull(Bill bill);
    }
}
