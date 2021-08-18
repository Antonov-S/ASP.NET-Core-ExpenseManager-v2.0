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
        public DbSet<ApplicationUser> ApplicationUsers { get; init; }
        public DbSet<Credit> Credits { get; init; }
        public DbSet<InstallmentLoan> InstallmentLoans { get; init; }
        public DbSet<Saving> Savings { get; init; }
        public DbSet<ContributionToSaving> ContributionToSavings { get; init; }

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

            builder
                .Entity<Expense>()
                .HasOne(c => c.User)
                .WithMany(c => c.Expenses)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Income>()
                .HasOne(c => c.User)
                .WithMany(c => c.Incomes)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Credit>()
                .HasOne(c => c.User)
                .WithMany(c => c.Credits)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<InstallmentLoan>()
                .HasOne(c => c.Credit)
                .WithMany(c => c.InstallmentLoans)
                .HasForeignKey(c => c.CreditId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ApplicationUser>()
                .HasOne(c => c.Saving);

            builder
                .Entity<ContributionToSaving>()
                .HasOne(c => c.Saving)
                .WithMany(c => c.ContributionToSavings)
                .HasForeignKey(c => c.SavingId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
