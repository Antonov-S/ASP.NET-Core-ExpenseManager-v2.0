namespace ExpenseManager_v2._0.Services.Borrowed
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static ExpenseManager_v2._0.Data.DataConstants.BorrowedItem;

    public class AddItemServiceModel
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Owner { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public bool IsReturned { get; set; }

        public int BorrowedLibraryId { get; init; }
    }
}
