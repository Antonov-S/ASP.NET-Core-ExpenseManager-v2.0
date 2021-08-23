namespace ExpenseManager_v2._0.Services.Saving
{
    using System.ComponentModel.DataAnnotations;

    public class AddSavingServiceModel
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        [Display(Name = "Desired Total")]
        public decimal DesiredTotal { get; init; }
    }
}
