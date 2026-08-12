using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Services.Client;
using HospitalManagement.ViewModel.Client.Patient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using HospitalManagement.Entity.Model;


namespace HospitalManagement.Pages.Client.Account
{
    public class ProfileModel : PageModel
    {
        public string CookiePatientId { get; set; }

        public IFormFile? ProfilePhoto { get; set; }


        private readonly IPatientServices _PatientServices;
        private readonly IWebHostEnvironment _webHostEnvironment;




        [BindProperty]
     public    PatientUpdateProfileModelView PatientUpdateProfileModelView { get; set; } = new PatientUpdateProfileModelView();


        public ProfileModel(IPatientServices _PatientServices, IWebHostEnvironment _webHostEnvironment)
        {
            this._PatientServices = _PatientServices;
            this._webHostEnvironment = _webHostEnvironment;
        }


        public void OnGet()
        {
           

            CookiePatientId = User.FindFirst("PatientId")?.Value;

            var patientData =_PatientServices.GetByid(Convert.ToInt32(CookiePatientId));

            var BlodGroupList = Enum.GetValues(typeof(BloodGroupType))
                .Cast<BloodGroupType>()
                .Select(bg => new SelectListItem
                {
                    Value = bg.ToString(),
                    Text = bg.ToString()
                })
                .ToList();

            if (patientData != null)
            {
                PatientUpdateProfileModelView.PatientId = patientData.PatientId;
                PatientUpdateProfileModelView.FirstName = patientData.FirstName;
                PatientUpdateProfileModelView.LastName = patientData.LastName;
                PatientUpdateProfileModelView.Gender = patientData.Gender;
                PatientUpdateProfileModelView.DateOfBirth = patientData.DateOfBirth;
                PatientUpdateProfileModelView.MobileNo = patientData.MobileNo;
                PatientUpdateProfileModelView.Email = patientData.Email;
                
                PatientUpdateProfileModelView.ProfilePhotoPath = patientData.ProfilePhotoPath;
                PatientUpdateProfileModelView.Address = patientData.Address;
                PatientUpdateProfileModelView.City = patientData.City;
                PatientUpdateProfileModelView.StateName = patientData.StateName;
                PatientUpdateProfileModelView.Pincode = patientData.Pincode;
                PatientUpdateProfileModelView.BloodGroup = patientData.BloodGroup;
                
            }




        }

        public void OnPostUpdateProfile()
        {

            if (ModelState.IsValid)
            {
                var OldPath = "";

                 OldPath = PatientUpdateProfileModelView.ProfilePhotoPath;


                CookiePatientId = User.FindFirst("PatientId")?.Value;



                if (ProfilePhoto != null)
                {
                    using FileStream fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "Client/ProfilePhoto/", ProfilePhoto.FileName), FileMode.Create);
                    ProfilePhoto.CopyTo(fs);
                    fs.Close();
                    OldPath = "Client/ProfilePhoto/" + ProfilePhoto.FileName;
                }



                Patient UpdateProfile = new Patient {

                    PatientId = PatientUpdateProfileModelView.PatientId,
                    FirstName = PatientUpdateProfileModelView.FirstName,
                    LastName = PatientUpdateProfileModelView.LastName,
                    Gender = PatientUpdateProfileModelView.Gender,
                    DateOfBirth = PatientUpdateProfileModelView.DateOfBirth,
                    MobileNo = PatientUpdateProfileModelView.MobileNo,                   
                    Address = PatientUpdateProfileModelView.Address,
                    City = PatientUpdateProfileModelView.City,
                    StateName = PatientUpdateProfileModelView.StateName,
                    Pincode = PatientUpdateProfileModelView.Pincode,
                    BloodGroup = PatientUpdateProfileModelView.BloodGroup,
                    ProfilePhotoPath = OldPath

                };

                var Result = _PatientServices.UpdateProfile(UpdateProfile);


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

            }



        }
    }
}
