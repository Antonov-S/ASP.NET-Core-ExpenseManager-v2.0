namespace ExpenseManager_v2._0.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BorrowedLibrary
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        public int Total { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public string UserId { get; init; }
        public ApplicationUser User { get; init; }

        public IEnumerable<BorrowedItem> BorrowedItems { get; init; } = new List<BorrowedItem>();
    }
}
