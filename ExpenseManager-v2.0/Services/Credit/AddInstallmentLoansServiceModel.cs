namespace ExpenseManager_v2._0.Services.Credit
{
    using System.ComponentModel.DataAnnotations;
    using ExpenseManager_v2._0.Data.Models;

    public class AddInstallmentLoansServiceModel
    {
        public int Id { get; init; }

        [Required]
        public string Date { get; init; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public decimal Amount { get; init; }

        public int CreditId { get; init; }
        public Credit Credit { get; init; }
    }
}
