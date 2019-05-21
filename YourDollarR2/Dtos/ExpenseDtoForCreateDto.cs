using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourDollarR2.Dtos
{
    public class ExpenseDtoForCreateDto
    {
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

        [Required]
        public Guid ReturnedCategoryId { get; set; }

        public IList<BudgetCategoryDto> Categories { get; set; }

        public BudgetCategoryDto BudgetCategory { get; set; }
    }
}
