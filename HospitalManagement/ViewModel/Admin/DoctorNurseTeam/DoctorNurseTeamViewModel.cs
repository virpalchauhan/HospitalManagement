using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Admin.DoctorNurseTeam
{
    public class DoctorNurseTeamViewModel
    {

        public int TeamId { get; set; }

        [Required(ErrorMessage = "Team Name is required.")]
        [StringLength(100, ErrorMessage = "Team Name cannot exceed 100 characters.")]
        public string TeamName { get; set; }

        [Required(ErrorMessage = "Please select a Doctor.")]
        public int? DoctorId { get; set; }

        [Required(ErrorMessage = "Please select a Department.")]
        public int? DepartmentId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
