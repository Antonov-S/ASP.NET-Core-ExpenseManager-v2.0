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
                    Notes = c.Notes,
                    Category = c.ExpenseCategory.Name
                })
                .ToList();

            return View(expense);
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
