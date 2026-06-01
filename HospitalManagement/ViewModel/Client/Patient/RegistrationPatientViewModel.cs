using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.ViewModel.Client.Patient
{


    [Table("RegistrationPatientViewModel")]
    public class RegistrationPatientViewModel
    {
        [Key]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public GenderType? Gender { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression(@"^[0-9]{10}$",
     ErrorMessage = "Mobile Number must be exactly 10 digits")]
        public string? MobileNo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(
     @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$",
     ErrorMessage = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number and one special character")]
        public string? PasswordHash { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("PasswordHash",
    ErrorMessage = "Password and Confirm Password do not match")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "ProfilePhoto is Required.")]
        public IFormFile? ProfilePhoto { get; set; }
        public string? ProfilePhotoPath { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50)]
        public string? City { get; set; }

        [Required(ErrorMessage = "State Name is required")]
        [StringLength(50)]
        public string? StateName { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^[0-9]{6}$",
     ErrorMessage = "Pincode must be exactly 6 digits")]
        public string? Pincode { get; set; }

        [Required(ErrorMessage = "Blood Group is required")]
        public BloodGroupType? BloodGroup { get; set; }

        public DateTime? CreateDate { get; set; }

       


    }
}
