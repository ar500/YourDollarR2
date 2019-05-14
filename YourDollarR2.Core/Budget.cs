using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace YourDollarR2.Core
{
    public class Budget
    {
        public Guid Id { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public DateTime CycleStartDate { get; set; }

        public DateTime CycleEndDate { get; set; }

        public decimal Funds { get; set; } = 0;

        public string OwnerEmail { get; set; }
    }
}
