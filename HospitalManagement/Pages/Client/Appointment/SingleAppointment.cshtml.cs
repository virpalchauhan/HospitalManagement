using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Client.Appointment
{
    public class SingleAppointmentModel : PageModel
    {
        private readonly IAppointmentTableServices _AppointmentTableServices;

        [BindProperty]

        public AppointmentPatientDepartmentInnerJoin AppointmentPatientDepartmentInnerJoin { get; set; } = new();


        public SingleAppointmentModel(IAppointmentTableServices _AppointmentTableServices)
        {
            this._AppointmentTableServices = _AppointmentTableServices;
        }

        public void OnGet(int AppointmentId)
        {
            var data = _AppointmentTableServices.AppointmentPatientDepartmentInnerJoinforDoctor(AppointmentId);

            AppointmentPatientDepartmentInnerJoin.DoctorName = data.DoctorName;
            AppointmentPatientDepartmentInnerJoin.DepartmentName = data.DepartmentName;
            AppointmentPatientDepartmentInnerJoin.SuggestedDate = data.SuggestedDate;
            AppointmentPatientDepartmentInnerJoin.SuggestedTime = data.SuggestedTime;
            AppointmentPatientDepartmentInnerJoin.AppointmentDate = data.AppointmentDate;
            AppointmentPatientDepartmentInnerJoin.AppointmentTime = data.AppointmentTime;
            AppointmentPatientDepartmentInnerJoin.AppointmentId = data.AppointmentId;
            AppointmentPatientDepartmentInnerJoin.Status = data.Status;



        }

        public IActionResult OnPostAccept()
        {
            AppointmentTable UpdateAppointmentStatus = new AppointmentTable
            {
                //Status = AppointmentStatusType.Accept,
                AppointmentId= AppointmentPatientDepartmentInnerJoin.AppointmentId,
                Status= AppointmentStatusType.Confirmed
            };

            var Result = _AppointmentTableServices.UpdateAppointmentStatus(UpdateAppointmentStatus);

            if (Result>=1)
            {
                TempData["Msg"] = "Appointment Status Update Successfully";
                return RedirectToPage("/Client/Appointment/AppointmentList");
            }
            else
            {
                TempData["Msg"] = "Appointment Status Update Failed";
                return RedirectToPage("/Client/Appointment/AppointmentList");
            }

            


        }
        public IActionResult OnPostRejected()
        {
            AppointmentTable UpdateAppointmentStatus = new AppointmentTable
            {
                Status = AppointmentStatusType.Reject,
                AppointmentId = AppointmentPatientDepartmentInnerJoin.AppointmentId
            };

            var Result = _AppointmentTableServices.UpdateAppointmentStatus(UpdateAppointmentStatus);

            if (Result >= 1)
            {
                TempData["Msg"] = "Appointment Status Update Successfully";
                return RedirectToPage("/Client/Appointment/AppointmentList");
            }
            else
            {
                TempData["Msg"] = "Appointment Status Update Failed";
                return RedirectToPage("/Client/Appointment/AppointmentList");
            }

        }
    }
}
