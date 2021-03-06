﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        public decimal AllottedFunds { get; set; } = 0;

        [Required]
        public IEnumerable<Guid> ReturnedBillIds { get; set; } = new List<Guid>();

        [Required]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; } = "fixThis@Soon.Com";

        public MultiSelectList BillsMultiSelectList { get; set; }

        public IList<BillDto> Bills { get; set; } = new List<BillDto>();

        public BillForCreateDto Bill { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
