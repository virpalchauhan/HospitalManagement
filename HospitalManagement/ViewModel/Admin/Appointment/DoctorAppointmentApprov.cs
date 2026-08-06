using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Admin.Appointment
{
    public class DoctorAppointmentApprovViewModel
    {

        [Required(ErrorMessage = "Please select appointment date.")]
        public DateOnly? AppointmentDate { get; set; }

        [Required(ErrorMessage = "Please select appointment time.")]
        public TimeOnly? AppointmentTime { get; set; }
        public int? AppointmentId { get; set; }





    }
}
