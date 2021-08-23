namespace ExpenseManager_v2._0.Services.Borrowed
{
    using System;

    public class BorrowedItemsListingServiceModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public string Date { get; set; }

        public int BorrowedLibraryId { get; init; }
    }
}
