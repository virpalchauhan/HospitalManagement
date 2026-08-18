using HospitalManagement.EmailServices;
using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel.Admin.Appointment.Leave;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages.Admin.Leave
{

   
    public class ViewSingleLeaveAdminModel : PageModel
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly LeaveApprovedEmailTemplateCode _LeaveApprovedEmailTemplateCode;
        private readonly LeaveRejectedEmailTemplateCode _LeaveRejectedEmailTemplateCode;

        private readonly ILeaveRequestServices _LeaveRequestServices;

        public ViewSingleLeaveAdminModel(ILeaveRequestServices _LeaveRequestServices, IWebHostEnvironment _WebHostEnvironment, LeaveApprovedEmailTemplateCode _LeaveApprovedEmailTemplateCode, LeaveRejectedEmailTemplateCode _LeaveRejectedEmailTemplateCode)
        {
            this._LeaveRequestServices = _LeaveRequestServices;
            this._WebHostEnvironment = _WebHostEnvironment;
            this._LeaveApprovedEmailTemplateCode = _LeaveApprovedEmailTemplateCode;
            this._LeaveRejectedEmailTemplateCode = _LeaveRejectedEmailTemplateCode;
        }

        [BindProperty(SupportsGet = true)]
        public int LeaveRequestId { get; set; }

        [BindProperty]

        public LeaveRequestSingleViewModel LeaveRequestSingleData { get; set; }

        public void OnGet()
        {
            LeaveRequestSingleData = _LeaveRequestServices.GetSingleLeaveRequest(LeaveRequestId);
        }

        public IActionResult OnPostApprove()
        {

          var  CookieDoctorNurceId = User.FindFirst("DoctorNurceId")?.Value;
            LeaveRequests LeaveRequests = new LeaveRequests()
            {
                LeaveRequestId = LeaveRequestId,
                AdminRemark = LeaveRequestSingleData.AdminRemark,
                Status=Entity.Model.Enums.LeaveStatusType.Approved,
                ReviewedBy=Convert.ToInt32(CookieDoctorNurceId)
            };

            var Result = _LeaveRequestServices.UpdateLeaveRequest(LeaveRequests);

            if (Result>=1)
            {
                TempData["Msg"] = "Leave Status Update Successfully";


                LeaveRequestSingleData = _LeaveRequestServices.GetSingleLeaveRequest(LeaveRequestId);

                string FilePath = Path.Combine(_WebHostEnvironment.WebRootPath, "EmailTemplet", "LeaveApprovedEmailTemplate.html");
                
                string EmailBody = System.IO.File.ReadAllText(FilePath);

                EmailBody=EmailBody.Replace("{EmployeeName}", LeaveRequestSingleData.EmployeeName);
                EmailBody=EmailBody.Replace("{LeaveType}", LeaveRequestSingleData.LeaveType.ToString());
                EmailBody=EmailBody.Replace("{FromDate}", LeaveRequestSingleData.FromDate.ToString());
                EmailBody=EmailBody.Replace("{ToDate}", LeaveRequestSingleData.ToDate.ToString());
                EmailBody=EmailBody.Replace("{AdminRemark}", LeaveRequestSingleData.AdminRemark);




                _LeaveApprovedEmailTemplateCode.LeaveApprovedEmailTemplateCodeSend(LeaveRequestSingleData.EmployeeEmail, EmailBody);
                return RedirectToPage("/Admin/Leave/AllLeaveRequests");
            }
            else
            {
                TempData["Msg"] = "Leave Status Update Fail";
                return RedirectToPage("/Admin/Leave/AllLeaveRequests");
            }
        }

        public IActionResult OnpostReject()
        {
            LeaveRequests LeaveRequests = new LeaveRequests()
            {
                LeaveRequestId = LeaveRequestId,
                AdminRemark = LeaveRequestSingleData.AdminRemark,
                Status = Entity.Model.Enums.LeaveStatusType.Rejected
            };
            var Result = _LeaveRequestServices.UpdateLeaveRequest(LeaveRequests);

            if (Result >= 1)
            {
                TempData["Msg"] = "Leave Status Update Successfully";


                LeaveRequestSingleData = _LeaveRequestServices.GetSingleLeaveRequest(LeaveRequestId);

                string FilePath = Path.Combine(_WebHostEnvironment.WebRootPath, "EmailTemplet", "LeaveRejectedEmailTemplate.html");

                string EmailBody = System.IO.File.ReadAllText(FilePath);

                EmailBody = EmailBody.Replace("{EmployeeName}", LeaveRequestSingleData.EmployeeName);
                EmailBody = EmailBody.Replace("{LeaveType}", LeaveRequestSingleData.LeaveType.ToString());
                EmailBody = EmailBody.Replace("{FromDate}", LeaveRequestSingleData.FromDate.ToString());
                EmailBody = EmailBody.Replace("{ToDate}", LeaveRequestSingleData.ToDate.ToString());
                EmailBody = EmailBody.Replace("{AdminRemark}", LeaveRequestSingleData.AdminRemark);
                _LeaveRejectedEmailTemplateCode.LeaveRejectedEmailTemplateCodeSend(LeaveRequestSingleData.EmployeeEmail, EmailBody);

                return RedirectToPage("/Admin/Leave/AllLeaveRequests");
            }
            else
            {
                TempData["Msg"] = "Leave Status Update Fail";
                return RedirectToPage("/Admin/Leave/AllLeaveRequests");
            }
        }

    }
}
