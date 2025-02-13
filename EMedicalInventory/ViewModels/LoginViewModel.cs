using System.ComponentModel.DataAnnotations;

namespace EMedicalInventory.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is requiered.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is requiered.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool IsRemember {  get; set; }

    }
}
