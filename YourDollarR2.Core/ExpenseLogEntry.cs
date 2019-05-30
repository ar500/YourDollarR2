using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YourDollarR2.Core
{
    public class ExpenseLogEntry : ExpenseBase
    {
        [MaxLength(50)]
        public string ShortName { get; set; }

        [MaxLength(200)]
        public string Remarks { get; set; }
    }
}
