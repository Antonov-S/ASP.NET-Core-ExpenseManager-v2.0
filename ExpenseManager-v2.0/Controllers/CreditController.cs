namespace ExpenseManager_v2._0.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ExpenseManager_v2._0.Infrastructure;
    using ExpenseManager_v2._0.Services.Credit;

    public class CreditController : Controller
    {
        private readonly ICreditService creditService;

        public CreditController(ICreditService creditService)
            => this.creditService = creditService;

        public IActionResult Add()
            => View(creditService.GETAdd());
    }
}
