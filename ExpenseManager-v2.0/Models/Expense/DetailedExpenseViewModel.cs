namespace ExpenseManager_v2._0.Models.Expense
{
    using ExpenseManager_v2._0.Data.Models;

    public class DetailedExpenseViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ExpensDate { get; init; }

        public decimal Amount { get; init; }

        public string Notes { get; init; }

        public int ExpenseCategoryId { get; init; }

        public ExpenseCategory ExpenseCategory { get; init; }

        public string Category { get; init; }
    }
}
