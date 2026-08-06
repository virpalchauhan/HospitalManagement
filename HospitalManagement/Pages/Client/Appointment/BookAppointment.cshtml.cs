using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using HospitalManagement.ViewModel.Client.Appointment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagement.Pages.Client.Appointment
{
    public class BookAppointmentModel : PageModel
    {
        // Interface 
        private readonly IDepartmentTblServices ObjDepartmentTbl;
        private readonly IDoctorAndNurseServices _DoctorAndNurseServices;
        private readonly IAppointmentTableServices _AppointmentTableServices;


        public string CookiePatientId { get; set; }


        [BindProperty]

        public PatientAppointment PatientAppointment { get; set; }


        [BindProperty]

        public List<SelectListItem> DepartmentList { get; set; }

        [BindProperty]

        public DoctorNurseApplicationsView DoctorNurseApplicationsView { get; set; }

        public BookAppointmentModel(IDepartmentTblServices ObjDepartmentTbl, IDoctorAndNurseServices _DoctorAndNurseServices, IAppointmentTableServices _AppointmentTableServices)
        {
            this.ObjDepartmentTbl = ObjDepartmentTbl;
            this._DoctorAndNurseServices = _DoctorAndNurseServices;
            this._AppointmentTableServices = _AppointmentTableServices;
        }

        public void OnGet()
        {

            var deptData = ObjDepartmentTbl.AllDepartment();

            DepartmentList = deptData.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();
        }

        public JsonResult OnGetDoctorByDepartment(int departmentId)
        {
            var doctorList = _DoctorAndNurseServices.GetDoctorsByDepartment(departmentId);


            var result = doctorList.Select(x => new
            {
                doctorId = x.DoctorNurceId,
                doctorName = x.FirstName + " " + x.LastName
               
            });

            return new JsonResult(result);
        }


        public void OnPost()
        {
            ModelState.Clear();

            if (TryValidateModel(PatientAppointment, nameof(PatientAppointment)))
            {

                CookiePatientId = User.FindFirst("PatientId")?.Value;

                AppointmentTable InsertAppointmentData = new AppointmentTable
                {

                    DepartmentId = PatientAppointment.DepartmentId,
                    DoctorId = PatientAppointment.DoctorId,
                    SuggestedDate = PatientAppointment.SuggestedDate,
                    SuggestedTime = PatientAppointment.SuggestedTime,
                    AppointmentBookDate = DateTime.Now,
                    Reason = PatientAppointment.Reason,
                    PatientId = Convert.ToInt32(CookiePatientId),
                    Status = AppointmentStatusType.Pending
                };

                var Result = _AppointmentTableServices.AddAppointment(InsertAppointmentData);



            }



        }

    }
}
