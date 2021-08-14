namespace ExpenseManager_v2._0.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Expense;

    public class Expense
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        public string Notes { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public int ExpenseCategoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; init; }

        public string UserId { get; set; }
        public ApplicationUser User { get; init; }
    }
}
