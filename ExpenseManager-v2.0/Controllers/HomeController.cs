namespace ExpenseManager_v2._0.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Models;
    using ExpenseManager_v2._0.Models.Home;
    using ExpenseManager_v2._0.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly ExpenseManagerDbContext data;

        public HomeController(IStatisticsService statistics, ExpenseManagerDbContext data)
        {
            this.statistics = statistics;
            this.data = data;        
        }

        public IActionResult Index()
        {
            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalTransactions = totalStatistics.TotalTransactions,
                TotalUsers = totalStatistics.TotalUsers
            });
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
