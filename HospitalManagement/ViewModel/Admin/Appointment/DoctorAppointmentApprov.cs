using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Admin.Appointment
{
    public class DoctorAppointmentApprovViewModel
    {

        [Required(ErrorMessage = "Please Select Appointment Date.")]
        public DateOnly? AppointmentDate { get; set; }

        [Required(ErrorMessage = "Please Select Appointment Time.")]
        public TimeOnly? AppointmentTime { get; set; }
        public int? AppointmentId { get; set; }





    }
}
