using HospitalManagement.EmailServices;
using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;


namespace HospitalManagement.Pages.Admin.Account
{
    public class ForgotPasswordModel : PageModel
    {


        private readonly IAccountServices ObjAccountServices;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly IDoctorAndNurseServices ObjDoctorAndNurseServices;
        

        [BindProperty]

        public ForgotPasswordViewModel ForgotPasswordViewModel { get; set; }

        [BindProperty]


        public OtpViewModel OtpViewModel { get; set; }

        [BindProperty]

        public ForgotNewPassword ForgotNewPassword { get; set; }

        [BindProperty]
        public string CurrentStep { get; set; } = "Email";


        public ForgotPasswordModel(IAccountServices ObjAccountServices, IWebHostEnvironment WebHostEnvironment, IDoctorAndNurseServices ObjDoctorAndNurseServices)
        {
            this.ObjAccountServices = ObjAccountServices;
            this.WebHostEnvironment = WebHostEnvironment;
            this.ObjDoctorAndNurseServices = ObjDoctorAndNurseServices;
        }


        public void OnGet()
        {
            if (TempData["Step"] != null)
            {
                CurrentStep = TempData["Step"].ToString();
            }
        }

        public IActionResult OnPostSendOtp()
        {
            TempData["Step"] = "Email";
            
            ModelState.Clear();

            if (TryValidateModel(ForgotPasswordViewModel, nameof(ForgotPasswordViewModel)))
            {
                


                var Result = ObjAccountServices.ForgotPassword(ForgotPasswordViewModel.Email);



               


                if (Result == null)
                {
                    TempData["Msg"] = "No account found with this email address. Please check and try again.";
                    ForgotPasswordViewModel.Email = "";
                    return RedirectToPage();

                }
                else
                {
                    if (Result.LastFailedAttempt != null && Result.LastFailedAttempt < DateTime.Now.AddMinutes(-15))
                    {

                        Result.OTPAttempts = 0;

                        ObjAccountServices.UpdateOnlyOtpAttemts(Result.DoctorNurceId, Convert.ToInt32(Result.OTPAttempts));
                    }

                    if (Result.LockoutEndTime != null && Result.LockoutEndTime > DateTime.Now)
                    {
                        TempData["Msg"] = "Your account is temporarily locked. Please try again later.";
                        ForgotPasswordViewModel.Email = "";
                        return RedirectToPage();

                    }


                   

                    HttpContext.Session.SetString("ResetEmail", Result.Email);


                    string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "EmailTemplet", "ForgotPasswordTemplet.html");

                    string EmailBody = System.IO.File.ReadAllText(filePath);

                    Random random = new Random();
                    int otp = random.Next(100000, 999999);

                    EmailBody = EmailBody.Replace("{{OTP}}", otp.ToString());
                    EmailBody = EmailBody.Replace("{{UserName}}", Result.FirstName + " " + Result.LastName);
                    EmailBody = EmailBody.Replace("{{UserEmail}}", Result.Email);


                    ObjAccountServices.SetOtpForUser(Result.DoctorNurceId, otp.ToString(), DateTime.Now.AddMinutes(10));

                    bool ResultOutput = ForgotPasswordTempletCode.ForgotPasswordTempletCodeSend(Result.Email, EmailBody);

                    if (ResultOutput)
                    {
                        TempData["Step"] = "Otp";
                        return RedirectToPage();

                    }

                }


            }
            return Page();

        }

        public IActionResult OnPostVerifyOtp()
        {

            var ResetEmail = HttpContext.Session.GetString("ResetEmail");
           

            if (!TryValidateModel(OtpViewModel, nameof(OtpViewModel)))
            {
                
                //TempData["Step"] = "Otp";

                var UserData = ObjDoctorAndNurseServices.GetByEmail(ResetEmail);
                var otp = OtpViewModel.Otp;
                if (UserData.OTP!=otp)
                {
                    UserData.OTPAttempts++;
                    UserData.LastFailedAttempt = DateTime.Now;

                    ObjAccountServices.UpdateOTPAttempts(UserData.DoctorNurceId, Convert.ToInt32( UserData.OTPAttempts), UserData.LastFailedAttempt);


                    if (UserData.OTPAttempts>=5)
                    {
                        UserData.LockoutEndTime = DateTime.Now.AddMinutes(15);
                        UserData.OTPAttempts = 0;

                        ObjAccountServices.UpdateLockoutEndTime(UserData.DoctorNurceId, Convert.ToInt32(UserData.OTPAttempts), UserData.LockoutEndTime);

                        TempData["Msg"] = "Too many failed attempts. Your account has been locked for 15 minutes.";
                        
                        TempData["Step"] = "Email";
                    }
                    else
                    {
                        TempData["Msg"] = "Invalid OTP. Please try again.";
                        
                        TempData["Step"] = "Otp";
                        OtpViewModel.Otp = "";
                        return RedirectToPage();


                    }
                }
                else if (UserData.OTP==otp)
                {
                    TempData["Step"] = "Reset";
                   


                }




              
            }

           
            return Page();
        }

        public IActionResult ResetPassword()
        {
            if (!TryValidateModel(ForgotNewPassword, nameof(ForgotNewPassword)))
            {
                var ResetEmail = HttpContext.Session.GetString("ResetEmail");


                var UserData = ObjDoctorAndNurseServices.GetByEmail(ResetEmail);

                DoctorsAndNurse UpdateForgotPassword = new DoctorsAndNurse()
                {
                    OTP = null,
                    OTPAttempts = 0,
                    LockoutEndTime = null,
                    DoctorNurceId = UserData.DoctorNurceId,
                    PasswordHash = ForgotNewPassword.ConfirmPassword
                };

                int data = ObjAccountServices.SetPasswordForUser(UpdateForgotPassword);

                if (data > 0)
                {
                    TempData["Msg"] = "Your password has been changed successfully. Please log in using your new password.";

                    return RedirectToPage("/Admin/Account/Login");
                }
            }
            return Page();
        }
    }
}
