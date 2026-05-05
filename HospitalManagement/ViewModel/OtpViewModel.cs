using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class OtpViewModel
    {


        [Required(ErrorMessage = "OTP is required.")]

        [StringLength(6, MinimumLength = 6,
             ErrorMessage = "OTP must be exactly 6 digits.")]

        [RegularExpression("^[0-9]{6}$",
             ErrorMessage = "OTP must contain only numbers (0–9).")]

        public string? Otp { get; set; }


    }
}
