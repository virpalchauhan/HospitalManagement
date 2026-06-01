using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Entity.Model
{
    [Table("DoctorsAndNurse")]
    public class DoctorsAndNurse
    {
        [Key]
        public int DoctorNurceId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        // tinyint
        public GenderType Gender { get; set; }

        // date
        public DateOnly DateOfBirth { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }

        public int DepartmentId { get; set; }

        // NULL allow
        public string? ProfilePhotoPath { get; set; }

        // decimal(10,2)
        [Column(TypeName = "decimal(10,2)")]
        public decimal SalaryAmount { get; set; }

        public DateTime JoiningDate { get; set; }

        public string PasswordHash { get; set; }

        // tinyint
        public DoctorNurseStatusType AccountStatus { get; set; }

        // tinyint
        public DoctorNurseOfferletterSendType OfferLetterSent { get; set; }

        public DateTime CreatedDate { get; set; }

        // tinyint - NULL allow
        public DoctorNurseApplicationsRollType RollType { get; set; }

        // NULL allow
        public string? OTP { get; set; }

        public DateTime? OTPExpiry { get; set; }

        public int? OTPAttempts { get; set; }

        public DateTime? LockoutEndTime { get; set; }

        public DateTime? LastFailedAttempt { get; set; }

        // varchar(MAX)
        public string? ResumePath { get; set; }

        public DateTime? LastOtpSentTime { get; set; }
    }
}