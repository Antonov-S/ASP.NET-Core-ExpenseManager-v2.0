namespace ExpenseManager_v2._0.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ExpenseManager_v2._0.Data.DataConstants;

    public class IncomeCategory
    {
        public int Id { get; init; }

        [Required]
        [MinLength(Categorys.NameMinLength)]
        [MaxLength(Categorys.NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Income> Incomes { get; init; } = new List<Income>();
    }
}
