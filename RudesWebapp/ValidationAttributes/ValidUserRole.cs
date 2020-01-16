using System.ComponentModel.DataAnnotations;
using RudesWebapp.Models;

namespace RudesWebapp.ValidationAttributes
{
    public class ValidUserRole : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var roleName = (string) value;

            return !Roles.IsValidUserRoleName(roleName)
                ? new ValidationResult("User role invalid")
                : ValidationResult.Success;
        }
    }
}