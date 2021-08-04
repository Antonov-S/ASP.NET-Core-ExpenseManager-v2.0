namespace ExpenseManager_v2._0.Controllers
{
    using System.Security.Claims;
    using ExpenseManager_v2._0.Services.Income;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    public class IncomeController : Controller
    {
        private readonly IIncomeService incomeService;

        public IncomeController(IIncomeService incomeService)
            => this.incomeService = incomeService;
       

        [Authorize]
        public IActionResult Add()
            => View(incomeService.GETAdd());

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddIncomeServiceModel income)
        {
            if (!incomeService.IsIncomeCategoryExist(income))
            {
                this.ModelState.AddModelError(nameof(income.IncomeCategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                income.IncomeCategories = incomeService.GetIncomeCategories();

                return View(income);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            incomeService.POSTAdd(income, userId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var incomesForThisUser = this.incomeService.All(currentUserId);

            return View(incomesForThisUser);
        }

    }
}
