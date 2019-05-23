using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YourDollarR2.Dtos;

namespace YourDollarR2.Core.Validators
{
    public class ValidFundAllocationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var budgetDto = (BudgetDto)validationContext.ObjectInstance;
            var allocatedFunds = ((decimal)value);
            var allotedFunds = budgetDto.AllotedFunds;

            if(allotedFunds < allocatedFunds)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"Allocated funds must be less than the budget's alloted funds";
        }
    }
}
