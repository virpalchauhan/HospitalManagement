using HospitalManagement.EmailServices;
using HospitalManagement.Entity.Model;
using HospitalManagement.Helper;
using HospitalManagement.Services.Client;
using HospitalManagement.ViewModel;
using HospitalManagement.ViewModel.Client.Patient;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using System.ComponentModel.Design;

namespace HospitalManagement.Pages.Client.Account
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]

        public RegistrationPatientViewModel RegistrationPatientViewModel { get; set; } = new RegistrationPatientViewModel();

        [BindProperty]
        public SendOtpViewModel SendOtpViewModel { get; set; } = new SendOtpViewModel();


        [BindProperty]

        public VerifyOtpViewModel VerifyOtpViewModel { get; set; } = new VerifyOtpViewModel();

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPatientServices _patientServices;
        private readonly IPendingRegistrationPatientsServices _PendingRegistrationPatientsServices;
        private readonly IJwtTokenHelper _JwtTokenHelper;
        private readonly RegistrationOTPVerificationCode _RegistrationOTPVerificationCode;

        public RegistrationModel(IWebHostEnvironment _webHostEnvironment, IPatientServices _patientServices, IPendingRegistrationPatientsServices _PendingRegistrationPatientsServices, IJwtTokenHelper _JwtTokenHelper, RegistrationOTPVerificationCode _RegistrationOTPVerificationCode)
        {
            this._webHostEnvironment = _webHostEnvironment;
            this._patientServices = _patientServices;
            this._PendingRegistrationPatientsServices = _PendingRegistrationPatientsServices;
            this._RegistrationOTPVerificationCode = _RegistrationOTPVerificationCode;
        }

        [BindProperty]
        public string EmailInput { get; set; } = "abled";

        [BindProperty]
        public string RegistrationDiv { get; set; } = "Hide";

        [BindProperty]
        public string OtpInput { get; set; } = "Hide";

        [BindProperty]

        public string EmailBtn { get; set; } = "Show";
        [BindProperty]

        public string EmailDivClass { get; set; } = "col-md-8";



        public void OnGet()
        {
            
            if (TempData["OtpInputStep"] != null)
            {
                OtpInput = TempData["OtpInputStep"].ToString();
            }
            if (TempData["EmailInputStep"] != null)
            {
                EmailInput = TempData["EmailInputStep"].ToString();
            }
            if (TempData["RegistrationDivStep"] != null)
            {
                RegistrationDiv = TempData["RegistrationDivStep"].ToString();
            }
            if (TempData["EmailBtnStep"] != null)
            {
                EmailBtn = TempData["EmailBtnStep"].ToString();
            }
            if (TempData["EmailDivClassStep"] != null)
            {
                EmailDivClass = TempData["EmailDivClassStep"].ToString();
            }

        }

        public IActionResult OnPostSendOtp()
        {
            ModelState.Clear();

            if (TryValidateModel(SendOtpViewModel, nameof(SendOtpViewModel)))
            {


                if (_patientServices.UserExist(SendOtpViewModel.Email))
                {
                    TempData["MsgNormal"] =
    "An Account With This Email Address Already Exists. Please Use A Different Email Or Sign In.";
                    return RedirectToPage();

                }


                Random random = new Random();

                int RandomOTP = random.Next(100000, 999999);
                bool IsSend = false;
                bool AddResult = false;
                var PatientData = _PendingRegistrationPatientsServices.GetByEmail(SendOtpViewModel.Email);



                if (PatientData == null)
                {
                    PendingRegistrationPatients InsertpendingRegistrationPatients = new PendingRegistrationPatients
                    {
                        Email = SendOtpViewModel.Email,
                        OTP = RandomOTP.ToString(),
                        OTPExpiry = DateTime.Now.AddMinutes(10),
                        OTPAttempts = 0,
                        LastOTPSentTime = DateTime.Now
                    };

                    AddResult = _PendingRegistrationPatientsServices.AddPendingRegistrationPatient(InsertpendingRegistrationPatients);
                }
                else if (PatientData != null)
                {

                    TimeSpan TimeDifference = DateTime.Now - PatientData.LastOTPSentTime.Value;

                    if (TimeDifference.TotalSeconds < 60)
                    {
                        int remainingSeconds = 60 - (int)TimeDifference.TotalSeconds;

                        TempData["MsgNormal"] =
    $"Please Wait {remainingSeconds} Seconds Before Requesting Another OTP.";
                        TempData["Email"] = SendOtpViewModel.Email;
                        TempData["OtpInputStep"] = "Show";

                        return RedirectToPage();
                    }

                    int OtpAttempts = PatientData.OTPAttempts.Value;

                    if (OtpAttempts >= 5)
                    {
                        if (TimeDifference.TotalMinutes <= 15)
                        {
                            TempData["MsgDanger"] =
     "Your Account Has Been Temporarily Locked Due To Multiple Incorrect OTP Attempts.";

                            return RedirectToPage();
                        }

                        OtpAttempts = 0;
                    }

                    PendingRegistrationPatients UpdatependingRegistrationPatients =
                        new PendingRegistrationPatients
                        {
                            Email = SendOtpViewModel.Email,
                            OTP = RandomOTP.ToString(),
                            OTPExpiry = DateTime.Now.AddMinutes(10),
                            OTPAttempts = OtpAttempts,
                            LastOTPSentTime = DateTime.Now
                        };

                    AddResult =
                        _PendingRegistrationPatientsServices
                        .UpdatePendingRegistrationPatient(UpdatependingRegistrationPatients);
                }



                if (AddResult)
                {

                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplet", "RegistrationOTPVerification.html");

                    string EmailBody = System.IO.File.ReadAllText(filePath);


                    EmailBody = EmailBody.Replace("{{OTP}}", RandomOTP.ToString());
                    EmailBody = EmailBody.Replace("{{UserName}}", RegistrationPatientViewModel.FirstName + RegistrationPatientViewModel.LastName);

                    IsSend = _RegistrationOTPVerificationCode.RegistrationOTPVerificationCodeSend(SendOtpViewModel.Email, EmailBody);
                }



                if (IsSend)
                {
                    TempData["OtpInputStep"] = "Show";
                    TempData["MsgSuccess"] =
     "OTP Sent Successfully To Your Email. Please Check Your Inbox.";
                    TempData["Email"] = SendOtpViewModel.Email;

                }
                else
                {
                    _PendingRegistrationPatientsServices.DeletePendingRegistrationPatient(SendOtpViewModel.Email);


                    TempData["MsgDanger"] =
       "Failed To Send OTP. Please Try Again.";
                    return RedirectToPage();
                }
                return RedirectToPage();

            }

            return RedirectToPage();
        }

        public IActionResult OnPostOTPVerification()
        {


            ModelState.Clear();

            if (TryValidateModel(VerifyOtpViewModel, nameof(VerifyOtpViewModel)))
            {
                var PatientData = _PendingRegistrationPatientsServices.GetByEmail(VerifyOtpViewModel.Email);



                TimeSpan TimeDifference = DateTime.Now - PatientData.LastOTPSentTime.Value;
                int OtpAttempts = PatientData.OTPAttempts.Value;

                if (OtpAttempts >= 5)
                {
                    if (TimeDifference.TotalMinutes <= 15)
                    {
                        TempData["MsgDanger"] ="Your Account Has Been Temporarily Locked Due To Multiple Incorrect OTP Attempts.";

                        return RedirectToPage();
                    }


                    OtpAttempts = 0;
                }




                if (PatientData == null)
                {
                    TempData["MsgDanger"] = "Invalid Request.";

                    return RedirectToPage();
                }


                if (PatientData.OTPExpiry < DateTime.Now)
                {
                    TempData["MsgDanger"] = "OTP Has Expired.";
                    return RedirectToPage();
                }

                if (PatientData.OTP != VerifyOtpViewModel.OTP)
                {


                    OtpAttempts++;


                    PendingRegistrationPatients UpdateOtpAttemptsModel = new PendingRegistrationPatients
                    {
                        Email = VerifyOtpViewModel.Email,
                        OTPAttempts = OtpAttempts
                    };


                    _PendingRegistrationPatientsServices.UpdateOtpAttempts(UpdateOtpAttemptsModel);



                    TempData["Email"] = VerifyOtpViewModel.Email;
                    TempData["OtpInputStep"] = "Show";
                    TempData["MsgDanger"] = "Invalid OTP.";
                    return RedirectToPage();
                }
                TempData["MsgSuccess"] =
    "OTP Verified Successfully. You Can Now Complete Your Registration By Filling In The Remaining Details.";
                TempData["Email"] = VerifyOtpViewModel.Email;
                TempData["RegistrationDivStep"] = "Show";
                TempData["EmailInputStep"] = "disabled";
                TempData["EmailBtnStep"] = "Hide";
                TempData["EmailDivClassStep"] = "col-md-12";

                return RedirectToPage();




            }


            return RedirectToPage();

        }



        public IActionResult OnPostRegisterBtn()
        {
            ModelState.Clear();

            if (TryValidateModel(RegistrationPatientViewModel, nameof(RegistrationPatientViewModel)))
            {



                var ProfilePath = "";

                if (RegistrationPatientViewModel.ProfilePhoto != null && RegistrationPatientViewModel.ProfilePhoto.Length > 0)
                {
                    using FileStream fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "Client/ProfilePhoto/", RegistrationPatientViewModel.ProfilePhoto.FileName), FileMode.Create);
                    RegistrationPatientViewModel.ProfilePhoto.CopyTo(fs);
                    ProfilePath = "Client/ProfilePhoto/" + RegistrationPatientViewModel.ProfilePhoto.FileName;
                    fs.Close();
                }


                Patient insertData = new Patient
                {
                    FirstName = RegistrationPatientViewModel.FirstName,

                    LastName = RegistrationPatientViewModel.LastName,

                    Gender = RegistrationPatientViewModel.Gender,

                    DateOfBirth = RegistrationPatientViewModel.DateOfBirth,

                    MobileNo = RegistrationPatientViewModel.MobileNo,

                    Email = RegistrationPatientViewModel.Email,

                    PasswordHash = RegistrationPatientViewModel.PasswordHash,

                    ProfilePhotoPath = ProfilePath,

                    Address = RegistrationPatientViewModel.Address,

                    City = RegistrationPatientViewModel.City,

                    StateName = RegistrationPatientViewModel.StateName,

                    Pincode = RegistrationPatientViewModel.Pincode,

                    BloodGroup = RegistrationPatientViewModel.BloodGroup,

                    CreateDate = DateTime.Now
                };

                int ResultData = _patientServices.RegisterPatient(insertData);

                if (ResultData == 1)
                {
                    _PendingRegistrationPatientsServices.DeletePendingRegistrationPatient(SendOtpViewModel.Email);
                    Patient LoginData = new Patient
                    {
                        Email = RegistrationPatientViewModel.Email,
                        PasswordHash = RegistrationPatientViewModel.PasswordHash
                    };


                    var Login = _patientServices.Login(LoginData);

                    if (Login!=null)
                    {
                        var Token = _JwtTokenHelper.JWTGenerateTokenForPatient(Login.PatientId.ToString());
                        Response.Cookies.Append("AuthToken", Token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = false,
                            Expires = DateTime.Now.AddMinutes(60),
                            SameSite = SameSiteMode.Lax
                        });

                        return RedirectToPage("/Client/Home");
                    }




                }
                else if (ResultData == 0)
                {
                    TempData["MsgDanger"] =
     "Failed To Register Patient. Please Try Again.";
                    return RedirectToPage();
                }
                else if (ResultData == 2)
                {
                    TempData["MsgNormal"] =
    "Email Already Exists. Please Use A Different Email.";
                    return RedirectToPage();
                }

                return RedirectToPage();

            }
            return RedirectToPage();
        }

    }
}
