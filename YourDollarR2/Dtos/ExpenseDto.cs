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
        [DataType(DataType.Currency)]
        public decimal PayoutAmount { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string PayoutAccountNumber { get; set; }

        public BudgetCategoryDto BudgetCategory { get; set; }
    }
}
