namespace ExpenseManager_v2._0.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class InstallmentLoan
    {
        public int Id { get; init; }

        [Required]
        public string Date { get; init; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; init; }

        [Required]
        public bool IsDeleted { get; set; }

        public int CreditId { get; init; }
        public Credit Credit { get; init; }
    }
}
