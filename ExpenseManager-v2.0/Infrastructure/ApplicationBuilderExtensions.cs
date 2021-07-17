namespace ExpenseManager_v2._0.Infrastructure
{
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ExpenseManagerDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(ExpenseManagerDbContext data)
        {
            if (data.ExpenseCategories.Any())
            {
                return;
            }

            if (data.IncomeCategories.Any())
            {
                return;
            }

            data.ExpenseCategories.AddRange(new[]
            {
                new ExpenseCategory { Name = "Utility bills"},
                new ExpenseCategory { Name = "Housing"},
                new ExpenseCategory { Name = "Food and supplies"},
                new ExpenseCategory { Name = "Transport"},
                new ExpenseCategory { Name = "Automobile"},
                new ExpenseCategory { Name = "Kids"},
                new ExpenseCategory { Name = "Clothing and footwear"},
                new ExpenseCategory { Name = "Personal"},
                new ExpenseCategory { Name = "Cigarettes and alcohol"},
                new ExpenseCategory { Name = "Entertainment"},
                new ExpenseCategory { Name = "Eating out"},
                new ExpenseCategory { Name = "Education"},
                new ExpenseCategory { Name = "Gifts"},
                new ExpenseCategory { Name = "Sports/Hobbies"},
                new ExpenseCategory { Name = "Travel/Leisure"},
                new ExpenseCategory { Name = "Medical"},
                new ExpenseCategory { Name = "Pets"},
                new ExpenseCategory { Name = "Others"},
            });

            data.IncomeCategories.AddRange(new[]
            {
                new IncomeCategory {Name = "Salary"},
                new IncomeCategory {Name = "Wages"},
                new IncomeCategory {Name = "Interest"},
                new IncomeCategory {Name = "Dividends"},
                new IncomeCategory {Name = "Business income"},
                new IncomeCategory {Name = "Capital gains"},
                new IncomeCategory {Name = "From the previous month"},
                new IncomeCategory {Name = "Sales"},
                new IncomeCategory {Name = "Others"},
            });
        }
    }
}
