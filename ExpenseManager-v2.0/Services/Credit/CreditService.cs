using ExpenseManager_v2._0.Data;

namespace ExpenseManager_v2._0.Services.Credit
{
    public class CreditService : ICreditService
    {
        private readonly ExpenseManagerDbContext data;

        public CreditService(ExpenseManagerDbContext data)
            =>this.data = data;

        public AddCreditServiceModel GETAdd()
        {
            return new AddCreditServiceModel();
        }
            
    }
}
