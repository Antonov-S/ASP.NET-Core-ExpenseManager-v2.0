namespace ExpenseManager_v2._0.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Income> Incomes { get; init; } = new List<Income>();
        public Income Income { get; init; }

        public IEnumerable<Expense> Expenses { get; init; } = new List<Expense>();
        public Expense Expense { get; init; }

        public IEnumerable<Credit> Credits { get; init; } = new List<Credit>();
        public Credit Credit { get; init; }
    }
}