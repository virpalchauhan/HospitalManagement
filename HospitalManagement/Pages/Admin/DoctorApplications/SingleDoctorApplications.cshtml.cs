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
        //[BindProperty]


        public DoctorApplicationView DoctorApplicationView { get; set; } = new DoctorApplicationView();

        //[BindProperty]


        public DoctorApplicationInnerJoin DoctorApplicationInnerJoin { get; set; } = new DoctorApplicationInnerJoin();

        [BindProperty]

        public DoctorApproveViewModel DoctorApproveViewModel { get; set; } = new DoctorApproveViewModel();

        private readonly IDoctorsServices ObjDoctorsServices;
        private readonly IDoctorApplicationservices ObjDoctorApplication;



        public SingleDoctorApplicationsModel(IDoctorApplicationservices ObjDoctorApplication, IDoctorsServices ObjDoctorsServices)
        {
            this.ObjDoctorApplication = ObjDoctorApplication;
            this.ObjDoctorsServices = ObjDoctorsServices;
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
                DoctorApproveViewModel.DoctorApplicationsId = id;
            }

        }

        //public IActionResult OnPostApprove(int id)
        //{


        //    return RedirectToPage(new { id = id });
        //}

        public IActionResult OnPostReject()
        {
            return Page();
        }

        public void OnPostFinalApprove()
        {

            var Number = DoctorApproveViewModel.DoctorApplicationsId;

            if (ModelState.IsValid)
            {

                var Application = ObjDoctorApplication.SingleData(DoctorApproveViewModel.DoctorApplicationsId);


                Doctors InsertDoctors = new Doctors()
                {
                    FirstName = Application.FirstName,
                    LastName = Application.LastName,
                    Gender = Application.Gender,
                    DateOfBirth = Application.DateOfBirth,
                    MobileNo = Application.MobileNo,
                    Email = Application.Email,
                    DepartmentId = Application.DepartmentId,
                    SalaryAmount = DoctorApproveViewModel.SalaryAmount,
                    JoiningDate = DoctorApproveViewModel.JoiningDate,
                    PasswordHash = "hello",
                    AccountStatus = 1,
                    OfferLetterSent = true,
                    CreatedDate = DateTime.Now,
                    ProfilePhotoPath=Application.ProfilePhotoPath
                //FirstName = DoctorApplicationInnerJoin.FirstName,
                //    LastName = DoctorApplicationInnerJoin.LastName,
                //    Gender = DoctorApplicationInnerJoin.Gender,
                //    DateOfBirth = DoctorApplicationInnerJoin.DateOfBirth,
                //    MobileNo = DoctorApplicationInnerJoin.MobileNo,
                //    Email = DoctorApplicationInnerJoin.Email,
                //    DepartmentId = DoctorApplicationInnerJoin.DepartmentId,
                //    SalaryAmount = DoctorApproveViewModel.SalaryAmount,
                //    JoiningDate = DoctorApproveViewModel.JoiningDate,
                //    PasswordHash = "hello",
                //    AccountStatus = 1,
                //    OfferLetterSent = true,
                //    CreatedDate = DateTime.Now
                };

                int result = ObjDoctorsServices.AddDoctor(InsertDoctors);

            }
        }
    }
}
