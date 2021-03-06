using System.ComponentModel.DataAnnotations;
using RudesWebapp.ValidationAttributes;

namespace RudesWebapp.Dtos
{
    public class ManageUserDTO
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide First Name")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Last Name")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide User Role")]
        [Display(Name = "User Role")]
        [ValidUserRole]
        public string Role { get; set; }
    }

    public class UpdateUserRoleDTO
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide User Role")]
        [Display(Name = "User Role")]
        [ValidUserRole]
        public string Role { get; set; }
    }
}