using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.Appointment
{



    public class AllAppointmentModel : PageModel
    {


        //Interface

        private readonly IAppointmentTableServices _AppointmentTableServices;

        public string CookieDoctorNurceId { get; set; }


        [BindProperty]

        public List<AppointmentPatientDepartmentInnerJoin> GetAllAppointmentsforDoctorList { get; set; }


        public AllAppointmentModel(IAppointmentTableServices _AppointmentTableServices)
        {
            this._AppointmentTableServices= _AppointmentTableServices; 
        }


        public void OnGet()
        {

            CookieDoctorNurceId = User.FindFirst("DoctorNurceId")?.Value;
            GetAllAppointmentsforDoctorList = _AppointmentTableServices.GetAllAppointmentsforDoctor(Convert.ToInt32(CookieDoctorNurceId));
        }

        public JsonResult OnGetStatus(AppointmentStatusType? status)
        {
            CookieDoctorNurceId = User.FindFirst("DoctorNurceId")?.Value;

            if (!status.HasValue)
            {
                var Data = _AppointmentTableServices.GetAllAppointmentsforDoctor(Convert.ToInt32(CookieDoctorNurceId));
                return new JsonResult(Data);
            }

            var data = _AppointmentTableServices.GetAppointmentByStatus(Convert.ToInt32(CookieDoctorNurceId),status);
            return new JsonResult(data);

        }

    }
}
