using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
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

        public void OnPostAccept()
        {
            AppointmentTable UpdateAppointmentStatus = new AppointmentTable
            {
                //Status = AppointmentStatusType.Accept,
                AppointmentId= AppointmentPatientDepartmentInnerJoin.AppointmentId
            };

            var Result = _AppointmentTableServices.UpdateAppointmentStatus(UpdateAppointmentStatus);


        }
        public void OnPostRejected()
        {
            AppointmentTable UpdateAppointmentStatus = new AppointmentTable
            {
                //Status = AppointmentStatusType.Rejected,
                AppointmentId = AppointmentPatientDepartmentInnerJoin.AppointmentId
            };

            var Result = _AppointmentTableServices.UpdateAppointmentStatus(UpdateAppointmentStatus);

        }
    }
}
