using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Services;
using HospitalManagement.ViewModel.Client.LeaveRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.Leave
{
    public class ApplyLeaveModel : PageModel
    {

        private readonly ILeaveRequestServices _leaveRequestServices;

        [BindProperty]
        public   LeaveRequestViewModel LeaveRequestViewModel { get; set; } = new LeaveRequestViewModel();

        public string CookieEmployeeId { get; set; }
       


        public ApplyLeaveModel(ILeaveRequestServices _leaveRequestServices)
        {
            this._leaveRequestServices = _leaveRequestServices;
        }

        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            CookieEmployeeId= User.FindFirst("DoctorNurceId")?.Value;
            var cookieRollType = User.FindFirst("RollType")?.Value;

            Enum.TryParse<DoctorNurseApplicationsRollType>(
                cookieRollType,
                true,
                out var employeeType
            );


            if (ModelState.IsValid)
            {
                LeaveRequests leaveRequest = new LeaveRequests()
                {
                    EmployeeId= Convert.ToInt32(CookieEmployeeId),
                    EmployeeType = employeeType,
                    LeaveType= LeaveRequestViewModel.LeaveType,
                    FromDate=LeaveRequestViewModel.FromDate,
                    ToDate=LeaveRequestViewModel.ToDate,
                    Reason=LeaveRequestViewModel.Reason,
                    Status= LeaveStatusType.Pending,
                    CreatedAt =System.DateTime.Now


                };


              var Result=  _leaveRequestServices.AddLeaveRequest(leaveRequest);

                if (Result>=1)
                {
                    TempData["Msg"] = "Leave Request Submitted Successfully.";
                    return RedirectToPage("/Admin/Leave/MyLeaveRequests");
                }
                else
                {
                    TempData["Msg"] = "Failed to Submit Leave Request.";
                    return Page();
                }
                
            }
            return Page();

        }
    }
}
