namespace ExpenseManager_v2._0.Services.Saving
{
    using System.Collections.Generic;
    using ExpenseManager_v2._0.Data.Models;
    using ExpenseManager_v2._0.Views.Saving;

    public interface ISavingService
    {
        public AddSavingServiceModel GETAddSaving();
        public void POSTAddSaving(AddSavingServiceModel addSavingModel, string userId);
        public AddContributionServiceModel GETAddContribution();
        public IEnumerable<SavingServiceListingModel> All(string userId);
        public SavingDetailsServiceModel Details(int savingId);
        public bool EditSaving(int id, string name, decimal desiredTotal);
        public bool DeleteSaving(int savingId);
        public Saving FindSaving(int savingId);
        public void POSTAddContribution(AddContributionServiceModel addContributionModel, int savingId, string userId);
        public SavingServiceModel Total(string currentUserId);
        public bool IsUserExist(string currentUserId);
        public int FindSavingIdByUserId(string userId);
        public bool IsSavingExist(int savingId);
        public IEnumerable<ListingContributionServiceModel> AllContributionToThisSaving(int savingId);
        public bool IsContributionExist(int contributionId);
        bool DeleteContribution(int Id);
        public Saving FindSavingById(int savingId);
        public ContributionToSaving findContributionByContributionId(int contributionId);
        public int FindSavingByContributionId(int ContributionId);
        public AddContributionServiceModel FindContributionById(int contributionId);
        public bool Edit(int id, string date, decimal amount, int savingId);
    }
}
