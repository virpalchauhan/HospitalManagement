using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountServices ObjIAccountServices;

       [BindProperty]

       public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();




        public LoginModel(IAccountServices ObjIAccountServices)
        {
            this.ObjIAccountServices = ObjIAccountServices;
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

                int Result = ObjIAccountServices.Login(LoginData);


                if (Result==0)
                {
                    TempData["Msg"] = "Email and Password Dont match";
                }
                else if (Result >=1)
                {

                }
                   

            }
            return Page();
        }
    }
}
