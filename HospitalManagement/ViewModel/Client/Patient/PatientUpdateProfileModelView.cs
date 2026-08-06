using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Client.Patient
{
    public class PatientUpdateProfileModelView
    {

        public int PatientId { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        [RegularExpression(@"^[A-Za-z\s]{2,50}$")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        [RegularExpression(@"^[A-Za-z\s]{2,50}$")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public GenderType? Gender { get; set; }

        [Required(ErrorMessage = "DateOfBirth is Required")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "MobileNo is Required")]
        [RegularExpression(@"^[6-9]\d{9}$")]
        public string? MobileNo { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [RegularExpression(@"^[A-Za-z\s]{2,50}$")]
        public string? City { get; set; }

        [Required(ErrorMessage = "StateName is Required")]
        [RegularExpression(@"^[A-Za-z\s]{2,50}$")]
        public string? StateName { get; set; }

        [Required(ErrorMessage = "Pincode is Required")]
        [RegularExpression(@"^\d{6}$")]
        public string? Pincode { get; set; }

        [Required(ErrorMessage = "BloodGroup is Required")]
        public BloodGroupType? BloodGroup { get; set; }

        public IFormFile? ProfilePhoto { get; set; }

        public string? ProfilePhotoPath { get; set; }

    }
}
