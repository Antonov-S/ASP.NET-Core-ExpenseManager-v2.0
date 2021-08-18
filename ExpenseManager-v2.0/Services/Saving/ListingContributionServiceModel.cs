namespace ExpenseManager_v2._0.Services.Saving
{
    using System;

    public class ListingContributionServiceModel
    {
        public int Id { get; set; }
        public string Date { get; init; }
        public decimal Amount { get; init; }
        public int SavingId { get; init; }
    }
}
