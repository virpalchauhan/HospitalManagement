namespace HospitalManagement.ViewModel.Admin.DoctorNurseTeam
{
    public class DoctorNurseTeamListViewModel
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }

        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
