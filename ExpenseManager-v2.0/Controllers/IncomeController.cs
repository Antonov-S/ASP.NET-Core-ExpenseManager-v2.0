namespace ExpenseManager_v2._0.Controllers
{
    using ExpenseManager_v2._0.Services.Income;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ExpenseManager_v2._0.Infrastructure;


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

            var userId = this.User.GetId();

            incomeService.POSTAdd(income, userId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All()
        {
            var currentUserId = this.User.GetId();

            var incomesForThisUser = this.incomeService.All(currentUserId);

            return View(incomesForThisUser);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            var incomeToBeEdited = this.incomeService.Details(id);

            if (incomeToBeEdited.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new AddIncomeServiceModel
            {
                Id = incomeToBeEdited.Id,
                Name = incomeToBeEdited.Name,
                IncomeDate = incomeToBeEdited.IncomeDate,
                Amount = incomeToBeEdited.Amount,
                Notes = incomeToBeEdited.Notes,
                IncomeCategoryId = incomeToBeEdited.IncomeCategoryId,
                IncomeCategories = this.incomeService.GetIncomeCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, AddIncomeServiceModel incomeToBeEdited)
        {
            if (!incomeService.IsIncomeCategoryExist(incomeToBeEdited))
            {
                this.ModelState.AddModelError(nameof(incomeToBeEdited.IncomeCategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                incomeToBeEdited.IncomeCategories = this.incomeService.GetIncomeCategories();

                return View(incomeToBeEdited);
            }

            var edited = this.incomeService.Edit(
                id,
                incomeToBeEdited.Name,
                incomeToBeEdited.IncomeDate,
                incomeToBeEdited.Amount,
                incomeToBeEdited.Notes,
                incomeToBeEdited.IncomeCategoryId);

            if (!edited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

    }
}
