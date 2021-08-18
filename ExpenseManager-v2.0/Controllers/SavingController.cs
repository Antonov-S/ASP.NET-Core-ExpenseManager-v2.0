namespace ExpenseManager_v2._0.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ExpenseManager_v2._0.Infrastructure;
    using ExpenseManager_v2._0.Services.Saving;

    public class SavingController : Controller
    {
        private readonly ISavingService savingService;

        public SavingController(ISavingService savingService)
            => this.savingService = savingService;

        [Authorize]
        public IActionResult Add()
            => View(savingService.GETAdd());

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddContributionServiceModel contribution, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(contribution);
            }
            var currentUserId = this.User.GetId();
            savingService.POSTAdd(contribution, Id, currentUserId);

            return RedirectToAction("Total", "Saving");
        }

        [Authorize]
        public IActionResult Total()
        {
            var currentUserId = this.User.GetId();

            var currentSavingTotal = savingService.Total(currentUserId);

            return View(currentSavingTotal);
        }

        [Authorize]
        public ActionResult Contributions(int id)
        {
            var exists = savingService.IsSavingExist(id);

            if (!exists)
            {
                return NotFound();
            }
            var contributions = savingService.AllContributionToThisSaving(id);
            return View(contributions);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var exists = savingService.IsContributionExist(id);
            if (!exists)
            {
                return NotFound();
            }

            var result = savingService.DeleteContribution(id);
            if (!result)
            {
                return BadRequest();
            }

            var Id = savingService.FindSavingByContributionId(id);
            return RedirectToAction(nameof(Contributions), new { id = Id });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            var contributionToBeEdited = this.savingService.FindContributionById(id);

            return View(new AddContributionServiceModel 
            { 
                Id = contributionToBeEdited.Id,
                Date = contributionToBeEdited.Date,
                Amount = contributionToBeEdited.Amount,
                SavingId = contributionToBeEdited.SavingId
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(AddContributionServiceModel contributionModel, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(contributionModel);
            }

            var edited = this.savingService.Edit(
                contributionModel.Id, 
                contributionModel.Date, 
                contributionModel.Amount, 
                contributionModel.SavingId);

            if (!edited)
            {
                return BadRequest();
            }

            //var Id = savingService.FindSavingByContributionId(contributionModel.Id);
            //return RedirectToAction(nameof(Contributions), new { id = Id });
            return RedirectToAction(nameof(Total));
        }


    }
}