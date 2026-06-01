using HospitalManagement.EmailServices;
using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net.NetworkInformation;


namespace HospitalManagement.Pages.Admin.Account
{
    public class ForgotPasswordModel : PageModel
    {


        private readonly IAccountServices ObjAccountServices;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly IDoctorAndNurseServices ObjDoctorAndNurseServices;
        private readonly ForgotPasswordTempletCode _ForgotPasswordTempletCode;
        private readonly PasswordChangedTempletCode _PasswordChangedTempletCode;


        [BindProperty]

        public ForgotPasswordViewModel ForgotPasswordViewModel { get; set; }

        [BindProperty]


        public OtpViewModel OtpViewModel { get; set; }

        [BindProperty]

        public ForgotNewPassword ForgotNewPassword { get; set; }

        [BindProperty]
        public string CurrentStep { get; set; } = "Email";


        public ForgotPasswordModel(IAccountServices ObjAccountServices, IWebHostEnvironment WebHostEnvironment, IDoctorAndNurseServices ObjDoctorAndNurseServices, ForgotPasswordTempletCode _ForgotPasswordTempletCode, PasswordChangedTempletCode _PasswordChangedTempletCode)
        {
            this.ObjAccountServices = ObjAccountServices;
            this.WebHostEnvironment = WebHostEnvironment;
            this.ObjDoctorAndNurseServices = ObjDoctorAndNurseServices;
            this._ForgotPasswordTempletCode = _ForgotPasswordTempletCode;
            this._PasswordChangedTempletCode = _PasswordChangedTempletCode;
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
                    TempData["MsgNormal"] =
     "No Account Found With This Email Address. Please Check And Try Again.";
                    ForgotPasswordViewModel.Email = "";
                    return RedirectToPage();

                }
                else
                {
                    TimeSpan timeSpan = DateTime.Now - Convert.ToDateTime(Result.LastOtpSentTime);

                    if (timeSpan.TotalSeconds < 60)
                    {
                        int remainingSeconds = 60 - (int)timeSpan.TotalSeconds;

                        TempData["MsgNormal"] =
     $"You Can Request A New OTP After {remainingSeconds} Seconds.";

                        return RedirectToAction("ForgotPassword");
                    }



                    if (Result.LastFailedAttempt != null && Result.LastFailedAttempt < DateTime.Now.AddMinutes(-15))
                    {

                        Result.OTPAttempts = 0;


                        ObjAccountServices.UpdateOnlyOtpAttemts(Result.DoctorNurceId, Convert.ToInt32(Result.OTPAttempts));
                    }

                    if (Result.LockoutEndTime != null && Result.LockoutEndTime > DateTime.Now)
                    {
                        TempData["MsgDanger"] =
    "Your Account Is Temporarily Locked. Please Try Again Later.";
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




                    ObjAccountServices.SetOtpForUser(Result.DoctorNurceId, otp.ToString(), DateTime.Now.AddMinutes(10),System.DateTime.Now);

                    bool ResultOutput = _ForgotPasswordTempletCode.ForgotPasswordTempletCodeSend(Result.Email, EmailBody);

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

            ModelState.Clear();
            if (TryValidateModel(OtpViewModel, nameof(OtpViewModel)))
            {
                

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

                        TempData["MsgDanger"] = "Too many failed attempts. Your account has been locked for 15 minutes.";
                        
                        TempData["Step"] = "Email";
                    }
                    else
                    {
                        TempData["MsgDanger"]  = "Invalid OTP. Please try again.";
                        
                        TempData["Step"] = "Otp";
                        OtpViewModel.Otp = "";
                        return RedirectToPage();


                    }
                }
                else if (UserData.OTP==otp)
                {
                    TempData["Step"] = "Reset";

                    return RedirectToPage();

                }




              
            }

           
            return Page();
        }

        public IActionResult OnPostResetPassword()
        {
            ModelState.Clear();
            if (TryValidateModel(ForgotNewPassword, nameof(ForgotNewPassword)))
            {
                var ResetEmail = HttpContext.Session.GetString("ResetEmail");


                var UserData = ObjDoctorAndNurseServices.GetByEmail(ResetEmail);

                DoctorsAndNurse UpdateForgotPassword = new DoctorsAndNurse()
                {
                   
                   
                    
                    DoctorNurceId = UserData.DoctorNurceId,
                    PasswordHash = ForgotNewPassword.ConfirmPassword
                  
                    
                };

                int data = ObjAccountServices.SetPasswordForUser(UpdateForgotPassword);

                if (data > 0)
                {


                    string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "EmailTemplet", "PasswordChangedTemplet.html");

                    string EmailBody = System.IO.File.ReadAllText(filePath);

                    Random random = new Random();
                    
                    EmailBody = EmailBody.Replace("{{UserName}}", UserData.FirstName + " " + UserData.LastName);
                    EmailBody = EmailBody.Replace("{{UserEmail}}", UserData.Email);


                    

                    bool ResultOutput = _PasswordChangedTempletCode.PasswordChangedTempletCodeSend(UserData.Email, EmailBody);


                    TempData["MsgSuccess"] = "Your password has been changed successfully. Please log in using your new password.";


                    

                    return RedirectToPage("/Admin/Account/Login");
                }
            }
            return Page();
        }
    }
}
