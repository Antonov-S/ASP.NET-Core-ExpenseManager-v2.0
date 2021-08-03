namespace ExpenseManager_v2._0.Services.Expense
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    
    public class ExpenseService : IExpenseService
    {
        private readonly ExpenseManagerDbContext data;

        public ExpenseService(ExpenseManagerDbContext data)
            => this.data = data;


        public AddExpenseServiceModel GETAdd()
        {
            return new AddExpenseServiceModel
            {
                ExpenseCategories = this.GetExpenseCategories()
            };
        }

        public void POSTAdd(AddExpenseServiceModel addServiceModel, string userId)
        {
            var expenseData = new Expense
            {
                Name = addServiceModel.Name,
                ExpenseDate = DateTime.Parse(addServiceModel.ExpensDate),
                Amount = addServiceModel.Amount,
                Notes = addServiceModel.Notes,
                ExpenseCategoryId = addServiceModel.ExpenseCategoryId,
                UserId = userId
            };

            data.Add(expenseData);
            data.SaveChanges();
        }

        public IEnumerable<ExpenseServiceListingModel> All(string currentUserId)
        {
            var expenses = this.data
                .Expenses
                .Where(c => c.UserId == currentUserId)
                .OrderByDescending(c => c.Id)
                .Select(c => new ExpenseServiceListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ExpensDate = c.ExpenseDate.ToString("dd/MM/yyyy"),
                    Amount = c.Amount,
                    Category = c.ExpenseCategory.Name
                })
                .ToList();

            return expenses;
        }

        public IEnumerable<ExpenseCategoryServicesModel> GetExpenseCategories()
            => this.data
            .ExpenseCategories
            .Select(c => new ExpenseCategoryServicesModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        
    }
}
