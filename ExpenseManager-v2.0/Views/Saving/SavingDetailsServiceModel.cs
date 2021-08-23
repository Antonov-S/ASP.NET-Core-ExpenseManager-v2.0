namespace ExpenseManager_v2._0.Views.Saving
{
    using ExpenseManager_v2._0.Services.Saving;

    public class SavingDetailsServiceModel : AddSavingServiceModel
    {
        public string UserId { get; init; }
        public decimal CurrentTotal { get; set; }
    }
}
