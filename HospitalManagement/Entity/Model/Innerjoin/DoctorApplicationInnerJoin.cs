using HospitalManagement.Entity.Model.Enums;

namespace HospitalManagement.Entity.Model.Innerjoin
{
    public class DoctorApplicationInnerJoin
    {
        public int DoctorApplicationsId { get; set; }



        public string? FirstName { get; set; }

        public string? LastName { get; set; }


        public GenderType Gender { get; set; }


        public DateOnly DateOfBirth { get; set; }


        public string? MobileNo { get; set; }


        public string? Email { get; set; }

        public int DepartmentId { get; set; }



        public string? ProfilePhotoPath { get; set; }


        public string? ResumePath { get; set; }



        public DateTime RequestDate { get; set; }


        public ApplicationStatusType ApplicationStatus { get; set; }

        public string DepartmentName { get; set; }


    }
}
