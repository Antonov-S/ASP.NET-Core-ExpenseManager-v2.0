namespace ExpenseManager_v2._0.Services.Saving
{
    using System.Collections.Generic;
    using ExpenseManager_v2._0.Data.Models;

    public interface ISavingService
    {
        AddContributionServiceModel GETAdd();
        void POSTAdd(AddContributionServiceModel addContributionModel, int savingId, string userId);
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
