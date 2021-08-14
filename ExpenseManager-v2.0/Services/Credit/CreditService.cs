namespace ExpenseManager_v2._0.Services.Credit
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
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
                IsDeleted = false,
                UserId = userId
            };

            data.Add(creditData);
            data.SaveChanges();
        }

        public IEnumerable<CreditServiceListingModel> All(string currentUserId)
        {
            var credits = this.data
                .Credits
                .Where(c => c.UserId == currentUserId && c.IsDeleted != true)
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
              .Where(c => c.Id == creditId && IsDeleted(creditId) != true)
              .Select(c => new CreditDetailsServiceModel
              {
                  Id = c.Id,
                  Name = c.Name,
                  AmountOfMonthlyInstallment = c.AmountOfMonthlyInstallment,
                  NumberOfInstallmentsRemaining = c.NumberOfInstallmentsRemaining,
                  UnpaidFees = c.UnpaidFees,
                  MaturityDate = c.MaturityDate.ToString("dd/MM/yyyy"),
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

            if (editedData == null || editedData.IsDeleted == true)
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

        public bool Delete(int id)
        {
            var deletedCredit = FindCredit(id);

            if (deletedCredit == null || deletedCredit.IsDeleted == true)
            {
                return false;
            }

            deletedCredit.IsDeleted = true;

            data.SaveChanges();
            return true;
        }

        public void POSTMakePayment(AddInstallmentLoansServiceModel installmentLoanModel, int Id)
        {
                      
            var installmentLoanData = new InstallmentLoan
            {
                Date = installmentLoanModel.Date,
                Amount = installmentLoanModel.Amount,
                CreditId = Id
            };

            data.Add(installmentLoanData);

            var creditToBeRedused = FindCredit(installmentLoanData.CreditId);

            if (creditToBeRedused != null)
            {
                creditToBeRedused.Total -= installmentLoanData.Amount;
            }

            data.SaveChanges();
        }

        public IEnumerable<ListingInstallmentLoansServiceModel> AllPaymentsOnCredit(int creditId)
        {
            var payments = this.data
                .InstallmentLoans
                .Where(p => p.CreditId == creditId && p.IsDeleted != true)
                .Select(p => new ListingInstallmentLoansServiceModel
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    Date = p.Date
                })
                .ToList();

            return payments;
        }

        public bool DeletePayment(int Id)
        {
            var deletedPayment = this.data
                .InstallmentLoans
                .Where(c => c.Id == Id)
                .FirstOrDefault();

            if (deletedPayment.IsDeleted == true)
            {
                return false;
            }

            if (deletedPayment == null)
            {
                return false;
            }

            deletedPayment.IsDeleted = true;

            data.SaveChanges();
            return true;
        }


        public bool IsCreditExist(int creditId)
                => data
                .Credits
                .Any(e => e.Id == creditId && e.IsDeleted != true);

        public Credit FindCredit(int id)
            => this.data
            .Credits.Find(id);

        public bool IsCreditRemainingAmountEnough(int creditId, decimal installmentLoanAmount)
        {
            var credit = FindCredit(creditId);

            if (credit.Total < installmentLoanAmount)
            {
                return false;
            }

            return true;
        }

        public bool IsDeleted(int id)
            => this.data
                .Credits
                .Where(c => c.Id == id)
                .Select(c => c.IsDeleted)
                .FirstOrDefault();

        public bool IsPaymentExist(int paymentId)
            => data
                .InstallmentLoans
                .Any(e => e.Id == paymentId && IsDeleted(paymentId) != true);
    }        
}
