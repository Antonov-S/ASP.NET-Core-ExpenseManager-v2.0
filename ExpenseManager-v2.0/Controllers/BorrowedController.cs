namespace ExpenseManager_v2._0.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ExpenseManager_v2._0.Services.Borrowed;
    using Microsoft.AspNetCore.Authorization;
    using ExpenseManager_v2._0.Infrastructure;

    public class BorrowedController : Controller
    {
        private readonly IBorrowedService borrowedService;

        public BorrowedController(IBorrowedService borrowedService)
            => this.borrowedService = borrowedService;


        [Authorize]
        public IActionResult AddItem()
            => View(borrowedService.GETAdd());

        [HttpPost]
        [Authorize]
        public IActionResult AddItem(AddItemServiceModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            var currentUserId = this.User.GetId();            
            var result = borrowedService.POSTAdd(item, currentUserId);

            if (!result)
            {
                BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult TotalItems()
        {
            var currentUserId = this.User.GetId();

            var currentBorrowedLibrary = borrowedService.TotalItems(currentUserId);

            return View(currentBorrowedLibrary);
        }
    }
}
