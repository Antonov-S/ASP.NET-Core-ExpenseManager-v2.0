namespace ExpenseManager_v2._0
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Infrastructure;
    using ExpenseManager_v2._0.Services.Borrowed;
    using ExpenseManager_v2._0.Services.Credit;
    using ExpenseManager_v2._0.Services.Expense;
    using ExpenseManager_v2._0.Services.Income;
    using ExpenseManager_v2._0.Services.Saving;
    using ExpenseManager_v2._0.Services.Statistics;    
    using ExpenseManager_v2._0.Data.Models;    

    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => this.Configuration = configuration;
        

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ExpenseManagerDbContext>(options => options
            .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ExpenseManagerDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });

            services
                .AddTransient<IStatisticsService, StatisticsService>();

            services
                .AddTransient<IExpenseService, ExpenseService>();

            services
                .AddTransient<IIncomeService, IncomeService>();

            services
                .AddTransient<ICreditService, CreditService>();

            services
                .AddTransient<ISavingService, SavingService>();

            services
                .AddTransient<IBorrowedService, BorrowedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });

            
        }
    }
}
