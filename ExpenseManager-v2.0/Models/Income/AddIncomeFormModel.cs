namespace ExpenseManager_v2._0.Models.Income
{
    using ExpenseManager_v2._0.Models.Income;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddIncomeFormModel
    {
        [Required]
        [StringLength(Income.NameMaxLength, MinimumLength = Income.NameMinLength)]
        public string Name { get; init; }

        [Required]
        [Display(Name = "Date")]
        public string IncomeDate { get; init; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public decimal Amount { get; init; }

        [Required]
        [StringLength(Expense.NotesMaxLength,
            MinimumLength = Income.NotesMinLength,
            ErrorMessage = "The field must be with a minimum length of {2}")]
        public string Notes { get; init; }


        [Display(Name = "Category")]
        public int IncomeCategoryId { get; init; }

        public IEnumerable<IncomeCategoryViewModel> IncomeCategories { get; set; }
    }
}
