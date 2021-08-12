namespace ExpenseManager_v2._0.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ExpenseManager_v2._0.Infrastructure;
    using ExpenseManager_v2._0.Services.Credit;
    using Microsoft.AspNetCore.Authorization;

    public class CreditController : Controller
    {
        private readonly ICreditService creditService;

        public CreditController(ICreditService creditService)
            => this.creditService = creditService;

        [Authorize]
        public IActionResult Add()
            => View(creditService.GETAdd());

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCreditServiceModel credit)
        {
            if (!ModelState.IsValid)
            {
                return View(credit);
            }

            var userId = this.User.GetId();

            creditService.POSTAdd(credit, userId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All()
        {
            var currentUserId = this.User.GetId();

            var creditForThisUser = this.creditService.All(currentUserId);

            return View(creditForThisUser);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            var creditToBeEdited = this.creditService.Details(id);

            if (creditToBeEdited.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new AddCreditServiceModel
            {
                Id = creditToBeEdited.Id,
                Name = creditToBeEdited.Name,
                AmountOfMonthlyInstallment = creditToBeEdited.AmountOfMonthlyInstallment,
                NumberOfInstallmentsRemaining = creditToBeEdited.NumberOfInstallmentsRemaining,
                UnpaidFees = creditToBeEdited.UnpaidFees,
                MaturityDate = creditToBeEdited.MaturityDate,
                Total = creditToBeEdited.Total,
                Notes = creditToBeEdited.Notes
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, AddCreditServiceModel creditToBeEdited)
        {
            if (!ModelState.IsValid)
            {
                return View(creditToBeEdited);
            }

            var edited = this.creditService.Edit(
                id,
                creditToBeEdited.Name,
                creditToBeEdited.AmountOfMonthlyInstallment,
                creditToBeEdited.NumberOfInstallmentsRemaining,
                creditToBeEdited.UnpaidFees,
                creditToBeEdited.MaturityDate,
                creditToBeEdited.Total,
                creditToBeEdited.Notes);

            if (!edited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var exists = creditService.IsCreditExist(id);

            if (!exists)
            {
                return NotFound();
            }

            var detailedExpense = creditService.Details(id);

            return View(detailedExpense);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var exists = creditService.IsCreditExist(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = creditService.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult MakePayment()
            => View(new AddInstallmentLoansServiceModel());

        [HttpPost]
        [Authorize]
        public IActionResult MakePayment(AddInstallmentLoansServiceModel installmentLoanModel, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(installmentLoanModel);
            }

            creditService.POSTMakePayment(installmentLoanModel, Id);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult PaymentsOnCredit(int Id)
        {
            var exists = creditService.IsCreditExist(Id);

            if (!exists)
            {
                return NotFound();
            }

            var payments = creditService.AllPaymentsOnCredit(Id);

            if (payments == null)
            {
                return BadRequest();
            }

            return View(payments);
        }

    }
}
