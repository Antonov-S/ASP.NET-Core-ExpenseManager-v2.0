namespace ExpenseManager_v2._0.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ExpenseManager_v2._0.Services.Users;

    public class UsersController : AdminController
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        public IActionResult All() => View(userService.GetAllUsers());

        
        public IActionResult DeleteUser(string Id)
        {
            var exists = userService.IsUserExist(Id);

            if (!exists)
            {
                return NotFound();
            }

            var result = userService.DeleteUser(Id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
