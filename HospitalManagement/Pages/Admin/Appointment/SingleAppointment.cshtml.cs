using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using HospitalManagement.ViewModel.Admin.Appointment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.Appointment
{
    public class SingleAppointmentModel : PageModel
    {

        //Interface

        private readonly IAppointmentTableServices _AppointmentTableServices;

        [BindProperty]

        public AppointmentPatientDepartmentInnerJoin AppointmentPatientDepartmentInnerJoinSingleData { get; set; } = new AppointmentPatientDepartmentInnerJoin();

        [BindProperty]

        public DoctorAppointmentApprovViewModel DoctorAppointmentApprovViewModel { get; set; } = new DoctorAppointmentApprovViewModel();


        public SingleAppointmentModel(IAppointmentTableServices _AppointmentTableServices)
        {
            this._AppointmentTableServices = _AppointmentTableServices;
        }

        public void OnGet(int AppointmentId)
        {

            var Data = _AppointmentTableServices.AppointmentPatientDepartmentInnerJoin(AppointmentId);

            AppointmentPatientDepartmentInnerJoinSingleData.SuggestedDate = Data.SuggestedDate;
            AppointmentPatientDepartmentInnerJoinSingleData.SuggestedTime = Data.SuggestedTime;
            AppointmentPatientDepartmentInnerJoinSingleData.PatientName = Data.PatientName;
            AppointmentPatientDepartmentInnerJoinSingleData.DepartmentName = Data.DepartmentName;

        }


        public IActionResult OnPost(int AppointmentId)
        {
            if (ModelState.IsValid)
            {
                AppointmentTable ApprowAppointment = new AppointmentTable()
                {

                    AppointmentDate = DoctorAppointmentApprovViewModel.AppointmentDate,
                    AppointmentTime = DoctorAppointmentApprovViewModel.AppointmentTime,
                    AppointmentId = AppointmentId

                };

                var Result = _AppointmentTableServices.DoctorResponse(ApprowAppointment);


                if (Result >= 1)
                {
                    TempData["Msg"] = "Appointment Status Update Successfully";
                   
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
