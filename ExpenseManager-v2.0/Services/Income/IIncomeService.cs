namespace ExpenseManager_v2._0.Services.Income
{
    using System.Collections.Generic;

    public interface IIncomeService
    {
        AddIncomeServiceModel GETAdd();

        void POSTAdd(AddIncomeServiceModel addServiceModel,
            string userId);
        IEnumerable<IncomeServiceListingModel> All(string userId);
        IncomeDetailsServiceModel Details(int incomeId);

        bool Edit(
            int id,
            string name,
            string incomeDate,
            decimal amount,
            string notes,
            int incomeCategoryId);

        IEnumerable<IncomeCategoryServicesModel> GetIncomeCategories();
        bool IsIncomeCategoryExist(AddIncomeServiceModel income);
        public bool IsincomeExist(int incomeId);
        public int GetCategoryId(int incomeId);
        public string GetCategorieName(int incomeCategoryId);
    }
}
