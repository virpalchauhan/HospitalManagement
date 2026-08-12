using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Client.Appointment
{
    public class AppointmentListModel : PageModel
    {

        private readonly IAppointmentTableServices _AppointmentTableServices;

        [BindProperty]

        public List<AppointmentPatientDepartmentInnerJoin> AppointmentPatientDepartmentInnerJoinList { get; set; }


        [BindProperty]

        public string PatientCookie { get; set; }


        public AppointmentListModel(IAppointmentTableServices _AppointmentTableServices)
        {
           this._AppointmentTableServices = _AppointmentTableServices;
        }






        public void OnGet()
        {
            PatientCookie = User.FindFirst("PatientId")?.Value;
            


            AppointmentPatientDepartmentInnerJoinList = _AppointmentTableServices.AppointmentPatientDepartmentInnerJoinForPatient(Convert.ToInt32(PatientCookie));
        }
    }
}
