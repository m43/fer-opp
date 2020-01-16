using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace RudesWebapp.ValidationAttributes
{
    public class SqlDateTimeFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            
            var date = (DateTime) value;

            if (date.Date < (DateTime) SqlDateTime.MinValue)
                return new ValidationResult("Date must be after " + SqlDateTime.MinValue.Value.ToShortDateString());

            if (date.Date > (DateTime) SqlDateTime.MaxValue)
                return new ValidationResult("Date must be before " + SqlDateTime.MaxValue.Value.ToShortDateString());

            return ValidationResult.Success;
        }
    }
}