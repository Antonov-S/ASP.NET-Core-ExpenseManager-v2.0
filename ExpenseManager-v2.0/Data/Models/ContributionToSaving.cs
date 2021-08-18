namespace ExpenseManager_v2._0.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ContributionToSaving
    {
        public int Id { get; init; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public int SavingId { get; init; }
        public Saving Saving { get; init; }
    }
}
