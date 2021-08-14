namespace ExpenseManager_v2._0.Services.Credit
{
    using System.Collections.Generic;
    using ExpenseManager_v2._0.Data.Models;

    public interface ICreditService
    {
        AddCreditServiceModel GETAdd();
        void POSTAdd(AddCreditServiceModel addCreditModel,string userId);
        IEnumerable<CreditServiceListingModel> All(string userId);
        CreditDetailsServiceModel Details(int creditId);
        bool Edit(
            int id,
            string name,
            decimal amountOfMonthlyInstallment,
            int numberOfInstallmentsRemaining,
            decimal unpaidFees,
            string maturityDate,
            decimal total,
            string notes);
        public bool IsCreditExist(int creditId);
        public Credit FindCredit(int id);
        bool Delete(int id);
        public bool IsCreditRemainingAmountEnough(int creditId, decimal installmentLoanAmount);
        void POSTMakePayment(AddInstallmentLoansServiceModel installmentLoanModel, int Id);
        IEnumerable<ListingInstallmentLoansServiceModel> AllPaymentsOnCredit(int creditId);
        public bool IsDeleted(int id);
        public bool DeletePayment(int paymentId);
        public bool IsPaymentExist(int paymentId);

    }
}
