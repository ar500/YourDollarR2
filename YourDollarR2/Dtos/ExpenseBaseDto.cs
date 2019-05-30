using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourDollarR2.Dtos
{
    public class ExpenseBaseDto
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatePaid { get; set; }

        [Range(0, 10000)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal AmountSpent { get; set; } = 0.00M;

        public BudgetCategoryDto BudgetCategory { get; set; }

        public Guid? BudgetId { get; set; }
    }
}
