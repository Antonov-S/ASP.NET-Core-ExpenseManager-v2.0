namespace ExpenseManager_v2._0.Services.Credit
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Credit;

    public class AddCreditServiceModel
    {
        public int Id { get; init; }

        [Required]
        [Display(Name = "Credit Name")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required]
        [Display(Name = "Amount of Monthly Installment")]
        [Range(00001, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public decimal AmountOfMonthlyInstallment { get; init; }

        [Required]
        [Display(Name = "Number Of Installments Remaining")]
        public int NumberOfInstallmentsRemaining { get; set; }

        [Required]
        [Display(Name = "Unpaid fees")]
        public decimal UnpaidFees { get; init; }

        [Required]
        [Display(Name = "Maturity date")]
        public string MaturityDate { get; init; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public decimal Total { get; init; }

        [StringLength(NotesMaxLength,
            MinimumLength = NotesMinLength,
            ErrorMessage = "The field must be with a minimum length of {2}")]
        public string Notes { get; init; }
    }
}
