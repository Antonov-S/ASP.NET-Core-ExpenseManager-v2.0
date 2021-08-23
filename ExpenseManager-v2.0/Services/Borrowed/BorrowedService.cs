namespace ExpenseManager_v2._0.Services.Borrowed
{
    using System;    
    using System.Linq;
    using System.Collections.Generic;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;   

    public class BorrowedService : IBorrowedService
    {
        private readonly ExpenseManagerDbContext data;

        public BorrowedService(ExpenseManagerDbContext data)
            => this.data = data;

        public AddItemServiceModel GETAdd()
        {
            return new AddItemServiceModel();
        }

        public bool POSTAdd(AddItemServiceModel addItemModel, string userId)
        {
            throw new NotImplementedException();
        }

        //public bool POSTAdd(AddItemServiceModel addItemModel, string userId)
        //{
        //    var currentBorrowedLibraryId = this.data
        //        .ApplicationUsers
        //        .Where(c => c.Id == userId)
        //        .Select(c => c.BorrowedLibrary.Id)
        //        .FirstOrDefault();

        //    if (currentBorrowedLibraryId == 0)
        //    {
        //        return false;
        //    }

        //    var itemData = new BorrowedItem
        //    {
        //        Name = addItemModel.Name,
        //        Owner = addItemModel.Owner,
        //        Date = DateTime.Parse(addItemModel.Date),
        //        IsDeleted = false,
        //        IsReturned = false,
        //        BorrowedLibraryId = currentBorrowedLibraryId
        //    };         

        //    data.Add(itemData);
        //    data.SaveChanges();

        //    return true;
        //}

        public BorrowedLibraryServiceModel TotalItems(string currentUserId)
        {
            var borrowedData = this.data
                .BorrowedLibraries
                .Where(c => c.UserId == currentUserId)                
                .FirstOrDefault();

            return null;
        }
    }
}
