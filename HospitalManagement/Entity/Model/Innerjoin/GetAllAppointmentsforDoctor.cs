namespace HospitalManagement.Entity.Model.Innerjoin
{
    public class GetAllAppointmentsforDoctor
    {
        public int? AppointmentId { get; set; }

        public string? PatientName { get; set; }

        public int? DoctorNurceId { get; set; }

        public string? DepartmentName { get; set; }


        public DateTime? AppointmentBookDate { get; set; }

        public int? PatientId { get; set; }


    }
}
