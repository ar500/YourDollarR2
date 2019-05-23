using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YourDollarR2.Core.Services;

namespace YourDollarR2.Dtos
{
    public class BudgetDto
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

        [DataType(DataType.Currency)]
        public decimal Funds { get; set; } = 0;

        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; } = "removethis@soonest.opp";

        public ICollection<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();

        public IEnumerable<FundsInCategory> CategoryGroups { get; set; }
    }
}
