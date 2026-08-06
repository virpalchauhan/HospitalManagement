using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Client.Appointment
{
    public class PatientAppointment
    {

        [Required(ErrorMessage = "Please select department.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid department.")]
        public int? DepartmentId { get; set; }


        [Required(ErrorMessage = "Please select doctor.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select doctor.")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Please select Suggested appointment date.")]
        [DataType(DataType.Date)]
        public DateOnly? SuggestedDate { get; set; }

        [Required(ErrorMessage = "Please select Suggested appointment time.")]
        [DataType(DataType.Time)]
        public TimeOnly? SuggestedTime { get; set; }

        [Required(ErrorMessage = "Please enter reason.")]
        [StringLength(300, MinimumLength = 5,
         ErrorMessage = "Reason must be between 5 and 300 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,()\-]+$",
         ErrorMessage = "Reason contains invalid characters.")]
        public string? Reason { get; set; }

       


    }
}
