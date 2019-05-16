using System;
using System.ComponentModel.DataAnnotations;

namespace YourDollarR2.Dtos
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public decimal PayoutAmount { get; set; }

        [Required]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(100)]
        public string PayoutAccountNumber { get; set; }

        public BudgetCategoryDto BudgetCategory { get; set; }
    }
}
