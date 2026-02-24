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

        public List<DoctorApplicationInnerJoin> AllPendingDoctorApplications { get; set; }

        private readonly IDoctorApplicationservices ObjDoctorApplicationservices;

        public AllPendingDoctorApplicationsModel(IDoctorApplicationservices ObjDoctorApplicationservices)
        {
            this.ObjDoctorApplicationservices = ObjDoctorApplicationservices;
        }


        public void OnGet()
        {

            AllPendingDoctorApplications=ObjDoctorApplicationservices.AllPendingDoctorApplications();

        }
    }
}
