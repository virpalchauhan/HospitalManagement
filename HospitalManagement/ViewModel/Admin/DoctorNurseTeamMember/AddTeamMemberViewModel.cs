using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Admin.DoctorNurseTeamMember
{
    public class AddTeamMemberViewModel
    {
        [Required(ErrorMessage = "Please select a Nurse.")]
        public int NurseId { get; set; }
    }
}
