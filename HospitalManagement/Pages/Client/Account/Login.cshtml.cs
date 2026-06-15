using HospitalManagement.Entity.Model;
using HospitalManagement.Helper;
using HospitalManagement.Services.Client;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Client.Account
{
    public class LoginModel : PageModel
    {

        private readonly IPatientServices _patientServices;
        private readonly IJwtTokenHelper _JwtTokenHelper;


        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public LoginModel(IPatientServices _patientServices, IJwtTokenHelper _JwtTokenHelper)
        {
            this._patientServices = _patientServices;
            this._JwtTokenHelper = _JwtTokenHelper;
        }


        public void OnGet()
        {
            


        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Patient LoginPatient = new Patient
                {
                    Email = LoginViewModel.Email,
                    PasswordHash = LoginViewModel.PasswordHash
                };

                var LoginResult = _patientServices.Login(LoginPatient);

                if (LoginResult != null)
                {
                    var Token = _JwtTokenHelper.JWTGenerateTokenForPatient(LoginResult.PatientId.ToString());
                    Response.Cookies.Append("AuthToken", Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        Expires = DateTime.Now.AddMinutes(60),
                        SameSite = SameSiteMode.Lax
                    });

                    return RedirectToPage("/Client/Home");
                }
                TempData["MsgDanger"] =
    "Email or Password Dont match";
                return RedirectToPage();


            }
            TempData["MsgNormal"] ="Something Wrong";
            return RedirectToPage();
        }
    }
}
