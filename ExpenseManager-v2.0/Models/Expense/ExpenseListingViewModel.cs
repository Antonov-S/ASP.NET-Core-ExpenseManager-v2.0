namespace ExpenseManager_v2._0.Models.Expense
{
    public class ExpenseListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ExpensDate { get; init; }

        public decimal Amount { get; init; }

        public string Notes { get; init; }

        public string Category { get; init; }
    }
}
