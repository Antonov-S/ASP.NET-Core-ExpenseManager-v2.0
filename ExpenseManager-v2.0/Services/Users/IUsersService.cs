namespace ExpenseManager_v2._0.Services.Users
{
    using System.Collections.Generic;
    using ExpenseManager_v2._0.Data.Models;

    public interface IUsersService
    {
        public IEnumerable<UserListingModel> GetAllUsers();
        public bool IsUserExist(string userId);
        public bool DeleteUser(string userId);
        public ApplicationUser FindUser(string userId);
    }
}
