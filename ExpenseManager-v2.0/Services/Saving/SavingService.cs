namespace ExpenseManager_v2._0.Services.Saving
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;
    using ExpenseManager_v2._0.Views.Saving;

    public class SavingService : ISavingService
    {
        private readonly ExpenseManagerDbContext data;

        public SavingService(ExpenseManagerDbContext data)
            => this.data = data;

        public AddSavingServiceModel GETAddSaving()
        {
            return new AddSavingServiceModel();
        }

        public void POSTAddSaving(AddSavingServiceModel addSavingModel, string userId)
        {
            var savingData = new Saving
            {
                Name = addSavingModel.Name,
                DesiredTotal = addSavingModel.DesiredTotal,
                IsDeleted = false,
                UserId = userId
            };

            data.Add(savingData);
            data.SaveChanges();
        }


        public AddContributionServiceModel GETAddContribution()
        {
            return new AddContributionServiceModel();
        }

        public void POSTAddContribution(AddContributionServiceModel addContributionModel,
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
            data.SaveChanges();
        }

        public IEnumerable<SavingServiceListingModel> All(string userId)
        {
            var savings = this.data
                .Savings
                .Where(c => c.UserId == userId && c.IsDeleted != true)
                .Select(c => new SavingServiceListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    DesiredTotal = c.DesiredTotal,
                    UserId = c.UserId
                })
                .ToList();

            return savings;
        }

        public SavingDetailsServiceModel Details(int savingId)
            => this.data
              .Savings
              .Where(c => c.Id == savingId && c.IsDeleted != true)
              .Select(c => new SavingDetailsServiceModel
              {
                  Id = c.Id,
                  Name = c.Name,
                  DesiredTotal = c.DesiredTotal,
                  CurrentTotal = c.CurrentTotal,
                  UserId = c.UserId
              })
              .FirstOrDefault();

        public bool EditSaving(int id, string name, decimal desiredTotal)
        {
            var editedSaving = this.data.Savings.Find(id);

            if (editedSaving == null || editedSaving.IsDeleted == true)
            {
                return false;
            }

            editedSaving.Name = name;
            editedSaving.DesiredTotal = desiredTotal;            

            this.data.SaveChanges();

            return true;
        }

        public bool DeleteSaving(int savingId)
        {
            var deletedSaving = FindSaving(savingId);

            if (deletedSaving == null || deletedSaving.IsDeleted == true)
            {
                return false;
            }

            deletedSaving.IsDeleted = true;

            data.SaveChanges();
            return true;
        }

        public SavingServiceModel Total(string currentUserId)
        {
            var savingData = this.data
                .Savings
                .Where(c => c.UserId == currentUserId && c.IsDeleted != true)
                .Select(c => new SavingServiceModel
                {
                    Id = c.Id,
                    Total = c.CurrentTotal
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
                saving.CurrentTotal -= deletedContribution.Amount;
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

                saving.CurrentTotal -= differenceBetweenPreviousAndCurrentAmount;
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

        //public int FindSavingIdByUserId(string userId)
        //    => this.data
        //        .ApplicationUsers
        //        .Where(c => c.Id == userId)
        //        .Select(c => c.Saving.Id)
        //        .FirstOrDefault();

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

        public int FindSavingIdByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Saving FindSaving(int savingId)
            => this.data
            .Savings.Find(savingId);
    }
}
