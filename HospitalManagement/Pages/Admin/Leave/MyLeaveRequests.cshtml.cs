using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.Leave
{
    public class MyLeaveRequestsModel : PageModel
    {

        public string CookieEmployeeId { get; set; }

        [BindProperty]

        public List<LeaveRequests> LeaveRequestsList { get; set; }

        private readonly ILeaveRequestServices _LeaveRequestServices;

        public MyLeaveRequestsModel(ILeaveRequestServices _LeaveRequestServices)
        {
            this._LeaveRequestServices = _LeaveRequestServices;
        }


        public void OnGet()
        {
            CookieEmployeeId = User.FindFirst("DoctorNurceId")?.Value;

            LeaveRequestsList = _LeaveRequestServices.GetLeaveRequestsByEmployeeId(Convert.ToInt32(CookieEmployeeId));
        }

        public JsonResult OnGetStatus(byte? status)
        {
            var employeeIdValue = User.FindFirst("DoctorNurceId")?.Value;

            if (string.IsNullOrEmpty(employeeIdValue))
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Unauthorized"
                });
            }

            int employeeId = Convert.ToInt32(employeeIdValue);


            LeaveRequestsList =
                _LeaveRequestServices.GetMyLeaveRequestsByStatus(
                    employeeId,
                    status);


            return new JsonResult(LeaveRequestsList);
        }
    }
}
