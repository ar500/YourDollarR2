using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourDollarR2.Dtos
{
    public class BudgetDto
    {
        public Guid Id { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-mm}", ApplyFormatInEditMode = true)]
        public DateTime CycleStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-mm}", ApplyFormatInEditMode = true)]
        public string CycleEndDate { get; set; }

        public decimal Funds { get; set; } = 0;

        public string OwnerEmail { get; set; } = "removethis@soonest.opp";
    }
}
