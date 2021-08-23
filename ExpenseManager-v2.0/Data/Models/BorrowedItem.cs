namespace ExpenseManager_v2._0.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.BorrowedItem;

    public class BorrowedItem
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Owner { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public bool IsReturned { get; set; }

        public int BorrowedLibraryId { get; init; }
        public BorrowedLibrary BorrowedLibrary { get; init; }

    }
}
