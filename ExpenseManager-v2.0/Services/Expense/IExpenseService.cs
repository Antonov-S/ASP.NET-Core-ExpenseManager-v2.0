namespace ExpenseManager_v2._0.Services.Expense
{
    using System.Collections.Generic;
    using ExpenseManager_v2._0.Data.Models;

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

        public bool IsExpenseCategoryExist(AddExpenseServiceModel expenseToBeChacked);

        public bool IsExpenseExist(int expenseId);

        IEnumerable<ExpenseCategoryServicesModel> GetExpenseCategories();

        public int GetCategoryId(int expenseId);

        public string GetCategorieName(int expenseCategoryId);

        public Expense FindExpense(int id);

        bool Delete(int id);
    }
}
