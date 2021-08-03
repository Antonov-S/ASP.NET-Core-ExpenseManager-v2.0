namespace ExpenseManager_v2._0.Services.Expense
{
    using System.Collections.Generic;

    public interface IExpenseService
    {
        AddExpenseServiceModel GETAdd();

        void POSTAdd(AddExpenseServiceModel addServiceModel, string userId);

        IEnumerable<ExpenseServiceListingModel> All(string userId);

        IEnumerable<ExpenseCategoryServicesModel> GetExpenseCategories();
    }
}
