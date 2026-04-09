using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.DoctorApplications
{
    public class AllRejectDoctorApplicationsModel : PageModel
    {

        [BindProperty]

        public List<DoctorNurseApplicationInnerJoin> AllRejectDoctorApplications { get; set; }

        private readonly IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices;

        public AllRejectDoctorApplicationsModel(IDoctorNurseApplicationServices ObjDoctorApplicationservices)
        {
            this.ObjDoctorNurseApplicationServices = ObjDoctorApplicationservices;
        }

        public void OnGet()
        {
            AllRejectDoctorApplications = ObjDoctorNurseApplicationServices.AllRejectDoctorApplications();

        }
    }
}
