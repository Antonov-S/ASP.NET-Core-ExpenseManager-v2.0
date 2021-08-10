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

        public ExpenseDetailsServiceModel Details(int expenseId)
        {
            if (IsExpenseExist(expenseId))
            {
                var expenseCategoryId = GetCategoryId(expenseId);

                var categorieName = GetCategorieName(expenseCategoryId);

               var details = this.data
                    .Expenses
                    .Where(c => c.Id == expenseId)
                    .Select(c => new ExpenseDetailsServiceModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ExpensDate = c.ExpenseDate.ToString("dd/MM/yyyy"),
                        Amount = c.Amount,
                        Notes = c.Notes,
                        ExpenseCategoryId = c.ExpenseCategoryId,
                        UserId = c.UserId,
                        Categorie = categorieName
                    })
                    .FirstOrDefault();

                return details;
            }
            else
            {
                return null;
            }      
            
        }

        public bool Edit(int id, string name, string expensDate, decimal amount, string notes, int expensCategoryId)
        {
            var editedData = FindExpense(id);

            if (editedData == null)
            {
                return false;
            }

            editedData.Name = name;
            editedData.ExpenseDate = DateTime.Parse(expensDate);
            editedData.Amount = amount;
            editedData.Notes = notes;
            editedData.ExpenseCategoryId = expensCategoryId;

            data.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var deletedExpense = FindExpense(id);

            if (deletedExpense == null)
            {
                return false;
            }

            data.Expenses.Remove(deletedExpense);
            data.SaveChanges();
            return true;
        }

        public bool IsExpenseCategoryExist(AddExpenseServiceModel expenseToBeChacked)
            => data
            .ExpenseCategories
            .Any(c => c.Id == expenseToBeChacked.ExpenseCategoryId);

        public bool IsExpenseExist(int expenseId)
            => data
            .Expenses
            .Any(e => e.Id == expenseId);

        public IEnumerable<ExpenseCategoryServicesModel> GetExpenseCategories()
            => this.data
            .ExpenseCategories
            .Select(c => new ExpenseCategoryServicesModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        public string GetCategorieName(int expenseCategoryId)
            => this.data
            .ExpenseCategories
            .Where(c => c.Id == expenseCategoryId)
            .Select (c => c.Name)
            .FirstOrDefault()
            .ToString();

        public int GetCategoryId(int expenseId)
            => this.data
                .Expenses
                .Where(x => x.Id == expenseId)
                .Select(c => c.ExpenseCategoryId)
                .FirstOrDefault();

        public Expense FindExpense(int id)
            => this.data
            .Expenses.Find(id);
        
    }
}
