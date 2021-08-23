namespace ExpenseManager_v2._0.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;

    using static WebConstants;    

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ExpenseManagerDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<ExpenseManagerDbContext>();

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

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userMenager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleMenager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleMenager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleMenager.CreateAsync(role);

                    const string adminEmail = "admin@budget.com";
                    const string adminPassword = "admin12";

                    var user = new ApplicationUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userMenager.CreateAsync(user, adminPassword);

                    await userMenager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
