using System;
using System.ComponentModel.DataAnnotations;
using YourDollarR2.Core.Validators;

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
        [ValidDecimal]
        [DataType(DataType.Currency)]
        public decimal AmountPlanned { get; set; }

        [ValidDecimal]
        [DataType(DataType.Currency)]
        public decimal AmountSpent { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string PayoutAccountNumber { get; set; }

        [Required]
        public Guid ReturnedCategoryId { get; set; }

        public BudgetCategoryDto BudgetCategory { get; set; }
    }
}
