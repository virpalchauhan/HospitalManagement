using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class ForgotNewPassword
    {
        [Required(ErrorMessage = "Password is required")]
        public string? PasswordHash { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("PasswordHash", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
