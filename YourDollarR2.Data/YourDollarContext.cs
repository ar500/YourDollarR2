using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;

namespace YourDollarR2.DataAccess
{
    public class YourDollarContext : DbContext
    {

        public DbSet<Budget> Budgets { get; set; }

        public DbSet<BudgetCategory> Categories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public YourDollarContext(DbContextOptions<YourDollarContext> options)
        : base(options)
        {
        }

    }
}
