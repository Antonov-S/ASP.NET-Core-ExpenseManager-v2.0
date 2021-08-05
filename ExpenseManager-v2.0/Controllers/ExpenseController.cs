namespace ExpenseManager_v2._0.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Models.Expense;    
    using ExpenseManager_v2._0.Services.Expense;
    using ExpenseManager_v2._0.Infrastructure;

    public class ExpenseController : Controller
    {
        private readonly ExpenseManagerDbContext data;
        private readonly IExpenseService expenseService;

        public ExpenseController(ExpenseManagerDbContext data,
            IExpenseService expenseService)
        { 
            this.data = data;
            this.expenseService = expenseService;
        }


        [Authorize]
        public IActionResult Add()
            => View(expenseService.GETAdd());

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddExpenseServiceModel expense)
        {
            if (!this.data.ExpenseCategories.Any(c => c.Id == expense.ExpenseCategoryId))
            {
                this.ModelState.AddModelError(nameof(expense.ExpenseCategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                expense.ExpenseCategories = this.expenseService.GetExpenseCategories();

                return View(expense);
            }
            
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            expenseService.POSTAdd(expense, userId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var expensesForThisUser = this.expenseService.All(currentUserId);

            return View(expensesForThisUser);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var exists = this.data
                .Expenses
                .Any(q => q.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            var detailedExpense = this.data
                .Expenses
                .Where(c => c.Id == id)
                .Select(e => new DetailedExpenseViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    ExpensDate = e.ExpenseDate.ToString("dd/MM/yyyy"),
                    Amount = e.Amount,
                    Notes = e.Notes,
                    Category = e.ExpenseCategory.Name
                }).FirstOrDefault();
            return View(detailedExpense);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            var expenseToBeEdited = this.expenseService.Details(id);

            if (expenseToBeEdited.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new AddExpenseServiceModel
            {
                Id = expenseToBeEdited.Id,
                Name = expenseToBeEdited.Name,
                ExpensDate = expenseToBeEdited.ExpensDate,
                Amount = expenseToBeEdited.Amount,
                Notes = expenseToBeEdited.Notes,
                ExpenseCategoryId = expenseToBeEdited.ExpenseCategoryId,
                ExpenseCategories = this.expenseService.GetExpenseCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, AddExpenseServiceModel expenseToBeEdited)
        {
            try
            {
                if (!expenseService.IsExpenseCategoryExist(expenseToBeEdited))
                {
                    this.ModelState.AddModelError(nameof(expenseToBeEdited.ExpenseCategoryId), "Category does not exist.");
                }

                if (!ModelState.IsValid)
                {
                    expenseToBeEdited.ExpenseCategories = this.expenseService.GetExpenseCategories();

                    return View(expenseToBeEdited);
                }

                this.expenseService.Edit(
                    id,
                    expenseToBeEdited.Name,
                    expenseToBeEdited.ExpensDate,
                    expenseToBeEdited.Amount,
                    expenseToBeEdited.Notes,
                    expenseToBeEdited.ExpenseCategoryId);

                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(expenseToBeEdited);
            }
        }

    }
}
