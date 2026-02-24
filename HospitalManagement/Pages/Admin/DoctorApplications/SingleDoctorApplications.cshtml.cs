using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.DoctorApplications
{
    public class SingleDoctorApplicationsModel : PageModel
    {
        private readonly IDoctorApplicationservices ObjDoctorApplication;

        [BindProperty]




        public DoctorApplicationView DoctorApplicationView { get; set; } = new DoctorApplicationView();
        [BindProperty]




        public DoctorApplicationInnerJoin DoctorApplicationInnerJoin { get; set; } = new DoctorApplicationInnerJoin();

        [BindProperty]

        public DoctorApproveViewModel DoctorApproveViewModel { get; set; } = new DoctorApproveViewModel();   



        public SingleDoctorApplicationsModel(IDoctorApplicationservices ObjDoctorApplication)
        {
            this.ObjDoctorApplication = ObjDoctorApplication;
        }

        public void OnGet(int id)
        {

            var Data = ObjDoctorApplication.SingleData(id);

            if (Data != null)
            {
                DoctorApplicationInnerJoin.FirstName = Data.FirstName;
                DoctorApplicationInnerJoin.LastName = Data.LastName;
                DoctorApplicationInnerJoin.Email = Data.Email;
                DoctorApplicationInnerJoin.MobileNo = Data.MobileNo;
                DoctorApplicationInnerJoin.Gender = Data.Gender;
                DoctorApplicationInnerJoin.DateOfBirth = Data.DateOfBirth;
                DoctorApplicationInnerJoin.DepartmentName = Data.DepartmentName;
                DoctorApplicationInnerJoin.ResumePath = Data.ResumePath;
                DoctorApplicationInnerJoin.ProfilePhotoPath = Data.ProfilePhotoPath;
            }

        }

        public IActionResult OnPostApprove()
        {
            return Page();
        }

        public IActionResult OnPostReject()
        {
            return Page();
        }

        public IActionResult OnPostFinalApprove()
        {
            return Page();
        }
    }
}
