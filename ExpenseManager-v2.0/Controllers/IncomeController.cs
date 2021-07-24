namespace ExpenseManager_v2._0.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    using ExpenseManager_v2._0.Models.Income;
    using Microsoft.AspNetCore.Mvc;
    
    public class IncomeController : Controller
    {
        private readonly ExpenseManagerDbContext data;

        public IncomeController(ExpenseManagerDbContext data) =>
            this.data = data;

        public IActionResult Add() => View(new AddIncomeFormModel
        {
            IncomeCategories = this.GetIncomeCategories()
        });

        [HttpPost]
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

            var incomeData = new Income
            {
                Name = income.Name,
                IncomeDate = DateTime.Parse(income.IncomeDate),
                Amount = income.Amount,
                Notes = income.Notes,
                IncomeCategorysId = income.IncomeCategoryId
            };

            data.Add(incomeData);
            data.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

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
