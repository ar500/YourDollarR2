using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourDollarR2.Dtos
{
    public class BudgetForCreateOrEditDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CycleStartDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CycleEndDate { get; set; } = DateTime.Now.AddDays(5);

        [Required]
        [DataType(DataType.Currency)]
        public decimal AllottedFunds { get; set; } = 0;

        [Required]
        public IEnumerable<Guid> ReturnedExpenseIds { get; set; } = new List<Guid>();

        [Required]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; } = "fixThis@Soon.Com";

        public MultiSelectList ExpenseMultiSelectList { get; set; }

        public IList<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
