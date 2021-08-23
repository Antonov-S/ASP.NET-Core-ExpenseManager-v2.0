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
        private readonly IMapper _mapper;

        public CreditService(ExpenseManagerDbContext data, IMapper mapper)
        { 
            this.data = data;
            this._mapper = mapper;
        }


        public AddCreditServiceModel GETAdd()
        {
            return new AddCreditServiceModel();
        }

        public void POSTAdd(AddCreditServiceModel addCreditModel, string userId)
        {
            var creditData = _mapper.Map<Credit>(addCreditModel);
            creditData.UserId = userId;
            data.Add(creditData);
            data.SaveChanges();
        }

        public IEnumerable<CreditServiceListingModel> All(string currentUserId)
        {
            var credits = this.data
                .Credits
                .Where(c => c.UserId == currentUserId && c.IsDeleted != true)
                .OrderByDescending(c => c.Id)
                .ToList();

            var payments = _mapper.Map<List<Credit>, List<CreditServiceListingModel>>(credits);

            return payments;
        }

        public CreditDetailsServiceModel Details(int creditId)
        {
            var detail = this.data
                .Credits
                .Where(c => c.Id == creditId && IsDeleted(creditId) != true)
                .FirstOrDefault();

            return _mapper.Map<CreditDetailsServiceModel>(detail);            
        }

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
            var installmentLoans = this.data
                .InstallmentLoans
                .Where(p => p.CreditId == creditId && p.IsDeleted != true)
                .ToList();

            var payments = _mapper.Map<List<InstallmentLoan>, List<ListingInstallmentLoansServiceModel>>(installmentLoans);

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
