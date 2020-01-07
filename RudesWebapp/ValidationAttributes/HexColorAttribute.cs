using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RudesWebapp.ValidationAttributes
{
    public class HexColorAttribute : ValidationAttribute
    {
        private string _errorMessage;

        public HexColorAttribute()
        {
        }

        public HexColorAttribute(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var colorHexStr = (string) value;
            var valid = Regex.IsMatch(colorHexStr, "#[0-9a-fA-F]{6}");

            return valid
                ? ValidationResult.Success
                : new ValidationResult(_errorMessage ?? "Field has to have hex color format like '#123456'");
        }
    }
}