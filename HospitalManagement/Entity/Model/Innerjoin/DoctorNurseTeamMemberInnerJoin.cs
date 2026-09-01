namespace HospitalManagement.Entity.Model.Innerjoin
{
    public class DoctorNurseTeamMemberInnerJoin
    {

        public int TeamMemberId { get; set; }

        public int TeamId { get; set; }

        public int NurseId { get; set; }

        public bool IsActive { get; set; }

        public DateTime JoinedDate { get; set; }

        public DateTime? RemovedDate { get; set; }

        public string Name { get; set; }
    }
}
