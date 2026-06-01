using HospitalManagement.EmailServices;
using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.Account
{
    public class ChangePasswordModel : PageModel
    {
        [BindProperty]

            public ChangePasswordViewModel ChangePasswordViewModel { get; set; } = new ChangePasswordViewModel();


        public string CookieDoctorNurceId { get; set; }



        private readonly IAccountServices ObjAccountServices;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly PasswordChangedTempletCode _PasswordChangedTempletCode;


        public ChangePasswordModel(IAccountServices ObjAccountServices, IWebHostEnvironment WebHostEnvironment, PasswordChangedTempletCode _PasswordChangedTempletCode)
            {
            this.ObjAccountServices = ObjAccountServices;
            this.WebHostEnvironment = WebHostEnvironment;
            this._PasswordChangedTempletCode = _PasswordChangedTempletCode; 
        }


        public void OnGet()
        {
        }

        public IActionResult OnPost() {

            if (ModelState.IsValid)
            {
                CookieDoctorNurceId = User.FindFirst("DoctorNurceId")?.Value;


                    bool IsOldPasswordCorrect = ObjAccountServices.FindAccountForChangePassword(ChangePasswordViewModel.CurrentPassword, Convert.ToInt32(CookieDoctorNurceId));


                if (!IsOldPasswordCorrect)
                {
                    TempData["MsgDanger"] =
      "The Old Password You Entered Is Incorrect";
                    return RedirectToPage();
                }

                var UserData = ObjAccountServices.GetDoctorNurceById(Convert.ToInt32(CookieDoctorNurceId));




                var Result = ObjAccountServices.ChangePassword(ChangePasswordViewModel.NewPassword, Convert.ToInt32(CookieDoctorNurceId));
                if (Result == 1)
                {
                    TempData["MsgSuccess"] =
                        "Your Password Has Been Changed Successfully";
                }
                else if (Result == 0)
                {
                    TempData["MsgNormal"] =
                        "User Not Found";
                }
                else
                {
                    TempData["MsgDanger"] =
                        "Something Went Wrong While Changing Password";
                }


                string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "EmailTemplet", "PasswordChangedTemplet.html");

                string EmailBody = System.IO.File.ReadAllText(filePath);

                Random random = new Random();

                EmailBody = EmailBody.Replace("{{UserName}}", UserData.FirstName + " " + UserData.LastName);
                EmailBody = EmailBody.Replace("{{UserEmail}}", UserData.Email);




                bool ResultOutput = _PasswordChangedTempletCode.PasswordChangedTempletCodeSend(UserData.Email, EmailBody);




                return RedirectToPage();




            }

            TempData["MsgDanger"] = "Something Went Wrong";
            return RedirectToPage();

        }

    }
}
