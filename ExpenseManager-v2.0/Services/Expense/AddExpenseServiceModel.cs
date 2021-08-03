namespace ExpenseManager_v2._0.Services.Expense
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddExpenseServiceModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(Expense.NameMaxLength, MinimumLength = Expense.NameMinLength)]
        public string Name { get; init; }

        [Required]
        [Display(Name = "Date")]
        public string ExpensDate { get; init; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public decimal Amount { get; init; }

        [Required]
        [StringLength(Expense.NotesMaxLength,
            MinimumLength = Expense.NotesMinLength,
            ErrorMessage = "The field must be with a minimum length of {2}")]
        public string Notes { get; init; }


        [Display(Name = "Category")]
        public int ExpenseCategoryId { get; init; }

        public IEnumerable<ExpenseCategoryServicesModel> ExpenseCategories { get; set; }
    }
}
