using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using YourDollarR2.Dtos;

namespace YourDollarR2.Core.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidFundAllocationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var budgetDto = (BudgetDto)validationContext.ObjectInstance;
            var allocatedFunds = ((decimal)value);
            var allotedFunds = budgetDto.AllottedFunds;

            if (allotedFunds < allocatedFunds)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"Planned funds must be less than the budget's alloted funds";
        }
    }
}
