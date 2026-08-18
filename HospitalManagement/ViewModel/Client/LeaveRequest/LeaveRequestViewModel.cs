using HospitalManagement.Entity.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel.Client.LeaveRequest
{
    public class LeaveRequestViewModel
    {
        [Required(ErrorMessage = "Please select leave type.")]
        public LeaveType LeaveType { get; set; }

        [Required(ErrorMessage = "Please select from date.")]
        public DateOnly FromDate { get; set; }

        [Required(ErrorMessage = "Please select to date.")]
        public DateOnly ToDate { get; set; }

        [Required(ErrorMessage = "Please enter leave reason.")]
        [StringLength(500, MinimumLength = 5,
            ErrorMessage = "Reason must be between 5 and 500 characters.")]
        public string Reason { get; set; }
    }
}
