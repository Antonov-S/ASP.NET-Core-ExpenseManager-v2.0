namespace ExpenseManager_v2._0.Services.Income
{
    using ExpenseManager_v2._0.Services.Expense;
    using System.Collections.Generic;

    public interface IIncomeService
    {
        AddIncomeServiceModel GETAdd();

        void POSTAdd(AddIncomeServiceModel addServiceModel, string userId);

        IEnumerable<IncomeServiceListingModel> All(string userId);

        IEnumerable<IncomeCategoryServicesModel> GetIncomeCategories();
        bool IsIncomeCategoryExist(AddIncomeServiceModel income);
    }
}
