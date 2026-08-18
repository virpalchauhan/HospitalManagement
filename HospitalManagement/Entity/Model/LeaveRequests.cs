using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Entity.Model
{
    [Table("LeaveRequests")]

    public class LeaveRequests
    {
        [Key]

        public int LeaveRequestId { get; set; }

        public int EmployeeId { get; set; }

        public DoctorNurseApplicationsRollType EmployeeType { get; set; }

        public LeaveType LeaveType { get; set; }

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public string? Reason { get; set; }

        public LeaveStatusType Status { get; set; }

        public string? AdminRemark { get; set; }

        public int? ReviewedBy { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
