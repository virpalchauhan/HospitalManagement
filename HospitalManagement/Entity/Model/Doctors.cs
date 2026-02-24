using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Entity.Model
{
    [Table("Doctors")]
    public class Doctors
    {
        [Key]
        public int DoctorId { get; set; }

       
        public string? FirstName { get; set; }

      
        public string? LastName { get; set; }

      
        public GenderType Gender { get; set; }

       
        public DateOnly DateOfBirth { get; set; }

      
        public string? MobileNo { get; set; }

     
        public string? Email { get; set; }

     
        public int DepartmentId { get; set; }

       
        public string? ProfilePhotoPath { get; set; }

       
        public decimal SalaryAmount { get; set; }

       
        public DateTime? JoiningDate { get; set; }

      
        public string? PasswordHash { get; set; }

        public byte AccountStatus { get; set; }

        public bool OfferLetterSent { get; set; }

        public DateTime CreatedDate { get; set; }

     
       
    }
}
