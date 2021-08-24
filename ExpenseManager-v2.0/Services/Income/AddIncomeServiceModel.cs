namespace ExpenseManager_v2._0.Services.Income
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Income;

    public class AddIncomeServiceModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required]
        [Display(Name = "Date")]
        public string IncomeDate { get; init; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = ErrorMessageAmount)]
        public decimal Amount { get; init; }

        [Required]
        [StringLength(NotesMaxLength,
            MinimumLength = NotesMinLength,
            ErrorMessage = ErrorMessageNotes)]
        public string Notes { get; init; }


        [Display(Name = "Category")]
        public int IncomeCategoryId { get; init; }

        public IEnumerable<IncomeCategoryServicesModel> IncomeCategories { get; set; }
    }
}
