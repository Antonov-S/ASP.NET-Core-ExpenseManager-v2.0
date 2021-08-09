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

        public IncomeDetailsServiceModel Details(int incomeId)
        {
            if (IsincomeExist(incomeId))
            {
                var incomeCategoryId = GetCategoryId(incomeId);

                var categorieName = GetCategorieName(incomeCategoryId);

                var details = this.data
                     .Incomes
                     .Where(c => c.Id == incomeId)
                     .Select(c => new IncomeDetailsServiceModel
                     {
                         Id = c.Id,
                         Name = c.Name,
                         IncomeDate = c.IncomeDate.ToString("dd/MM/yyyy"),
                         Amount = c.Amount,
                         Notes = c.Notes,
                         IncomeCategoryId = c.IncomeCategorysId,
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

        public bool Edit(int id,
            string name,
            string incomeDate,
            decimal amount,
            string notes,
            int incomeCategoryId)
        {
            var editedData = this.data.Incomes.Find(id);

            if (editedData == null)
            {
                return false;
            }

            editedData.Name = name;
            editedData.IncomeDate = DateTime.Parse(incomeDate);
            editedData.Amount = amount;
            editedData.Notes = notes;
            editedData.IncomeCategorysId = incomeCategoryId;

            this.data.SaveChanges();

            return true;
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

        public bool IsincomeExist(int incomeId)
            => data
            .Incomes
            .Any(e => e.Id == incomeId);

        public int GetCategoryId(int incomeId)
            => this.data
                .Incomes
                .Where(x => x.Id == incomeId)
                .Select(c => c.IncomeCategorysId)
                .FirstOrDefault();

        public string GetCategorieName(int incomeCategoryId)
            => this.data
            .IncomeCategories
            .Where(c => c.Id == incomeCategoryId)
            .Select(c => c.Name)
            .FirstOrDefault()
            .ToString();
    }
}
