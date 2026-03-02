using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.DoctorApplications
{
    public class AllAcceptDoctorApplicationsModel : PageModel
    {

        [BindProperty]

        public List<DoctorApplicationInnerJoin> AllAcceptDoctorApplications { get; set; }

        private readonly IDoctorApplicationservices ObjDoctorApplicationservices;

        public AllAcceptDoctorApplicationsModel(IDoctorApplicationservices ObjDoctorApplicationservices)
        {
            this.ObjDoctorApplicationservices = ObjDoctorApplicationservices;
        }

        public void OnGet()
        {
            AllAcceptDoctorApplications= ObjDoctorApplicationservices.AllAcceptDoctorApplications();

        }
    }
}
