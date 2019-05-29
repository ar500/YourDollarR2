using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YourDollarR2.Core.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ValidDecimalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(base.IsValid(value, validationContext) != ValidationResult.Success)
            {
                return new ValidationResult(GetErrorMessage());
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false;

            if (!decimal.TryParse(value.ToString(), out decimal result))
            {
                return false;
            }
            else if (result >= decimal.MaxValue || result <= decimal.MinValue)
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        private string GetErrorMessage()
        {
            return $"You must supply a valid decimal number.";
        }
    }
}
