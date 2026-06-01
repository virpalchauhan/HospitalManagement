using HospitalManagement.Entity.Model;
using HospitalManagement.Services.Client;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Client.Account
{
    public class LoginModel : PageModel
    {

        private readonly IPatientServices _patientServices;


        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public LoginModel(IPatientServices _patientServices)
        {
            this._patientServices = _patientServices;
        }


        public void OnGet()
        {
            


        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                Patient LoginPatient = new Patient
                {
                    Email = LoginViewModel.Email,
                    PasswordHash = LoginViewModel.PasswordHash
                };

                var LoginResult = _patientServices.Login(LoginPatient);

            }
        }
    }
}
