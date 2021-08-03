namespace ExpenseManager_v2._0.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    using ExpenseManager_v2._0.Models.Expense;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ExpenseManager_v2._0.Services.Expense;

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
            var exists = this.data
                .Expenses
                .Any(q => q.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            var editedExpense = data.Expenses
                .Where(e => e.Id == id)
                .Select(x => new DetailedExpenseViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ExpensDate = x.ExpenseDate.ToString("dd/MM/yyyy"),
                    Amount = x.Amount,
                    Notes = x.Notes,
                    ExpenseCategoryId = x.ExpenseCategoryId,
                    Category = x.ExpenseCategory.Name,
                })
                .FirstOrDefault();

            return View(editedExpense);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(DetailedExpenseViewModel editedExpense)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(editedExpense);
                }

                var uppdatedExpense = new Expense
                {
                    Id = editedExpense.Id,
                    ExpenseDate = DateTime.Parse(editedExpense.ExpensDate),
                    Amount = editedExpense.Amount,
                    Notes = editedExpense.Notes,
                    ExpenseCategoryId = editedExpense.ExpenseCategory.Id
                };

                data.Expenses.Update(uppdatedExpense);
                
                return RedirectToAction(nameof(All));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(editedExpense);
            }
        }

    }
}
