using ExpenseManager_v2._0.Data;


namespace ExpenseManager_v2._0.Services.Saving
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Saving;


    public class AddContributionServiceModel
    {
        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = ErrorMessageAmount)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        public int SavingId { get; set; }
    }
}
