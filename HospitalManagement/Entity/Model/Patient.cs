using HospitalManagement.Entity.Model.Enums;

namespace HospitalManagement.Entity.Model
{
    public class Patient
    {

        public int PatientId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public GenderType? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? MobileNo { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? ProfilePhotoPath { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? StateName { get; set; }

        public string? Pincode { get; set; }

        public BloodGroupType? BloodGroup { get; set; }

        public DateTime? CreateDate { get; set; }

    }
}
