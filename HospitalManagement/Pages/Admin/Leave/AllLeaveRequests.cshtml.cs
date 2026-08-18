using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.Leave
{
    public class AllLeaveRequestsModel : PageModel
    {

        [BindProperty]
        public List<LeaveRequests> LeaveRequestsList { get; set; }

        private readonly ILeaveRequestServices _LeaveRequestServices;

        public AllLeaveRequestsModel(ILeaveRequestServices _LeaveRequestServices)
        {
            this._LeaveRequestServices = _LeaveRequestServices;
        }

        public void OnGet()
        {
            LeaveRequestsList = _LeaveRequestServices.GetAllLeaveRequests();

        }

        public JsonResult OnGetStatus(byte? status)
        {
            var LeaveRequestsList = _LeaveRequestServices.GetLeaveRequestsByStatus(status);

            return new JsonResult(LeaveRequestsList);
        }
    }
}
