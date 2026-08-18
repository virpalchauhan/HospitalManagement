using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Entity.Model
{

    [Table("DoctorNurseTeam")]

    public class DoctorNurseTeam
    {

        [Key]

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public int DoctorId { get; set; }

        public int? DepartmentId { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
