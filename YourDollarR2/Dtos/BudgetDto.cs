﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YourDollarR2.Core.Services;
using YourDollarR2.Core.Validators;

namespace YourDollarR2.Dtos
{
    public class BudgetDto
    {
        public Guid Id { get; set; }
        #region Added By User
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
        public decimal AllottedFunds { get; set; }

        [DataType(DataType.Currency)]
        [ValidFundAllocation]
        public decimal AllocatedFunds { get; set; }

        public ICollection<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();
        #endregion
        #region Automatically calculated
        [DataType(DataType.Currency)]
        public decimal UnAllocatedFunds { get; set; }

        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; } = "removethis@soonest.opp";

        
        public IEnumerable<FundsInCategory> CategoryGroups { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        #endregion
    }
}
