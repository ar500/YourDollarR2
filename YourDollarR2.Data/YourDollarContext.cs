﻿using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;

namespace YourDollarR2.DataAccess
{
    public class YourDollarContext : DbContext
    {

        public DbSet<Budget> Budgets { get; set; }

        public DbSet<BudgetCategory> Categories { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<UnplannedExpense> UnplannedExpenses { get; set; }

        public DbSet<ExpenseLogEntry> ExpenseEntries { get; set; }

        public YourDollarContext(DbContextOptions<YourDollarContext> options)
        : base(options)
        {
        }

    }
}
