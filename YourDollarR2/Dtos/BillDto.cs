using System;
using System.ComponentModel.DataAnnotations;
using YourDollarR2.Core.Validators;

namespace YourDollarR2.Dtos
{
    public class BillDto : ExpenseBaseDto
    {
        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [Required]
        [Range(0, 10000)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal AmountPlanned { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string PayoutAccountNumber { get; set; }

        [Required]
        public Guid ReturnedCategoryId { get; set; }
    }
}
