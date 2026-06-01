     using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagement.Pages.Admin.Account
{
    public class ProfileModel : PageModel
    {

        public string CookieDoctorNurceId { get; set; }

        private readonly IDoctorAndNurseServices ObjDoctorAndNurseServices;
        private readonly IDepartmentTblServices ObjDepartmentTbl;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]

        public List<SelectListItem> DepartmentList { get; set; }

        [BindProperty]
        public IFormFile? ProfilePhoto { get; set; }


        [BindProperty]
        public DoctorNurseEditProfileViewModel DoctorNurseEditProfileViewModel { get; set; } = new DoctorNurseEditProfileViewModel();

        public ProfileModel(IDoctorAndNurseServices ObjDoctorAndNurseServices, IDepartmentTblServices ObjDepartmentTbl, IWebHostEnvironment webHostEnvironment)
        {
            this.ObjDoctorAndNurseServices = ObjDoctorAndNurseServices;
            this.ObjDepartmentTbl = ObjDepartmentTbl;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
            var deptData = ObjDepartmentTbl.AllDepartment();
            DepartmentList = deptData.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();

            CookieDoctorNurceId = User.FindFirst("DoctorNurceId")?.Value;

            var DoctorNurceData = ObjDoctorAndNurseServices.GetByID(Convert.ToInt32(CookieDoctorNurceId));

            if (DoctorNurceData != null)
            {


                DoctorNurseEditProfileViewModel.FirstName = DoctorNurceData.FirstName;

                DoctorNurseEditProfileViewModel.LastName = DoctorNurceData.LastName;

                DoctorNurseEditProfileViewModel.Gender = DoctorNurceData.Gender;

                DoctorNurseEditProfileViewModel.DateOfBirth = DoctorNurceData.DateOfBirth;

                DoctorNurseEditProfileViewModel.MobileNo = DoctorNurceData.MobileNo;

                DoctorNurseEditProfileViewModel.Email = DoctorNurceData.Email;

                DoctorNurseEditProfileViewModel.DepartmentName = DoctorNurceData.DepartmentName;

                DoctorNurseEditProfileViewModel.ProfilePhotoPath = DoctorNurceData.ProfilePhotoPath;

                DoctorNurseEditProfileViewModel.SalaryAmount = DoctorNurceData.SalaryAmount;

                DoctorNurseEditProfileViewModel.JoiningDate = DoctorNurceData.JoiningDate;



                DoctorNurseEditProfileViewModel.AccountStatus = DoctorNurceData.AccountStatus;

                DoctorNurseEditProfileViewModel.OfferLetterSent = DoctorNurceData.OfferLetterSent;

                DoctorNurseEditProfileViewModel.CreatedDate = DoctorNurceData.CreatedDate;

                DoctorNurseEditProfileViewModel.RollType = DoctorNurceData.RollType;


            }

        }

        public IActionResult OnPost()
        {


            if (ModelState.IsValid)
            {



                var ProfilePath = "";

                CookieDoctorNurceId = User.FindFirst("DoctorNurceId")?.Value;

                //var DoctorNurceData = ObjDoctorAndNurseServices.GetByID(Convert.ToInt32(DoctorNurceId));

                if (ProfilePhoto != null)
                {
                    using FileStream fs = new FileStream(Path.Combine(webHostEnvironment.WebRootPath, "Client/ProfilePhoto/", ProfilePhoto.FileName), FileMode.Create);
                    ProfilePhoto.CopyTo(fs);
                    fs.Close();
                    ProfilePath = "Client/ProfilePhoto/" + ProfilePhoto.FileName;
                }
                else
                {
                    ProfilePath = DoctorNurseEditProfileViewModel.ProfilePhotoPath;
                }


                DoctorsAndNurse UpdateProfile = new DoctorsAndNurse
                {
                    FirstName = DoctorNurseEditProfileViewModel.FirstName,
                    LastName = DoctorNurseEditProfileViewModel.LastName,
                    Gender = DoctorNurseEditProfileViewModel.Gender,
                    DateOfBirth = DoctorNurseEditProfileViewModel.DateOfBirth,
                    ProfilePhotoPath = ProfilePath,
                    DoctorNurceId = Convert.ToInt32(CookieDoctorNurceId),


                };

                var Result = ObjDoctorAndNurseServices.UpdateProfile(UpdateProfile);

                if (Result == 1)
                {
                    TempData["MsgSuccess"] =
                        "Profile Updated Successfully";
                }
                else if (Result == 0)
                {
                    TempData["MsgNormal"] =
                        "User Not Found";
                }
                else
                {
                    TempData["MsgDanger"] =
                        "Error While Updating Profile. Please Try Again.";
                }

                return RedirectToPage();
            }
            else
            {
                TempData["MsgNormal"] = "Please fill all the required fields.";
                return RedirectToPage();

            }
        }
    }
}
