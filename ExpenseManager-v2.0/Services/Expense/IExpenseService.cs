namespace ExpenseManager_v2._0.Services.Expense
{
    using System.Collections.Generic;

    public interface IExpenseService
    {
        AddExpenseServiceModel GETAdd();
        void POSTAdd(AddExpenseServiceModel addServiceModel, string userId);
        IEnumerable<ExpenseServiceListingModel> All(string userId);
        ExpenseDetailsServiceModel Details(int expenseId);
        bool Edit(
            int id,
            string name,
            string expensDate,
            decimal amount,
            string notes,
            int expensCategoryId);
        bool IsExpenseCategoryExist(AddExpenseServiceModel expenseToBeChacked);
        IEnumerable<ExpenseCategoryServicesModel> GetExpenseCategories();
    }
}
