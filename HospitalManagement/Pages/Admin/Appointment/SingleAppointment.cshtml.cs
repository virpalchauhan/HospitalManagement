using HospitalManagement.EmailServices;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using HospitalManagement.ViewModel.Admin.Appointment;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalManagement.Pages.Admin.Appointment
{
    public class SingleAppointmentModel : PageModel
    {

        //Interface

        private readonly IAppointmentTableServices _AppointmentTableServices;
        private readonly AppointmentConfirmedEmailTempletCode _AppointmentConfirmedEmailTempletCode;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]

        public AppointmentPatientDepartmentInnerJoin AppointmentPatientDepartmentInnerJoinSingleData { get; set; } = new AppointmentPatientDepartmentInnerJoin();

        [BindProperty]

        public DoctorAppointmentApprovViewModel DoctorAppointmentApprovViewModel { get; set; } = new DoctorAppointmentApprovViewModel();


        public SingleAppointmentModel(IAppointmentTableServices _AppointmentTableServices, IWebHostEnvironment _webHostEnvironment, AppointmentConfirmedEmailTempletCode _AppointmentConfirmedEmailTempletCode)
        {
            this._AppointmentTableServices = _AppointmentTableServices;
            this._webHostEnvironment = _webHostEnvironment;
            this._AppointmentConfirmedEmailTempletCode = _AppointmentConfirmedEmailTempletCode;
        }

        public void OnGet(int AppointmentId)
        {

            var Data = _AppointmentTableServices.AppointmentPatientDepartmentInnerJoinforDoctor(AppointmentId);

            AppointmentPatientDepartmentInnerJoinSingleData.SuggestedDate = Data.SuggestedDate;
            AppointmentPatientDepartmentInnerJoinSingleData.SuggestedTime = Data.SuggestedTime;    
            AppointmentPatientDepartmentInnerJoinSingleData.PatientName = Data.PatientName;
            AppointmentPatientDepartmentInnerJoinSingleData.DepartmentName = Data.DepartmentName;
            AppointmentPatientDepartmentInnerJoinSingleData.AppointmentId = Data.AppointmentId;
            AppointmentPatientDepartmentInnerJoinSingleData.Reason = Data.Reason;
            AppointmentPatientDepartmentInnerJoinSingleData.Status = Data.Status;
            AppointmentPatientDepartmentInnerJoinSingleData.PatientEmail = Data.PatientEmail;
        }


        public IActionResult OnPostConfirm()
        {
            var Data = _AppointmentTableServices.AppointmentPatientDepartmentInnerJoinforDoctor(AppointmentPatientDepartmentInnerJoinSingleData.AppointmentId);
            AppointmentTable ConfirmedAppointment = new AppointmentTable()
                {

                    AppointmentDate = Data.SuggestedDate,
                    AppointmentTime = Data.SuggestedTime,
                    AppointmentId = AppointmentPatientDepartmentInnerJoinSingleData.AppointmentId,
                    Status= AppointmentStatusType.Confirmed,
                    PatientResponse= PatientResponseType.NotRequired

                };

                var Result = _AppointmentTableServices.DoctorResponse(ConfirmedAppointment);


                if (Result >= 1)
                {
                    TempData["Msg"] = "Appointment Status Update Successfully";

                //var Data = _AppointmentTableServices.AppointmentPatientDepartmentInnerJoinforDoctor(AppointmentPatientDepartmentInnerJoinSingleData.AppointmentId);

                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplet", "AppointmentConfirmedEmailTemplet.html");
                string EmailBody = System.IO.File.ReadAllText(filePath);

                EmailBody= EmailBody.Replace("{PatientName}", Data.PatientName);
                EmailBody= EmailBody.Replace("{AppointmentDate}", Data.SuggestedDate.ToString());
                EmailBody= EmailBody.Replace("{AppointmentTime}", Data.SuggestedTime.ToString());
                EmailBody= EmailBody.Replace("{DepartmentName}", Data.DepartmentName);
                EmailBody= EmailBody.Replace("{DoctorName}", Data.DoctorName);


                _AppointmentConfirmedEmailTempletCode.AppointmentConfirmedEmailTempletCodeSend(AppointmentPatientDepartmentInnerJoinSingleData.PatientEmail, EmailBody);
                return RedirectToPage("/Admin/Appointment/AllAppointment");
                }
                else
                {
                    TempData["Msg"] = "Appointment Status Update Failed";
                    return RedirectToPage("/Admin/Appointment/AllAppointment");
                }
            
           


        }

        public IActionResult OnpostSendSuggestion()
        {

            if (ModelState.IsValid)
            {
            AppointmentTable RescheduledAppointment = new AppointmentTable()
            {

                AppointmentDate = DoctorAppointmentApprovViewModel.AppointmentDate, 
                AppointmentTime = DoctorAppointmentApprovViewModel.AppointmentTime,
                AppointmentId = AppointmentPatientDepartmentInnerJoinSingleData.AppointmentId,
                Status = AppointmentStatusType.Rescheduled,
                PatientResponse = PatientResponseType.Pending

            };

            var Result = _AppointmentTableServices.DoctorResponse(RescheduledAppointment);


            if (Result >= 1)
            {
                TempData["Msg"] = "Appointment Status Update Successfully";

                    var Data = _AppointmentTableServices.AppointmentPatientDepartmentInnerJoinforDoctor(AppointmentPatientDepartmentInnerJoinSingleData.AppointmentId);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplet", "AppointmentRescheduledEmailTemplet.html");
                    string EmailBody = System.IO.File.ReadAllText(filePath);

                    EmailBody = EmailBody.Replace("{PatientName}", Data.PatientName);
                    EmailBody = EmailBody.Replace("{SuggestedDate}", DoctorAppointmentApprovViewModel.AppointmentDate.ToString());
                    EmailBody = EmailBody.Replace("{SuggestedTime}", DoctorAppointmentApprovViewModel.AppointmentTime.ToString());
                    EmailBody = EmailBody.Replace("{DepartmentName}", Data.DepartmentName);
                    EmailBody = EmailBody.Replace("{DoctorName}", Data.DoctorName);

                    _AppointmentConfirmedEmailTempletCode.AppointmentConfirmedEmailTempletCodeSend(AppointmentPatientDepartmentInnerJoinSingleData.PatientEmail, EmailBody);

                    return RedirectToPage("/Admin/Appointment/AllAppointment");


                }
                else
            {
                TempData["Msg"] = "Appointment Status Update Failed";
                return RedirectToPage("/Admin/Appointment/AllAppointment");
            }
            }
            return RedirectToPage();


        }
    }
}
