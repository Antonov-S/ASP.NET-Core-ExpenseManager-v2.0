namespace ExpenseManager_v2._0.Services.Income
{
    public class IncomeServiceListingModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string IncomeDate { get; init; }

        public decimal Amount { get; init; }

        public string Category { get; init; }

        public string UserId { get; init; }
    }
}
