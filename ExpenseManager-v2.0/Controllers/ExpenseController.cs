namespace ExpenseManager_v2._0.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    using ExpenseManager_v2._0.Models.Expense;
    using Microsoft.AspNetCore.Mvc;

    public class ExpenseController : Controller
    {
        private readonly ExpenseManagerDbContext data;

        public ExpenseController(ExpenseManagerDbContext data) =>
            this.data = data;


        public IActionResult Add() => View(new AddExpenseFormModel
        {
            ExpenseCategories = this.GetExpenseCategories()
        });

        [HttpPost]
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

            var expenseData = new Expense
            {
                Name = expense.Name,
                ExpenseDate = DateTime.Parse(expense.ExpensDate),
                Amount = expense.Amount,
                Notes = expense.Notes,
                ExpenseCategoryId = expense.ExpenseCategoryId
            };

            data.Add(expenseData);
            data.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var expense = this.data
                .Expenses
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
