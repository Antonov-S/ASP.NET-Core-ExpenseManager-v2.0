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
        [Range(00001, int.MaxValue, ErrorMessage = ErrorMessageAmount)]
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
        [Range(00001, int.MaxValue, ErrorMessage = ErrorMessageAmount)]
        public decimal Total { get; init; }

        [StringLength(NotesMaxLength,
            MinimumLength = NotesMinLength,
            ErrorMessage = ErrorMessageNotes)]
        public string Notes { get; init; }
    }
}
