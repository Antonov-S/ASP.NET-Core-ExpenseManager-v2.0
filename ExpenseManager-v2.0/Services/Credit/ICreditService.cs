﻿namespace ExpenseManager_v2._0.Services.Credit
{
    using System.Collections.Generic;
    using ExpenseManager_v2._0.Data.Models;

    public interface ICreditService
    {
        AddCreditServiceModel GETAdd();

        void POSTAdd(AddCreditServiceModel addCreditModel,
            string userId);

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
    }
}
