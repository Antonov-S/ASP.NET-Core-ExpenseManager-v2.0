namespace ExpenseManager_v2._0.Services.Saving
{
    using System.ComponentModel.DataAnnotations;

    public class SavingServiceListingModel
    {
        public int Id { get; init; }
        public string Name { get; set; }

        [Display(Name = "Desired Total")]
        public decimal DesiredTotal { get; set; }

        [Display(Name = "Current Total")]
        public decimal CurrentTotal { get; set; }

        public string UserId { get; init; }
    }
}
