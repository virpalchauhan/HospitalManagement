using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.DoctorApplications
{
    public class AllAcceptDoctorApplicationsModel : PageModel
    {

        [BindProperty]

        public List<DoctorNurseApplicationInnerJoin> AllAcceptDoctorApplications { get; set; }

        private readonly IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices;

        public AllAcceptDoctorApplicationsModel(IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices)
        {
            this.ObjDoctorNurseApplicationServices = ObjDoctorNurseApplicationServices;
        }

        public void OnGet()
        {
            AllAcceptDoctorApplications= ObjDoctorNurseApplicationServices.AllAcceptDoctorApplications();

        }
    }
}
