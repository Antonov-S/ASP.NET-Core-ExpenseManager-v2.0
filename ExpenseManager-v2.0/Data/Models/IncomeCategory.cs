namespace ExpenseManager_v2._0.Data.Models
{
    using System.Collections.Generic;

    public class IncomeCategory
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Income> Incomes { get; init; } = new List<Income>();
    }
}
