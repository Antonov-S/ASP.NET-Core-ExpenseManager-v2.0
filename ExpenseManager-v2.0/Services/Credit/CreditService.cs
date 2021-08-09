namespace ExpenseManager_v2._0.Services.Credit
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    
    public class CreditService : ICreditService
    {
        private readonly ExpenseManagerDbContext data;

        public CreditService(ExpenseManagerDbContext data)
            => this.data = data;
            

        public AddCreditServiceModel GETAdd()
        {
            return new AddCreditServiceModel();
        }

        public void POSTAdd(AddCreditServiceModel addCreditModel, string userId)
        {
            var creditData = new Credit
            {
                Name = addCreditModel.Name,
                AmountOfMonthlyInstallment = addCreditModel.AmountOfMonthlyInstallment,
                NumberOfInstallmentsRemaining = addCreditModel.NumberOfInstallmentsRemaining,
                UnpaidFees = addCreditModel.UnpaidFees,
                MaturityDate = DateTime.Parse(addCreditModel.MaturityDate),
                Total = addCreditModel.Total,
                Notes = addCreditModel.Notes,
                UserId = userId
            };

            data.Add(creditData);
            data.SaveChanges();
        }

        public IEnumerable<CreditServiceListingModel> All(string currentUserId)
        {
            var credits = this.data
                .Credits
                .Where(c => c.UserId == currentUserId)
                .OrderByDescending(c => c.Id)
                .Select(c => new CreditServiceListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    MaturityDate = c.MaturityDate.ToString("dd/MM/yyyy"),
                    Total = c.Total.ToString(),
                    UserId = c.UserId
                })
                .ToList();

            return credits;
        }

        public CreditDetailsServiceModel Details(int creditId)
            => this.data
              .Credits
              .Where(c => c.Id == creditId)
              .Select(c => new CreditDetailsServiceModel
              {
                  Id = c.Id,
                  Name = c.Name,                  
                  AmountOfMonthlyInstallment = c.AmountOfMonthlyInstallment,
                  NumberOfInstallmentsRemaining = c.NumberOfInstallmentsRemaining,
                  UnpaidFees = c.UnpaidFees,
                  MaturityDate = c.MaturityDate.ToString("dd"),
                  Total = c.Total,
                  Notes = c.Notes,
                  UserId = c.UserId
              })
              .FirstOrDefault();

        public bool Edit(int id, 
            string name, 
            decimal amountOfMonthlyInstallment, 
            int numberOfInstallmentsRemaining, 
            decimal unpaidFees, 
            string maturityDate, 
            decimal total, 
            string notes)
        {
            var editedData = this.data.Credits.Find(id);

            if (editedData == null)
            {
                return false;
            }

            editedData.Name = name;
            editedData.AmountOfMonthlyInstallment = amountOfMonthlyInstallment;
            editedData.NumberOfInstallmentsRemaining = numberOfInstallmentsRemaining;
            editedData.UnpaidFees = unpaidFees;
            editedData.MaturityDate = DateTime.Parse(maturityDate);
            editedData.Total = total;
            editedData.Notes = notes;

            this.data.SaveChanges();

            return true;
        }

        public bool IsCreditExist(int creditId)
            => data
            .Credits
            .Any(e => e.Id == creditId);
    }
}
