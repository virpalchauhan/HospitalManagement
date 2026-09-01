using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Entity.Model
{
    [Table("DoctorNurseTeamMember")]
    public class DoctorNurseTeamMember
    {
        [Key]
        public int TeamMemberId { get; set; }

        public int TeamId { get; set; }

        public int NurseId { get; set; }

        public bool IsActive { get; set; }

        public DateTime JoinedDate { get; set; }

        public DateTime? RemovedDate { get; set; }
    }
    }
