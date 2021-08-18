namespace ExpenseManager_v2._0.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Income> Incomes { get; init; } = new List<Income>();

        public IEnumerable<Expense> Expenses { get; init; } = new List<Expense>();

        public IEnumerable<Credit> Credits { get; init; } = new List<Credit>();

        public Saving Saving { get; init; }

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}