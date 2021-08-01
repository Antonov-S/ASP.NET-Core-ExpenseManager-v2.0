namespace ExpenseManager_v2._0.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Collections.Generic;
    using System.Linq;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    using ExpenseManager_v2._0.Models.Expense;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ExpenseController : Controller
    {
        private readonly ExpenseManagerDbContext data;

        public ExpenseController(ExpenseManagerDbContext data) =>
            this.data = data;


        [Authorize]
        public IActionResult Add() => View(new AddExpenseFormModel
        {
            ExpenseCategories = this.GetExpenseCategories()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddExpenseFormModel expense)
        {
            if (!this.data.ExpenseCategories.Any(c => c.Id == expense.ExpenseCategoryId))
            {
                this.ModelState.AddModelError(nameof(expense.ExpenseCategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                expense.ExpenseCategories = this.GetExpenseCategories();

                return View(expense);
            }

            if (!this.UserHasRight())
            {
               return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var expenseData = new Expense
            {
                Name = expense.Name,
                ExpenseDate = DateTime.Parse(expense.ExpensDate),
                Amount = expense.Amount,
                Notes = expense.Notes,
                ExpenseCategoryId = expense.ExpenseCategoryId,
                UserId = userId
            };

            data.Add(expenseData);
            data.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var expense = this.data
                .Expenses
                .Where(c =>c.UserId == currentUserId)
                .OrderByDescending(c => c.Id)
                .Select(c => new ExpenseListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ExpensDate = c.ExpenseDate.ToString("dd/MM/yyyy"),
                    Amount = c.Amount,
                    Category = c.ExpenseCategory.Name
                })
                .ToList();

            return View(expense);
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

        private bool UserHasRight()
            => this.data
            .Users
            .Any(u => u.Id == this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        private IEnumerable<ExpenseCategoryViewModel> GetExpenseCategories()
            => this.data
            .ExpenseCategories
            .Select(c => new ExpenseCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

    }
}
