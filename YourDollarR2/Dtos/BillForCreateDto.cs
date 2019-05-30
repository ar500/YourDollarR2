using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YourDollarR2.Dtos
{
    public class BillForCreateDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatePaid { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal AmountPlanned { get; set; }

        [Range(0, 10000)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal AmountSpent { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string PayoutAccountNumber { get; set; }

        [Required]
        public Guid ReturnedCategoryId { get; set; }

        public IList<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public BudgetCategoryDto BudgetCategory { get; set; }

        public Guid? BudgetId { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
