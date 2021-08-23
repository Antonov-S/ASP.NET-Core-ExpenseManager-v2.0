namespace ExpenseManager_v2._0.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static DataConstants.ApplicationUser;

    public class ApplicationUser : IdentityUser
    {
        [MaxLength(FullNameMaxlength)]
        public string FullName { get; set; }
        public IEnumerable<Income> Incomes { get; init; } = new List<Income>();
        public IEnumerable<Expense> Expenses { get; init; } = new List<Expense>();
        public IEnumerable<Credit> Credits { get; init; } = new List<Credit>();
        public IEnumerable<Saving> Savings { get; init; } = new List<Saving>();
        public IEnumerable<BorrowedLibrary> BorrowedLibrarys { get; init; } = new List<BorrowedLibrary>();
        public bool IsDeleted { get; set; }
    }
}