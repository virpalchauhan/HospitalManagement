using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagement.Helper;

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

              

                var Token = ObjIJwtTokenHelper.JWTGenerateToken(Result.DoctorId.ToString(), Result.RollType.ToString());

                if (Result.DoctorId == 0)
                {
                    TempData["Msg"] = "Email and Password Dont match";
                }
                else if (Result.DoctorId >= 1)
                {
                    Response.Cookies.Append("AuthToken", Token);
                    return RedirectToPage("/Admin/index");
                }

               


            }
            return Page();
        }

       
    }
}
