using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{

   

    public class DoctorApplicationView
    {

        public int DoctorApplicationsId { get; set; }


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

        [Required(ErrorMessage = "Profile Photo is Required.")]
        public IFormFile? ProfilePhoto { get; set; }

        public string? ProfilePhotoPath { get; set; }

        [Required(ErrorMessage = "Resume is Required.")]
        public IFormFile? Resume { get; set; }

        public string? ResumePath { get; set; }


        

        public DateTime RequestDate { get; set; }
      
      
        public ApplicationStatusType ApplicationStatus { get; set; }

        public bool OfferLetterSent { get; set; }
    }
}
