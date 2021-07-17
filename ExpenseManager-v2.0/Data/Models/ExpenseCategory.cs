namespace ExpenseManager_v2._0.Data.Models
{
    using System.Collections.Generic;

    public class ExpenseCategory
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Expense> Expenses { get; init; } = new List<Expense>();
    }
}
