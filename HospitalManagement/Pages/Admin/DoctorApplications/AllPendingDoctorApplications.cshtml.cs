using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace HospitalManagement.Pages.Admin.DoctorApplications
{
    public class AllPendingDoctorApplicationsModel : PageModel
    {


        [BindProperty]

        public List<DoctorNurseApplicationInnerJoin> AllPendingDoctorApplications { get; set; }

        private readonly IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices;

        public AllPendingDoctorApplicationsModel(IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices)
        {
            this.ObjDoctorNurseApplicationServices = ObjDoctorNurseApplicationServices;
        }


        public void OnGet()
        {

            AllPendingDoctorApplications= ObjDoctorNurseApplicationServices.AllPendingDoctorApplications();

        }
    }
}
