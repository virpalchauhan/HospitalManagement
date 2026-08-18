using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.Leave
{
    public class ViewLeaveModel : PageModel
    {
        private readonly ILeaveRequestServices _LeaveRequestServices;

        [BindProperty(SupportsGet = true)]
        public int LeaveRequestId { get; set; }

        public ViewLeaveModel(ILeaveRequestServices _LeaveRequestServices)
        {
            this._LeaveRequestServices = _LeaveRequestServices;
        }

      public LeaveRequests LeaveRequests { get; set; } = new LeaveRequests();


        public IActionResult OnGet(int LeaveRequestId)
        {



            //var EmployeeIdValue = User.FindFirst("DoctorNurseId")?.Value;
            var EmployeeIdValue =  User.FindFirst("DoctorNurceId")?.Value;
            var RoleValue = User.FindFirst("RollType")?.Value;

            if (string.IsNullOrEmpty(EmployeeIdValue))
            {
                return RedirectToPage("/Admin/Account/Login");
            }

            if (!Enum.TryParse<DoctorNurseApplicationsRollType>(RoleValue,true,out var employeeType))
            {
                return Forbid();
            }

            if (employeeType!= DoctorNurseApplicationsRollType.Doctor && employeeType!= DoctorNurseApplicationsRollType.Nurse)
            {
                return Forbid();
            }

            int EmployeeId = Convert.ToInt32(EmployeeIdValue);
            

            LeaveRequests = _LeaveRequestServices.GetLeaveRequestsByLeaveId(LeaveRequestId);

            if (LeaveRequests==null)
            {
                return Page();
            }

            if (LeaveRequests.EmployeeId != EmployeeId)
            {
                return Forbid();
            }
            return Page();
        }
    }
}
