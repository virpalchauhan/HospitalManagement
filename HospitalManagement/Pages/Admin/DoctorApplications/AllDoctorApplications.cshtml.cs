using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.DoctorApplications
{
    public class AllDoctorApplicationsModel : PageModel
    {

        [BindProperty]

        public List<DoctorApplicationInnerJoin> AllDoctorApplications { get; set; }

        private readonly IDoctorApplicationservices ObjDoctorApplicationservices;

        public AllDoctorApplicationsModel(IDoctorApplicationservices ObjDoctorApplicationservices)
        {
            this.ObjDoctorApplicationservices = ObjDoctorApplicationservices;

        }
        public void OnGet()
        {   
            AllDoctorApplications= ObjDoctorApplicationservices.AlDoctorApplications();

        }
    }
}
