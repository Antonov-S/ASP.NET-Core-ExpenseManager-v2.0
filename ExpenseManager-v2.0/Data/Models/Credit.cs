namespace ExpenseManager_v2._0.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Credit;

    public class Credit
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal AmountOfMonthlyInstallment { get; set; }

        [Required]
        public int NumberOfInstallmentsRemaining { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal UnpaidFees { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime MaturityDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }

        public string Notes { get; set; }

        [Required]
        public string UserId { get; init; }
        public ApplicationUser User { get; init; }

        public IEnumerable<InstallmentLoan> InstallmentLoans { get; init; } = new List<InstallmentLoan>();
    }
}
