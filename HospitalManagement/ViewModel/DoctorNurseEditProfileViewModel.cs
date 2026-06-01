using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class DoctorNurseEditProfileViewModel
    {

        public int DoctorNurceId { get; set; }


        [Required(ErrorMessage = "First Name is Required.")]

        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Gender is Required.")]

        public GenderType Gender { get; set; }
        [Required(ErrorMessage = "Date of Birth is Required.")]


        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Mobile Number is Required.")]
        [Phone(ErrorMessage = "Invalid Mobile Number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile Number must be 10 digits")]
        public string? MobileNo { get; set; }

        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        public int DepartmentId { get; set; }

       
        public IFormFile? ProfilePhoto { get; set; }

        public string? ProfilePhotoPath { get; set; }

        [Required(ErrorMessage = "Resume is Required.")]
        public IFormFile? Resume { get; set; }

        public string? ResumePath { get; set; }
        public string? PasswordHash { get; set; }

        public decimal? SalaryAmount { get; set; }


        public DateTime? CreatedDate { get; set; }
        public DateTime? JoiningDate { get; set; }


        public ApplicationStatusType ApplicationStatus { get; set; }
        public DoctorNurseApplicationsRollType? RollType { get; set; }

        public DoctorNurseStatusType? AccountStatus { get; set; }

        public string? OTP { get; set; }

        public DateTime? OTPExpiry { get; set; }

        public int? OTPAttempts { get; set; }

        public DateTime? LockoutEndTime { get; set; }

        public DateTime? LastFailedAttempt { get; set; }

        public DoctorNurseOfferletterSendType? OfferLetterSent { get; set; }

        public string? DepartmentName { get; set; }




    }
}
