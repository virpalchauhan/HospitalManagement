using HospitalManagement.Entity.Model.Enums;

namespace HospitalManagement.Entity.Model.Innerjoin
{
    public class AppointmentPatientDepartmentInnerJoin
    {
        public int AppointmentId { get; set; }

        public int? PatientId { get; set; }

        public int? DoctorId { get; set; }

        public DateOnly? AppointmentDate { get; set; }

        public TimeOnly? AppointmentTime { get; set; }

        public int? DepartmentId { get; set; }

        public string? Reason { get; set; }

        public AppointmentStatusType Status { get; set; }

        public DateOnly? SuggestedDate { get; set; }

        public TimeOnly? SuggestedTime { get; set; }

        //public PatientResponseType? PatientResponse { get; set; }

        public DateTime? AppointmentBookDate { get; set; }

        // Inner Join Fields
        public string? PatientName { get; set; }

        public string? DepartmentName { get; set; }
    }
}