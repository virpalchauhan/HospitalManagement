using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class ForgotNewPassword
    {
        [Required(ErrorMessage = "Password is required")]

        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, number, and special character."
        )]

        public string? PasswordHash { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]

        [Compare("PasswordHash", ErrorMessage = "Passwords do not match")]

        public string? ConfirmPassword { get; set; }
    }
    }
