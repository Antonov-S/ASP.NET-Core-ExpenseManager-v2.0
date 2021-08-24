namespace ExpenseManager_v2._0.Services.Expense
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Expense;

    public class AddExpenseServiceModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required]
        [Display(Name = "Date")]
        public string ExpensDate { get; init; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = ErrorMessageAmount)]
        public decimal Amount { get; init; }

        [Required]
        [StringLength(NotesMaxLength,
            MinimumLength = NotesMinLength,
            ErrorMessage = ErrorMessageNotes)]
        public string Notes { get; init; }


        [Display(Name = "Category")]
        public int ExpenseCategoryId { get; init; }

        public IEnumerable<ExpenseCategoryServicesModel> ExpenseCategories { get; set; }
    }
}
