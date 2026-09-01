namespace HospitalManagement.Entity.Model.Innerjoin
{
    public class MyTeamModelInnerjoin
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string DepartmentName { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public int TeamMemberId { get; set; }

        public int NurseId { get; set; }

        public string NurseName { get; set; }

        public DateTime JoinedDate { get; set; }

        public bool? MemberIsActive { get; set; }

        public bool? TeamIsActive { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
