using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Client.Patient
{
    public class SendOtpViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        [StringLength(100)]
        public string? Email { get; set; }

    }
}
