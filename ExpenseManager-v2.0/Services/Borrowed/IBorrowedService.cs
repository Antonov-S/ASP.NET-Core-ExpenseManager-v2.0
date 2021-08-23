namespace ExpenseManager_v2._0.Services.Borrowed
{
    using System.Collections.Generic;

    public interface IBorrowedService
    {
        public AddItemServiceModel GETAdd();
        public bool POSTAdd(AddItemServiceModel addItemModel, string userId);
        public BorrowedLibraryServiceModel TotalItems(string currentUserId);
    }
}
