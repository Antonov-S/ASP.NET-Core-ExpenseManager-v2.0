namespace ExpenseManager_v2._0.Services.Income
{
    using System;    
    using System.Linq;
    using System.Collections.Generic;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;

    public class IncomeService : IIncomeService
    {
        private readonly ExpenseManagerDbContext data;

        public IncomeService(ExpenseManagerDbContext data)
            => this.data = data;

        public AddIncomeServiceModel GETAdd()
        {
            return new AddIncomeServiceModel
            {
                IncomeCategories = this.GetIncomeCategories()
            };
                       
        }

        public void POSTAdd(AddIncomeServiceModel addServiceModel, string userId)
        {
            var incomeData = new Income
            {
                Name = addServiceModel.Name,
                IncomeDate = DateTime.Parse(addServiceModel.IncomeDate),
                Amount = addServiceModel.Amount,
                Notes = addServiceModel.Notes,
                IncomeCategorysId = addServiceModel.IncomeCategoryId,
                UserId = userId
            };

            data.Add(incomeData);
            data.SaveChanges();
        }

        public IEnumerable<IncomeServiceListingModel> All(string currentUserId)
        {
            var incomes = this.data
                .Incomes
                .Where(c => c.UserId == currentUserId)
                .OrderByDescending(c => c.Id)
                .Select(c => new IncomeServiceListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    IncomeDate = c.IncomeDate.ToString("dd/MM/yyyy"),
                    Amount = c.Amount,
                    Category = c.IncomeCategory.Name
                })
                .ToList();

            return incomes;
        }

        public IEnumerable<IncomeCategoryServicesModel> GetIncomeCategories()
                => this.data
               .IncomeCategories
               .Select(c => new IncomeCategoryServicesModel
               {
                   Id = c.Id,
                   Name = c.Name
               })
               .ToList();

        public bool IsIncomeCategoryExist(AddIncomeServiceModel income)
            => data.IncomeCategories.Any(c => c.Id == income.IncomeCategoryId);
    }
}
