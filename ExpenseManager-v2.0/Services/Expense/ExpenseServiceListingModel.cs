namespace ExpenseManager_v2._0.Services.Expense
{
    public class ExpenseServiceListingModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ExpensDate { get; init; }

        public decimal Amount { get; init; }

        public string Category { get; init; }

        public string UserId { get; init; }
    }
}
