using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Entity.Model
{

    [Table("AppointmentTable")]

    public class AppointmentTable
    {

        [Key]
        public int AppointmentId { get; set; }

        public int? PatientId { get; set; }

        public int? DoctorId { get; set; }

        public DateOnly? AppointmentDate { get; set; }

        public TimeOnly? AppointmentTime { get; set; }

        public int? DepartmentId { get; set; }

        public string? Reason { get; set; }

        public AppointmentStatusType? Status { get; set; }

        public DateOnly? SuggestedDate { get; set; }

        public TimeOnly? SuggestedTime { get; set; }

        public PatientResponseType? PatientResponse { get; set; }

        public DateTime? AppointmentBookDate { get; set; }
        


    }
}
