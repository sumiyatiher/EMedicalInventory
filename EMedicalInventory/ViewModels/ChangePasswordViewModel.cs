using System.ComponentModel.DataAnnotations;

namespace EMedicalInventory.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z]).*$", ErrorMessage = "Password must contain at least one uppercase letter.")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
		[Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
