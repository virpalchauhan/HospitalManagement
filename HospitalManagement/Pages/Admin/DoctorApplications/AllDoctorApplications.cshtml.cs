using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.DoctorApplications
{
    public class AllDoctorApplicationsModel : PageModel
    {

        [BindProperty]

        public List<DoctorNurseApplicationInnerJoin> AllDoctorApplications { get; set; }

        private readonly IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices;

        public AllDoctorApplicationsModel(IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices)
        {
            this.ObjDoctorNurseApplicationServices = ObjDoctorNurseApplicationServices;

        }
        public void OnGet()
        {   
            AllDoctorApplications= ObjDoctorNurseApplicationServices.AlDoctorApplications();

        }
    }
}
