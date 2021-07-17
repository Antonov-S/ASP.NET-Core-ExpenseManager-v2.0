namespace ExpenseManager_v2._0.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Income
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [Required]
        public string Notes { get; set; }

        //[Required]
        //public int UserId { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        //// public User User { get; init; }

        [Required]
        public int IncomeCategorysId { get; set; }
        public IncomeCategory IncomeCategory { get; init; }
    }
}
