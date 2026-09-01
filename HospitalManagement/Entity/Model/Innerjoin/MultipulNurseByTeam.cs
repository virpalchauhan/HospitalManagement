using HospitalManagement.Entity.Model.Enums;

namespace HospitalManagement.Entity.Model.Innerjoin
{
    public class MultipulNurseByTeam
    {

        public string NurseName { get; set; }

        public DateTime CreateDate { get; set; }

        public int NurseId { get; set; }

        public bool IsActive { get; set; }

        public int TeamMemberId { get; set; }

        public DoctorNurseApplicationsRollType RollType { get; set; }

    }
}
