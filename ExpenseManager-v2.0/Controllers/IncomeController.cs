namespace ExpenseManager_v2._0.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    using ExpenseManager_v2._0.Models.Income;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    public class IncomeController : Controller
    {
        private readonly ExpenseManagerDbContext data;

        public IncomeController(ExpenseManagerDbContext data) =>
            this.data = data;

        [Authorize]
        public IActionResult Add() => View(new AddIncomeFormModel
        {
            IncomeCategories = this.GetIncomeCategories()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddIncomeFormModel income)
        {
            if (!this.data.IncomeCategories.Any(c => c.Id == income.IncomeCategoryId))
            {
                this.ModelState.AddModelError(nameof(income.IncomeCategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                income.IncomeCategories = this.GetIncomeCategories();

                return View(income);
            }

            if (!this.UserHasRight())
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var incomeData = new Income
            {
                Name = income.Name,
                IncomeDate = DateTime.Parse(income.IncomeDate),
                Amount = income.Amount,
                Notes = income.Notes,
                IncomeCategorysId = income.IncomeCategoryId,
                UserId = currentUserId
            };

            data.Add(incomeData);
            data.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var expense = this.data
                .Incomes
                .Where(c => c.UserId == currentUserId)
                .OrderByDescending(c => c.Id)
                .Select(c => new IncomeListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    IncomeDate = c.IncomeDate.ToString("dd/MM/yyyy"),
                    Amount = c.Amount,
                    Category = c.IncomeCategory.Name
                })
                .ToList();

            return View(expense);
        }

        private bool UserHasRight()
            => this.data
            .Users
            .Any(u => u.Id == this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        private IEnumerable<IncomeCategoryViewModel> GetIncomeCategories()
           => this.data
           .IncomeCategories
           .Select(c => new IncomeCategoryViewModel
           {
               Id = c.Id,
               Name = c.Name
           })
           .ToList();
    }
}
