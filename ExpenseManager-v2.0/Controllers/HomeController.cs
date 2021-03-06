namespace ExpenseManager_v2._0.Controllers
{
    using ExpenseManager_v2._0.Models.Home;
    using ExpenseManager_v2._0.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;

        public HomeController(IStatisticsService statistics)
            => this.statistics = statistics;

        public IActionResult Index()
        {
            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalTransactions = totalStatistics.TotalTransactions,
                TotalUsers = totalStatistics.TotalUsers
            });
        }
        
        public IActionResult Error() => View();
    }
}
