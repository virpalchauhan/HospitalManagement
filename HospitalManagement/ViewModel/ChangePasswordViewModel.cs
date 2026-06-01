using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current Password is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]

        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",

            ErrorMessage = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number and one special character"
        )]

        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]

        [Compare("NewPassword",
            ErrorMessage = "Password does not match")]

        public string ConfirmPassword { get; set; }

    }
}
