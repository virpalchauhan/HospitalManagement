using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Client.Patient
{
    public class VerifyOtpViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "OTP is required")]
        [RegularExpression(@"^[0-9]{6}$",
   ErrorMessage = "OTP must be exactly 6 digits")]
        public string? OTP { get; set; }

    }
}
