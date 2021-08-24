using ExpenseManager_v2._0.Data;

namespace ExpenseManager_v2._0.Services.Saving
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.ContributionToSaving;

    public class AddSavingServiceModel
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        [Range(00001, int.MaxValue, ErrorMessage = ErrorMessageAmount)]
        [Display(Name = "Desired Total")]
        public decimal DesiredTotal { get; init; }
    }
}
