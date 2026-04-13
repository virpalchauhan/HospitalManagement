using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagement.Helper;
using Azure.Core;

namespace HospitalManagement.Pages.Admin.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountServices ObjIAccountServices;
        private readonly IJwtTokenHelper ObjIJwtTokenHelper;

        [BindProperty]

       public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();




        public LoginModel(IAccountServices ObjIAccountServices, IJwtTokenHelper ObjIJwtTokenHelper)
        {
            this.ObjIAccountServices = ObjIAccountServices;
            this.ObjIJwtTokenHelper = ObjIJwtTokenHelper;
        }


        public void OnGet()
        {

            var token = Request.Cookies["AuthToken"];

            if (!string.IsNullOrEmpty(token))
            {
               
                Response.Redirect("/Admin/index");
            }
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                DoctorsAndNurse LoginData = new DoctorsAndNurse
                {
                    Email = LoginViewModel.Email,
                    PasswordHash = LoginViewModel.PasswordHash

                };

                var Result = ObjIAccountServices.Login(LoginData);

              

                var Token = ObjIJwtTokenHelper.JWTGenerateToken(Result.DoctorNurceId.ToString(), Result.RollType.ToString());

                if (Result.DoctorNurceId == 0)
                {
                    TempData["Msg"] = "Email and Password Dont match";
                }
                else if (Result.DoctorNurceId >= 1)
                {
                    Response.Cookies.Append("AuthToken", Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false, 
                        Expires = DateTime.Now.AddMinutes(60),
                        SameSite = SameSiteMode.Lax
                    });

                    return RedirectToPage("/Admin/index");
                }

               


            }
            return Page();
        }

       
    }
}
