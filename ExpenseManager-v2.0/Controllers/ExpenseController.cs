namespace ExpenseManager_v2._0.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;    
    using ExpenseManager_v2._0.Services.Expense;
    using ExpenseManager_v2._0.Infrastructure;

    public class ExpenseController : Controller
    {
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService expenseService)
            => this.expenseService = expenseService;
        


        [Authorize]
        public IActionResult Add()
            => View(expenseService.GETAdd());

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddExpenseServiceModel expense)
        {
            if (!expenseService.IsExpenseCategoryExist(expense))
            {
                this.ModelState.AddModelError(nameof(expense.ExpenseCategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                expense.ExpenseCategories = this.expenseService.GetExpenseCategories();

                return View(expense);
            }

            var currentUserId = this.User.GetId();

            expenseService.POSTAdd(expense, currentUserId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All()
        {
            var currentUserId = this.User.GetId();

            var expensesForThisUser = this.expenseService.All(currentUserId);

            return View(expensesForThisUser);
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
            
                if (!expenseService.IsExpenseCategoryExist(expenseToBeEdited))
                {
                    this.ModelState.AddModelError(nameof(expenseToBeEdited.ExpenseCategoryId), "Category does not exist.");
                }

                if (!ModelState.IsValid)
                {
                    expenseToBeEdited.ExpenseCategories = this.expenseService.GetExpenseCategories();

                    return View(expenseToBeEdited);
                }

                var edited = this.expenseService.Edit(
                    id,
                    expenseToBeEdited.Name,
                    expenseToBeEdited.ExpensDate,
                    expenseToBeEdited.Amount,
                    expenseToBeEdited.Notes,
                    expenseToBeEdited.ExpenseCategoryId);

                if (!edited)
                {
                    return BadRequest();
                }

                return RedirectToAction(nameof(All));
            
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var exists = expenseService.IsExpenseExist(id);

            if (!exists)
            {
                return NotFound();
            }

            var detailedExpense = expenseService.Details(id);

            if (detailedExpense.Categorie == null)
            {
                this.ModelState.AddModelError(nameof(detailedExpense.Categorie), "Category name is missing ;(.");
            }           

            return View(detailedExpense);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var exists = expenseService.IsExpenseExist(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = expenseService.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

    }
}
