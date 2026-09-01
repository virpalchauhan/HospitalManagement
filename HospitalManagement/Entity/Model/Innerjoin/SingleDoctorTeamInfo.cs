using HospitalManagement.Entity.Model.Enums;
using Microsoft.VisualBasic;

namespace HospitalManagement.Entity.Model.Innerjoin
{
    public class SingleDoctorTeamInfo
    {

        public int TeamId { get; set; }

        public string TeamName { get; set; }
        public string DepartmentName { get; set; }
        public string DoctorName { get; set; }

        public DateTime CreateDate { get; set; }

        public bool? IsActive { get; set; }

        public int NurseId { get; set; }

        public int DoctorId { get; set; }

        public DoctorNurseApplicationsRollType RollType { get; set; }



    }
}
