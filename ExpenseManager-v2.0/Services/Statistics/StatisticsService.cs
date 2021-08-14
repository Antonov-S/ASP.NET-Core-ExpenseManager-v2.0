namespace ExpenseManager_v2._0.Services.Statistics
{
    using System.Linq;
    using ExpenseManager_v2._0.Data;
    
    public class StatisticsService : IStatisticsService
    {
        private readonly ExpenseManagerDbContext data;

        public StatisticsService(ExpenseManagerDbContext data)
            => this.data = data;
       

        public StatisticsServiceModel Total()
        {
            var totalReportedTrasactions = this.data.Expenses.Count() 
                + this.data.Incomes.Count() 
                + this.data.Credits.Count() 
                + this.data.InstallmentLoans.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalTransactions = totalReportedTrasactions,
                TotalUsers = totalUsers
            };
        }
    }
}
