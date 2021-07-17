namespace ExpenseManager_v2._0.Data
{
    using ExpenseManager_v2._0.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
 
    public class ExpenseManagerDbContext : IdentityDbContext
    {
 
        public ExpenseManagerDbContext(DbContextOptions<ExpenseManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; init; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; init; }
        public DbSet<Income> Incomes { get; init; }
        public DbSet<IncomeCategory> IncomeCategories { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Expense>()
                .HasOne(c => c.ExpenseCategory)
                .WithMany(c => c.Expenses)
                .HasForeignKey(c => c.ExpenseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Income>()
                .HasOne(c => c.IncomeCategory)
                .WithMany(c => c.Incomes)
                .HasForeignKey(c => c.IncomeCategorysId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
