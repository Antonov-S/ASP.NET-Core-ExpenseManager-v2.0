namespace ExpenseManager_v2._0.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Saving
    {
        public int Id { get; init; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public string UserId { get; init; }
        public ApplicationUser User { get; init; }

        public IEnumerable<ContributionToSaving> ContributionToSavings { get; init; } = new List<ContributionToSaving>();
    }
}
