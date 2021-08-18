namespace ExpenseManager_v2._0.Services.Saving
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    
    public class SavingService : ISavingService
    {
        private readonly ExpenseManagerDbContext data;

        public SavingService(ExpenseManagerDbContext data)
            => this.data = data;

        public AddContributionServiceModel GETAdd()
        {
            return new AddContributionServiceModel();
        }

        public void POSTAdd(AddContributionServiceModel addContributionModel, 
            int savingId, 
            string userId)
        {
            var contributionData = new ContributionToSaving
            {
                Amount = addContributionModel.Amount,
                Date = DateTime.Parse(addContributionModel.Date),
                IsDeleted = false,
                SavingId = savingId
            };

            data.Add(contributionData);
            var savingToBeIncreased = data.Savings.Where(i => i.UserId == userId).FirstOrDefault();
            savingToBeIncreased.Total += contributionData.Amount;
            data.SaveChanges();
        }

        public SavingServiceModel Total(string currentUserId)
        {
            var savingData = this.data
                .Savings
                .Where(c => c.UserId == currentUserId && c.IsDeleted != true)
                .Select(c => new SavingServiceModel
                {
                    Id = c.Id,
                    Total = c.Total
                })
                .FirstOrDefault();

            return savingData;
        }

        public IEnumerable<ListingContributionServiceModel> AllContributionToThisSaving(int savingId)
        {
            var contributions = this.data
                .ContributionToSavings
                .Where(p => p.SavingId == savingId && p.IsDeleted != true)
                .Select(p => new ListingContributionServiceModel
                {
                    Id = p.Id,
                    Date = p.Date.ToString("dd/MM/yyyy"),
                    Amount = p.Amount                    
                })
                .ToList();

            return contributions;
        }

        public bool DeleteContribution(int Id)
        {
            var deletedContribution = findContributionByContributionId(Id);

            if (deletedContribution == null)
            {
                return false;
            }

            deletedContribution.IsDeleted = true;

            var savingToBeRedusedId = deletedContribution.SavingId;
            if (IsSavingExist(savingToBeRedusedId))
            {
                var saving = FindSavingById(savingToBeRedusedId);
                saving.Total -= deletedContribution.Amount;
            }
            else
            {
                return false;
            }

            data.SaveChanges();
            return true;
        }

        public bool Edit(int id, string date, decimal amount, int savingId)
        {
            var editedData = findContributionByContributionId(id);

            if (editedData == null)
            {
                return false;
            }

            var differenceBetweenPreviousAndCurrentAmount = editedData.Amount - amount;

            editedData.Date = DateTime.Parse(date);
            editedData.Amount = amount;

            if (IsSavingExist(editedData.SavingId))
            {
                var saving = FindSavingById(editedData.SavingId);
                
                 saving.Total -= differenceBetweenPreviousAndCurrentAmount;             
            }
            else
            {
                return false;
            }

            this.data.SaveChanges();
            return true;
        }

        public bool IsUserExist(string currentUserId)
            => this.data
            .ApplicationUsers
            .Any(e => e.Id == currentUserId);

        public int FindSavingIdByUserId(string userId)
            => this.data
                .ApplicationUsers
                .Where(c => c.Id == userId)
                .Select(c => c.Saving.Id)
                .FirstOrDefault();

        public bool IsSavingExist(int savingId)
            => data
                .Savings
                .Any(e => e.Id == savingId && e.IsDeleted != true);

        public bool IsContributionExist(int contributionId)
            => data
                .ContributionToSavings
                .Any(e => e.Id == contributionId && e.IsDeleted != true);

        public Saving FindSavingById(int savingId)
            => this.data
                    .Savings
                    .Where(s => s.Id == savingId)
                    .FirstOrDefault();

        public ContributionToSaving findContributionByContributionId(int contributionId)
            => this.data
                .ContributionToSavings
                .Where(c => c.Id == contributionId && c.IsDeleted != true)
                .FirstOrDefault();

        public int FindSavingByContributionId(int ContributionId)
            => this.data
            .ContributionToSavings
            .Where(cts => cts.Id == ContributionId)
            .Select(cts => cts.SavingId)
            .FirstOrDefault();

        public AddContributionServiceModel FindContributionById(int contributionId)
            => this.data
            .ContributionToSavings
            .Where(c => c.Id == contributionId && c.IsDeleted != true)
            .Select(c => new AddContributionServiceModel
            {
                Id = c.Id,
                Date = c.Date.ToString("dd/MM/yyyy"),
                Amount = c.Amount,
                SavingId = c.SavingId
            })
            .FirstOrDefault();        
    }
}
