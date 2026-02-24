using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Entity.Model
{

    [Table("DepartmentTbl")]

    public class DepartmentTbl
    {
        [Key]


        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }

        public DateTime CreationDate { get; set; }

    }
}
