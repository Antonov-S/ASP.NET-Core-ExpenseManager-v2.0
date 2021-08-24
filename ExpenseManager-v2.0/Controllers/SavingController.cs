namespace ExpenseManager_v2._0.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;    
    using ExpenseManager_v2._0.Infrastructure;
    using ExpenseManager_v2._0.Services.Saving;

    using static WebConstants;

    public class SavingController : Controller
    {
        private readonly ISavingService savingService;

        public SavingController(ISavingService savingService)
            => this.savingService = savingService;

        [Authorize]
        public IActionResult AddSaving()
            => View(savingService.GETAddSaving());

        [HttpPost]
        [Authorize]
        public IActionResult AddSaving(AddSavingServiceModel saving)
        {
            if (!ModelState.IsValid)
            {
                return View(saving);
            }

            var userId = this.User.GetId();

            savingService.POSTAddSaving(saving, userId);

            TempData[GlobalMessageKey] = "Your saving was added successfuly!";
            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult AddContribution()
            => View(savingService.GETAddContribution());

        [HttpPost]
        [Authorize]
        public IActionResult AddContribution(AddContributionServiceModel contribution, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(contribution);
            }
            var currentUserId = this.User.GetId();
            savingService.POSTAddContribution(contribution, Id, currentUserId);

            TempData[GlobalMessageKey] = "Your contribution on this saving was added successfuly!";

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult All()
        {
            var currentUserId = this.User.GetId();

            var savingsForThisUser = this.savingService.All(currentUserId);

            return View(savingsForThisUser);
        }

        [Authorize]
        public IActionResult EditSaving(int id)
        {
            var userId = this.User.GetId();

            var savingToBeEdited = this.savingService.Details(id);

            if (savingToBeEdited.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new AddSavingServiceModel
            {
                Id = savingToBeEdited.Id,
                Name = savingToBeEdited.Name,
                DesiredTotal = savingToBeEdited.DesiredTotal,

            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditSaving(int id, AddSavingServiceModel savingToBeEdited)
        {
            if (!ModelState.IsValid)
            {
                return View(savingToBeEdited);
            }

            var edited = this.savingService.EditSaving(id, savingToBeEdited.Name, savingToBeEdited.DesiredTotal);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = "Your saving was edited successfuly!";
            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var exists = savingService.IsSavingExist(id);

            if (!exists)
            {
                return NotFound();
            }

            var detailedSaving = savingService.Details(id);

            return View(detailedSaving);
        }

        [Authorize]
        public IActionResult DeleteSaving(int id)
        {
            var exists = savingService.IsSavingExist(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = savingService.DeleteSaving(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
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
        public IActionResult DeleteContribution(int id)
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
        public IActionResult EditContribution(int id)
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
        public IActionResult EditContribution(AddContributionServiceModel contributionModel, int id)
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

            TempData[GlobalMessageKey] = "Your contribution was edited successfuly!";
            return RedirectToAction(nameof(Contributions));
        }


    }
}