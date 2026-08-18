using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Admin.Appointment.Leave
{
    public class LeaveRequestSingleViewModel
    {
        public int LeaveRequestId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DoctorNurseApplicationsRollType EmployeeType { get; set; }

        public string DepartmentName { get; set; }

        public LeaveType LeaveType { get; set; }

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public string? Reason { get; set; }

        public LeaveStatusType Status { get; set; }

        [Required(ErrorMessage = "Admin remark is required.")]
        [StringLength(500, ErrorMessage = "Admin remark cannot exceed 500 characters.")]
        public string? AdminRemark { get; set; }

        public int? ReviewedBy { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? EmployeeEmail { get; set; }


    }
}
