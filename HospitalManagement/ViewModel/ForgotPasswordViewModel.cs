using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required (ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

    }
}
